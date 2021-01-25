using Gold;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gold_Game
{
    public class Player
    {
        string name;
        int cost;
        int target_cost;
        int number_of_gold;
        int coordinate_x;
        int coordinate_y;

        public static bool baska_hedef = false;

        public int adim_sayisi;

        public  Color color;

        public Player() { }
        public Player(string name, int cost, int target_cost, int number_of_gold,
                        int coordinate_x, int coordinate_y)
        {
            this.name = name;
            this.cost = cost;
            this.target_cost = target_cost;
            this.number_of_gold = number_of_gold;
            this.coordinate_x = coordinate_x;
            this.coordinate_y = coordinate_y;
        }
        public string get_Name()
        {
            return this.name;
        }
        public void set_Name(string name)
        {
            this.name = name;
        }
        public int get_Cost()
        {
            return this.cost;
        }
        public void set_Cost(int cost)
        {
            this.cost = cost;
        }
        public int get_target_cost()
        {
            return this.target_cost;
        }
        public void set_target_cost(int target_cost)
        {
            this.target_cost = target_cost;
        }

        //A oyuncusu için hedef belirleme algortiması.
        public int[] Set_TargetA(int x, int y, int[,] altin, int m, int n,int[,] gizli)
        {
            int i, j;
            int[] enYakin = new int[2];
            int yakin = 9999;
            int toplam;

            enYakin[0] = 0;
            enYakin[1] = 0;

            for (i = m - 1; i >= 0; i--)
            {
                for (j = n - 1; j >= 0; j--)
                {
                    if (altin[i, j] > 0 && gizli[i,j] != 1 && Form1.hedef_altin_var_mi[i, j] == true)
                    {
                        toplam = Math.Abs(i - x) + Math.Abs(j - y);
                        if (toplam < yakin)
                        {
                            enYakin[0] = i;
                            enYakin[1] = j;
                            yakin = toplam;
                        }
                    }

                }
            }

            return enYakin;
        }

        //B oyuncusu için hedef belirleme algortiması.
        public int[] Set_TargetB(int x, int y, int[,] altin, int m, int n,int[,] gizli)
        {
            int i, j;
            int[] enKarli = new int[2];
            int yakin = 0;
            int kar;

            enKarli[0] = 0;
            enKarli[1] = 0;

            for (i = m - 1; i >= 0; i--)
            {
                for (j = n - 1; j >= 0; j--)
                {
                    if (altin[i, j] > 0 && gizli[i,j] != 1 && Form1.hedef_altin_var_mi[i, j] == true)
                    {
                        int absolute = Math.Abs(i - x) + Math.Abs(j - y);
                        if (absolute != 0)
                        {
                            kar = altin[i, j] / absolute;
                            if (kar > yakin)
                            {
                                enKarli[0] = i;
                                enKarli[1] = j;
                                yakin = kar;
                            }
                        }

                    }

                }
            }

            return enKarli;
        }

        //c kirmizi altina gider
        public int[] Set_TargetC_phase_1(int x, int y, int[,] altin, int m, int n, int[,] gizli)
        {
            int i, j;
            int[] enYakin = new int[2];
            int gizliYakin = 9999;
            int toplam;

            enYakin[0] = 0;
            enYakin[1] = 0;

            
            for (i = m - 1; i >= 0; i--)
            {
                for (j = n - 1; j >= 0; j--)
                {
                    if (gizli[i, j] > 0)
                    {
                        int absolute = Math.Abs(i - x) + Math.Abs(j - y);
                        if (absolute != 0)
                        {
                            toplam = absolute;
                            if (toplam < gizliYakin)
                            {
                                enYakin[0] = i;
                                enYakin[1] = j;
                                gizliYakin = toplam;
                            }
                        }
                    }

                }
            }

            return enYakin;
        }

        //c kirmizi altindan sonra sari altini hedefler
        public int[] Set_TargetC_phase_2(int x, int y, int[,] altin, int m, int n, int[,] gizli)
        {
            int i, j;
            int[] enKarli = new int[2];
            int yakin = 0;
            int kar;

            enKarli[0] = 0;
            enKarli[1] = 0;

            for (i = m - 1; i >= 0; i--)
            {
                for (j = n - 1; j >= 0; j--)
                {
                    if (altin[i, j] > 0 && gizli[i, j] != 1 && Form1.hedef_altin_var_mi[i, j] == true)
                    {
                        int absolute = Math.Abs(i - x) + Math.Abs(j - y);
                        if (absolute != 0)
                        {
                            kar = altin[i, j] / absolute;
                            if (kar > yakin)
                            {
                                enKarli[0] = i;
                                enKarli[1] = j;
                                yakin = kar;
                            }
                        }

                    }

                }
            }

            return enKarli;
        }

        //D için hedef belirleme algoritması.
        public int[] Set_TargetD(int x, int y, int[,] altin, int[,] gizli ,int m, int n, int[][] hedef,Player[] player)
        {
            int i, j;

            int[] enKarli = new int[2];

            enKarli[0] = 0;
            enKarli[1] = 0;

            int yakin = 0;
            int kar;

            //D oyuncusunun A,B ve C'nin hedeflerine uzaklığı.
            int absoulute_rel_D_A = Math.Abs(x - hedef[0][0]) + Math.Abs(y - hedef[0][1]);
            int absoulute_rel_D_B = Math.Abs(x - hedef[1][0]) + Math.Abs(y - hedef[1][1]);
            int absoulute_rel_D_C = Math.Abs(x - hedef[2][0]) + Math.Abs(y - hedef[2][1]);

            //Bu hedeflerden en küçük olanın bulunması.
            int comperative = absoulute_rel_D_A;

            if (absoulute_rel_D_B < absoulute_rel_D_A)
            {
                comperative = absoulute_rel_D_B;
            }
            else if (absoulute_rel_D_C < absoulute_rel_D_A)
            {
                comperative = absoulute_rel_D_C;
            }

            int absolute_rel_A = Math.Abs(player[0].get_x_coordinate() - hedef[0][0]) + Math.Abs(player[0].get_y_coordinate() - hedef[0][1]);
            int absolute_rel_B = Math.Abs(player[1].get_x_coordinate() - hedef[1][0]) + Math.Abs(player[1].get_y_coordinate() - hedef[1][1]);
            int absolute_rel_C = Math.Abs(player[2].get_x_coordinate() - hedef[2][0]) + Math.Abs(player[2].get_y_coordinate() - hedef[2][1]);

            //D'nin A,B ve C'nin hedeflerine uzaklaığının karşılaştırılması.
            if (absoulute_rel_D_A < absolute_rel_A && comperative == absolute_rel_A)
            {
                enKarli[0] = hedef[0][0];
                enKarli[1] = hedef[0][1];

                Player.baska_hedef = true;
            }
            else if (absoulute_rel_D_B < absolute_rel_B && comperative == absolute_rel_B)
            {
                enKarli[0] = hedef[1][0];
                enKarli[1] = hedef[1][1];

                Player.baska_hedef = true;
            }
            else if (absoulute_rel_D_C < absolute_rel_C && comperative == absolute_rel_C)
            {
                enKarli[0] = hedef[3][0];
                enKarli[1] = hedef[3][1];

                Player.baska_hedef = true;
            }
            else
            {
                Player.baska_hedef = false;

                for (i = m - 1; i >= 0; i--)
                {
                    for (j = n - 1; j >= 0; j--)
                    {
                        if (altin[i, j] > 0 && Form1.hedef_altin_var_mi[i, j] == true)
                        {
                            int absolute = Math.Abs(i - x) + Math.Abs(j - y);
                            if (absolute != 0)
                            {
                                kar = altin[i, j] / absolute;
                                if (kar > yakin)
                                {
                                    enKarli[0] = i;
                                    enKarli[1] = j;
                                    yakin = kar;
                                }
                            }

                        }

                    }
                }
            }

            return enKarli;
        }

        public int get_number_of_gold()
        {
            return this.number_of_gold;
        }
        public void set_number_of_gold(int nofGold)
        {
            this.number_of_gold = nofGold;
        }
        public int get_x_coordinate()
        {
            return this.coordinate_x;
        }
        public int get_y_coordinate()
        {
            return this.coordinate_y;
        }

        public void set_x_coordinate(int xx)
        {
            this.coordinate_x = xx;
        }
        public void set_y_coordinate(int yy)
        {
            this.coordinate_y = yy;
        }
    }

}
