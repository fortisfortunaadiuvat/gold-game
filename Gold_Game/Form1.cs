using Gold_Game;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gold
{
    public partial class Form1 : Form
    {
        private Panel[,] BoardPanels;
        
        int mSize;
        int nSize;
        int yuzde;
        int posx, posy;
        int[,] altinMiktari;
        static int[,] kirmiziAltin;
        public static bool[,] hedef_altin_var_mi ;
        public static int gizli_altin;
        static int sari_altin;
        int para;
        int value;
        int gizli;
        bool flag = true;
        static int[][] hedef = new int[5][];
        static int[,] hedef_x_y = new int[4, 2];
        int[,] abs = new int[4, 2];

        Player playerA;
        Player playerB;
        Player playerC;
        Player playerD;

        Player[] player = new Player[4];
        

        Random r = new Random();
        
        //İstenilen dizine çıktı almamıza yarayan kod satırları.
        private StreamWriter writer_a = new StreamWriter("C:\\Users\\HakanK\\source\\repos\\Gold_Game\\KBTesta.txt");
        private StreamWriter writer_b = new StreamWriter("C:\\Users\\HakanK\\source\\repos\\Gold_Game\\KBTestb.txt");
        private StreamWriter writer_c = new StreamWriter("C:\\Users\\HakanK\\source\\repos\\Gold_Game\\KBTestc.txt");
        private StreamWriter writer_d = new StreamWriter("C:\\Users\\HakanK\\source\\repos\\Gold_Game\\KBTestd.txt");


        public Form1()
        {
            InitializeComponent();
        }

        private void get_form_data()
        {
            label1.Visible = false;
            label2.Visible = false;
            label3.Visible = false;
            textBox1.Visible = false;
            textBox2.Visible = false;
            textBox3.Visible = false;
            button1.Visible = false;
            label8.Visible = false;
            label9.Visible = false;
            label10.Visible = false;
            label11.Visible = false;
            label12.Visible = false;
            label13.Visible = false;
            label14.Visible = false;
            textBox8.Visible = false;
            textBox9.Visible = false;
            textBox10.Visible = false;
            textBox11.Visible = false;
            textBox12.Visible = false;
            textBox13.Visible = false;
            textBox14.Visible = false;
            textBox15.Visible = false;
            textBox16.Visible = false;
            textBox17.Visible = false;
            textBox18.Visible = false;
            textBox19.Visible = false;

            label4.Visible = true;
            label5.Visible = true;
            label6.Visible = true;
            label7.Visible = true;
            textBox4.Visible = true;
            textBox5.Visible = true;
            textBox6.Visible = true;
            textBox7.Visible = true;
        }

        //Oyunu başlatacak olan butonun aktifleşmesi.
        private async void button1_Click(object sender, EventArgs e)
        {
            mSize = int.Parse(textBox1.Text);
            nSize = int.Parse(textBox2.Text);
            yuzde = int.Parse(textBox3.Text);

            //Oyun başlangıcındaki parametrelerin alınması işlemi:
            int player_a_cost_data = int.Parse(textBox8.Text);
            int player_a__target_cost_data = int.Parse(textBox9.Text);
            int player_a_number_of_gold_data = int.Parse(textBox10.Text);

            int player_b_cost_data = int.Parse(textBox11.Text);
            int player_b__target_cost_data = int.Parse(textBox12.Text);
            int player_b_number_of_gold_data = int.Parse(textBox13.Text);

            int player_c_cost_data = int.Parse(textBox14.Text);
            int player_c__target_cost_data = int.Parse(textBox15.Text);
            int player_c_number_of_gold_data = int.Parse(textBox16.Text);

            int player_d_cost_data = int.Parse(textBox17.Text);
            int player_d__target_cost_data = int.Parse(textBox18.Text);
            int player_d_number_of_gold_data = int.Parse(textBox19.Text);

            //Oyuncuların yaratılması işlemi.
            playerA = new Player("A", 5, 5, 200, mSize - 1, nSize - 1);
            playerB = new Player("B", 5, 10, 200, mSize - 1, 0);
            playerC = new Player("C", 5, 15, 200, 0, nSize - 1);
            playerD = new Player("D", 5, 20, 200, 0, 0);

            //Alınan parametrelerin oyunculara atanması işlemi.
            playerA.set_Cost(player_a_cost_data);
            playerA.set_target_cost(player_a__target_cost_data);
            playerA.set_number_of_gold(player_a_number_of_gold_data);

            playerB.set_Cost(player_b_cost_data);
            playerB.set_target_cost(player_b__target_cost_data);
            playerB.set_number_of_gold(player_b_number_of_gold_data);

            playerC.set_Cost(player_c_cost_data);
            playerC.set_target_cost(player_c__target_cost_data);
            playerC.set_number_of_gold(player_c_number_of_gold_data);

            playerD.set_Cost(player_d_cost_data);
            playerD.set_target_cost(player_d__target_cost_data);
            playerD.set_number_of_gold(player_d_number_of_gold_data);

            //Oyuncu dizisi içerisine objelerin atılması.
            player[0] = playerA;
            player[1] = playerB;
            player[2] = playerC;
            player[3] = playerD;

            //Butonların ve labellerın görünür hale gelmesi.
            get_form_data();

            //İlgili alana oyuncunun puanını yazdırmak için puanların alınması.
            textBox4.Text = playerA.get_number_of_gold().ToString();
            textBox5.Text = playerB.get_number_of_gold().ToString();
            textBox6.Text = playerC.get_number_of_gold().ToString();
            textBox7.Text = playerD.get_number_of_gold().ToString();

            const int tileSize = 20;

            var clr1 = Color.Cyan;
            var clr2 = Color.Magenta;

            //Her bir oyuncu için renklerin tanımlanması.
            playerA.color = Color.DarkBlue;
            playerB.color = Color.Chocolate;
            playerC.color = Color.LightGreen;
            playerD.color = Color.Olive;

            //Dosyaya ilk bilgilerin kaydedilmesi.
            writer_a.WriteLine("A oyuncusunun hareket ve konum bilgileri:");
            writer_b.WriteLine("B oyuncusunun hareket ve konum bilgileri:");
            writer_c.WriteLine("C oyuncusunun hareket ve konum bilgileri:");
            writer_d.WriteLine("D oyuncusunun hareket ve konum bilgileri:");

            //"chess board" tanımlanması
            BoardPanels = new Panel[mSize, nSize];
            altinMiktari = new int[mSize, nSize];
            Form1.kirmiziAltin = new int[mSize, nSize];
            Form1.hedef_altin_var_mi = new bool[mSize, nSize];

            for (var m = 0; m < mSize; m++)
            {
                for (var n = 0; n < nSize; n++)
                {
                    //chess board alanlarının tanımlanması.
                    var newPanel = new Panel
                    {
                        Size = new Size(tileSize, tileSize),
                        Location = new Point(tileSize * m, tileSize * n)
                    };

                    altinMiktari[m, n] = 0;
                    Form1.kirmiziAltin[m,n] = 0;

                    //Panelin Controle eklenerek görselleştirilmesi.
                    Controls.Add(newPanel);

                    //Gelecekte kullanılmak ve değiştirilmek üzere Board Panelin tanımlanması.
                    BoardPanels[m, n] = newPanel;

                    //Arka plan renklendirmesi
                    if (m % 2 == 0)
                        newPanel.BackColor = n % 2 != 0 ? clr1 : clr2;
                    else
                        newPanel.BackColor = n % 2 != 0 ? clr2 : clr1;

                }
            }

            //Gizlive Normal altın sayılarının bulunması.
            int altin = mSize * nSize * yuzde / 100;
            gizli = altin / 10;
            Form1.gizli_altin = gizli;
            Form1.sari_altin = altin;

            int x, y;

            //Random olarak altınların panellere atanması işlemi.
            while (altin > 0)
            {
                x = r.Next(1,mSize-1);
                y = r.Next(1,nSize-1);

                    if (BoardPanels[x, y].BackColor != Color.Gold && BoardPanels[x, y].BackColor != Color.Red)
                    {
                        BoardPanels[x, y].BackColor = Color.Gold;
                        if (gizli > 0)
                        {
                            BoardPanels[x, y].BackColor = Color.Red;
                            Form1.kirmiziAltin[x, y] = 1;
                            gizli--;
                        }
                        altinMiktari[x, y] = r.Next(1, 5) * 5;
                        Form1.hedef_altin_var_mi[x, y] = true;

                        altin--;
                    }
                
            }

            //Oyuncuların panele renklendirme işlemi ile yerleştirilmesi.
            BoardPanels[playerA.get_x_coordinate(),playerA.get_y_coordinate()].BackColor = playerA.color;
            BoardPanels[playerB.get_x_coordinate(), playerB.get_y_coordinate()].BackColor = playerB.color;
            BoardPanels[playerC.get_x_coordinate(), playerC.get_y_coordinate()].BackColor = playerC.color;
            BoardPanels[playerD.get_x_coordinate(), playerD.get_y_coordinate()].BackColor = playerD.color;

            //Oyuncuların elenmesi veya kazanması durumunda gösterilecek olan mesaj kutusu.
            MessageBoxButtons buttons = MessageBoxButtons.OK;

            while (Form1.sari_altin > 0 )
            {
                int oyuncu_a_puan = int.Parse(textBox4.Text);
                int oyuncu_b_puan = int.Parse(textBox5.Text);
                int oyuncu_c_puan = int.Parse(textBox6.Text);
                int oyuncu_d_puan = int.Parse(textBox7.Text);

                //Oyundaki aktif oyuncu sayısını tutan değişken.
                int number_of_players_in_game = 4;

                if(oyuncu_a_puan <= 0)
                {
                    number_of_players_in_game--;

                    string message = "A oyuncusu elenmiştir";
                    string caption = "";

                    MessageBox.Show(message, caption, buttons);
                }
                if (oyuncu_b_puan <= 0)
                {
                    number_of_players_in_game--;

                    string message = "B oyuncusu elenmiştir";
                    string caption = "";

                    MessageBox.Show(message, caption, buttons);
                }
                if (oyuncu_c_puan <= 0)
                {
                    number_of_players_in_game--;

                    string message = "C oyuncusu elenmiştir";
                    string caption = "";

                    MessageBox.Show(message, caption, buttons);
                }
                if (oyuncu_d_puan <= 0)
                {
                    number_of_players_in_game--;

                    string message = "D oyuncusu elenmiştir";
                    string caption = "";

                    MessageBox.Show(message, caption, buttons);
                }
                //Oyunun daha rahat anlaşılabilmesi için gecikme eklendi.
                await Task.Delay(1000);

                //Oyunun minimum 2 oyuncu ile oynanması sağlandı aksi halde döngüye girmedi.
                if(number_of_players_in_game >= 2)
                {
                    //iç içe döngüler ile oyuncuların sıra ile oynanması sağlandı.
                    for (int c = 0; c < 4; c++)
                    {
                        //Her bir oyuncu için eğer sırası gelmiş ise hedef belirleme algoritmalarının çalıştırılması.
                        if (c == 0)
                        {
                            hedef[0] = player[0].Set_TargetA(player[0].get_x_coordinate(), player[0].get_y_coordinate(), altinMiktari, mSize, nSize, Form1.kirmiziAltin);
                            writer_a.WriteLine("A oyuncusu için yeni hedef x:" + hedef[0][0] + " " + "y:" + hedef[0][1]);
                        }
                        if (c == 1)
                        {
                            hedef[1] = player[1].Set_TargetB(player[1].get_x_coordinate(), player[1].get_y_coordinate(), altinMiktari, mSize, nSize, Form1.kirmiziAltin);
                            writer_b.WriteLine("B oyuncusu için yeni hedef x:" + hedef[1][0] + " " + "y:" + hedef[1][1]);
                        }
                        if (c == 2)
                        {
                            hedef[2] = player[2].Set_TargetC_phase_1(player[2].get_x_coordinate(), player[2].get_y_coordinate(), altinMiktari, mSize, nSize, Form1.kirmiziAltin);
                            writer_c.WriteLine("C oyuncusu için kırmızı yeni hedef x:" + hedef[2][0] + " " + "y:" + hedef[2][1]);

                            //Programın başlangıcında C 'nin en yakındaki gizli altınlar sarı oluyor.
                            if (Form1.gizli_altin > 0)
                            {
                                BoardPanels[hedef[2][0], hedef[2][1]].BackColor = Color.Gold;
                                Form1.gizli_altin--;
                            }

                            hedef[2] = player[2].Set_TargetC_phase_2(player[2].get_x_coordinate(), player[2].get_y_coordinate(), altinMiktari, mSize, nSize, Form1.kirmiziAltin);
                            writer_c.WriteLine("C oyuncusu için  yeni hedef x:" + hedef[2][0] + " " + "y:" + hedef[2][1]);
                        }
                        if (c == 3)
                        {
                            hedef[3] = player[3].Set_TargetD(player[3].get_x_coordinate(), player[3].get_y_coordinate(), altinMiktari, Form1.kirmiziAltin, mSize, nSize, hedef, player);
                            writer_d.WriteLine("C oyuncusu için  yeni hedef x:" + hedef[3][0] + " " + "y:" + hedef[3][1]);
                        }

                        //Her bir oyuncu için hedef belirleme algoritmasının ardından
                        //varsayılan hedef belirleme puanları düşüldü.
                        player[c].set_number_of_gold(player[c].get_number_of_gold() - player[c].get_target_cost());

                        //Hedef ile oyuncu arasındaki mesafenin bulunması.
                        hedef_x_y[c, 0] = hedef[c][0] - player[c].get_x_coordinate();
                        hedef_x_y[c, 1] = hedef[c][1] - player[c].get_y_coordinate();

                        //Bulunan mesafenin mutlak değerinin alınması sayesinde döngü daha rahat kontrol edilmiştir.
                        abs[c, 0] = Math.Abs(hedef_x_y[c, 0]);
                        abs[c, 1] = Math.Abs(hedef_x_y[c, 1]);

                        int counter = 0;

                        for (int l = 0; l < abs[c, 0]; l++)
                        {
                            if (counter < 3)
                            {
                                if (hedef_x_y[c, 0] < 0)
                                {
                                    counter++;
                                    await Sola_hareketAsync(player[c], c);
                                }
                                else if (hedef_x_y[c, 0] > 0)
                                {
                                    counter++;
                                    await Saga_hareketAsync(player[c], c);

                                }
                            }
                        }

                        //Oyuncunun maksimum 3 hamle yapması sağlandı.
                        counter = 3 - counter;

                        for (int l = 0; l < abs[c, 1]; l++)
                        {
                            if (counter > 0)
                            {
                                if (hedef_x_y[c, 1] < 0)
                                {
                                    counter--;
                                    await Yukari_hareketAsync(player[c], c);
                                }
                                else if (hedef_x_y[c, 1] > 0)
                                {
                                    counter--;
                                    await Asagi_hareketAsync(player[c], c);
                                }
                            }

                        }

                    }
                }
            }

            int max = player[0].get_number_of_gold();
            int player_sonuc = 0;

            for(int count = 1; count < 4; count++)
            {
                if(player[count].get_number_of_gold() > max)
                {
                    max = player[count].get_number_of_gold();
                    player_sonuc = count;
                }
            }

            MessageBoxButtons buttons2 = MessageBoxButtons.OK;

            string message2 = "Player" + player[player_sonuc].get_Name() + " oyunu kazanmıştır!";
            string caption2 = "";

            MessageBox.Show(message2, caption2, buttons2);

            writer_a.Close();
            writer_b.Close();
            writer_c.Close();
            writer_d.Close();
        }

        //Her bir oyuncu için sağa hareket hamlesinin algoritması.
        //Hareket sonucunda oyuncu puanlarının ilgili alanlara yazılması.
        private async Task Saga_hareketAsync(Player player,int sira)
        {
            //Başlangıç olarak oyuncu puanlarının alınması ve değiştirilmesi.
            para = player.get_number_of_gold();
            para -= 5;
            player.set_number_of_gold(para);

            //Sıraya kimde ise onun ilgili alanındaki puan değerinin değiştirilmesi.
            if (sira == 0 && para >= 0)
            {
                textBox4.Text = para.ToString();
            }
            if (sira == 1 && para >= 0)
            {
                textBox5.Text = para.ToString();
            }
            if (sira == 2 && para >= 0)
            {
                textBox6.Text = para.ToString();
            }
            if (sira == 3 && para >= 0)
            {
                textBox7.Text = para.ToString();
            }

            posx = player.get_x_coordinate();
            posy = player.get_y_coordinate();

            if (value == 1)
            {
                BoardPanels[posx, posy].BackColor = Color.Gold;
                value = 0;
            }
            else
            {
                if (posx % 2 == 0)
                    BoardPanels[posx, posy].BackColor = posy % 2 != 0 ? Color.Cyan : Color.Magenta;
                else
                    BoardPanels[posx, posy].BackColor = posy % 2 != 0 ? Color.Magenta : Color.Cyan;
            }
            posx++;

            //Hareketlerin panelin dışına çıkması engellendi.
            if (posx > nSize - 1)
            {
                flag = false;
                posx--;

                para = player.get_number_of_gold();
                para += 5;
                player.set_number_of_gold(para);
                textBox4.Text = para.ToString();

                Console.WriteLine("u hit the wall ! get back!");
            }

            //İlgili oyuncunun hareket ettiği noktadaki renk değişimi.
            BoardPanels[posx, posy].BackColor = player.color;
            player.set_x_coordinate(posx);

            //Varılan noktada altın değeri olması durumunda ilgili oyuncunun puanının artırılması.
            //Ve ilgili paneldeki altın sayısının sıfırlanması.
            if (altinMiktari[posx, posy] != 0 && Form1.kirmiziAltin[posx, posy] == 0 && flag == true)
            {
                value = 0;
                para = player.get_number_of_gold();
                para += altinMiktari[posx, posy];
                player.set_number_of_gold(para);

                if (sira == 0 && para >= 0)
                {
                    textBox4.Text = para.ToString();
                }
                if (sira == 1 && para >= 0)
                {
                    textBox5.Text = para.ToString();
                }
                if (sira == 2 && para >= 0)
                {
                    textBox6.Text = para.ToString();
                }
                if (sira == 3 && para >= 0)
                {
                    textBox7.Text = para.ToString();
                }

                altinMiktari[posx, posy] = 0;
                Form1.hedef_altin_var_mi[posx, posy] = false;

                Form1.sari_altin--;

            }
            else if (altinMiktari[posx, posy] != 0 && Form1.kirmiziAltin[posx, posy] == 1 && flag == true)
            {
                //İlgili alanın gizli altın bulundurduğunu belirten değişken.
                value = 1;

                Form1.kirmiziAltin[posx, posy] = 0;

                Form1.gizli_altin--;
            }

            flag = true;

            if(sira == 0)
            {
                writer_a.WriteLine("x:" + (posx) + "y:" + (posy));
            }
            if (sira == 1)
            {
                writer_b.WriteLine("x:" + (posx) + "y:" + (posy));
            }
            if (sira == 2)
            {
                writer_c.WriteLine("x:" + (posx) + "y:" + (posy));
            }
            if (sira == 3)
            {
                writer_d.WriteLine("x:" + (posx) + "y:" + (posy));
            }

            //Her bir hamle sonrası gecikme atanması ile paneldeki hareketlerin daha rahat gözlenmesi.
            await Task.Delay(1000);
        }

        //Her bir oyuncu için sola hareket hamlesinin algoritması.
        //Hareket sonucunda oyuncu puanlarının ilgili alanlara yazılması.
        private async Task Sola_hareketAsync(Player player,int sira)
        {
            para = player.get_number_of_gold();
            para -= 5;
            player.set_number_of_gold(para);
                        
            if (sira == 0 && para >= 0)
            {
                textBox4.Text = para.ToString();
            }
            if (sira == 1 && para >= 0)
            {
                textBox5.Text = para.ToString();
            }
            if (sira == 2 && para >= 0)
            {
                textBox6.Text = para.ToString();
            }
            if (sira == 3 && para >= 0)
            {
                textBox7.Text = para.ToString();
            }

            posx = player.get_x_coordinate();
            posy = player.get_y_coordinate();

            //Eğer ki kırmızı altın alınmış ise ilgili panelin bölümünü sarı yapar.
            if (value == 1)
            {
                BoardPanels[posx, posy].BackColor = Color.Gold;
                value = 0;

            }
            else
            {
                if (posx % 2 == 0)
                    BoardPanels[posx, posy].BackColor = posy % 2 != 0 ? Color.Cyan : Color.Magenta;
                else
                    BoardPanels[posx, posy].BackColor = posy % 2 != 0 ? Color.Magenta : Color.Cyan;
            }

            posx--;
                        
            if (posx < 0)
            {
                flag = false;
                posx++;

                para = player.get_number_of_gold();
                para += 5;
                player.set_number_of_gold(para);
                textBox4.Text = para.ToString();

                Console.WriteLine("u hit the wall ! get back!");
            }
                        
            BoardPanels[posx, posy].BackColor = player.color;
            player.set_x_coordinate(posx);
                        
            if (altinMiktari[posx, posy] != 0 && Form1.kirmiziAltin[posx, posy] == 0 && flag == true)
            {
                value = 0;
                para = playerA.get_number_of_gold();
                para += altinMiktari[posx, posy];
                playerA.set_number_of_gold(para);

                if (sira == 0 && para >= 0)
                {
                    textBox4.Text = para.ToString();
                }
                if (sira == 1 && para >= 0)
                {
                    textBox5.Text = para.ToString();
                }
                if (sira == 2 && para >= 0)
                {
                    textBox6.Text = para.ToString();
                }
                if (sira == 3 && para >= 0)
                {
                    textBox7.Text = para.ToString();
                }

                altinMiktari[posx, posy] = 0;
                Form1.hedef_altin_var_mi[posx, posy] = false;

                Form1.sari_altin--;

            }
            else if (altinMiktari[posx, posy] != 0 && Form1.kirmiziAltin[posx, posy] == 1 && flag == true)
            {
                value = 1;

                Form1.kirmiziAltin[posx, posy] = 0;

                Form1.gizli_altin--;
            }

            flag = true;

            if (sira == 0)
            {
                writer_a.WriteLine("x:" + (posx) + "y:" + (posy));
            }
            if (sira == 1)
            {
                writer_b.WriteLine("x:" + (posx) + "y:" + (posy));
            }
            if (sira == 2)
            {
                writer_c.WriteLine("x:" + (posx) + "y:" + (posy));
            }
            if (sira == 3)
            {
                writer_d.WriteLine("x:" + (posx) + "y:" + (posy));
            }

            await Task.Delay(1000);
        }
        //Her bir oyuncu için yukarı hareket hamlesinin algoritması.
        //Hareket sonucunda oyuncu puanlarının ilgili alanlara yazılması.
        private async Task Yukari_hareketAsync(Player player,int sira)
        {
            para = player.get_number_of_gold();
            para -= 5;
            player.set_number_of_gold(para);

            if (sira == 0 && para >= 0)
            {
                textBox4.Text = para.ToString();
            }
            if (sira == 1 && para >= 0)
            {
                textBox5.Text = para.ToString();
            }
            if (sira == 2 && para >= 0)
            {
                textBox6.Text = para.ToString();
            }
            if (sira == 3 && para >= 0)
            {
                textBox7.Text = para.ToString();
            }

            posx = player.get_x_coordinate();
            posy = player.get_y_coordinate();

            if (value == 1)
            {
                BoardPanels[posx, posy].BackColor = Color.Gold;
                value = 0;
            }
            else
            {
                if (posx % 2 == 0)
                    BoardPanels[posx, posy].BackColor = posy % 2 != 0 ? Color.Cyan : Color.Magenta;
                else
                    BoardPanels[posx, posy].BackColor = posy % 2 != 0 ? Color.Magenta : Color.Cyan;
            }
            posy--;
            if (posy < 0)
            {
                flag = false;
                posy++;

                para = playerA.get_number_of_gold();
                para += 5;
                playerA.set_number_of_gold(para);
                textBox4.Text = para.ToString();

                Console.WriteLine("u hit the wall ! get back!");
            }

            BoardPanels[posx, posy].BackColor = player.color;
            player.set_y_coordinate(posy);

            if (altinMiktari[posx, posy] != 0 && Form1.kirmiziAltin[posx, posy] == 0 && flag == true)
            {
                value = 0;
                para = player.get_number_of_gold();
                para += altinMiktari[posx, posy];
                player.set_number_of_gold(para);

                if (sira == 0 && para >= 0)
                {
                    textBox4.Text = para.ToString();
                }
                if (sira == 1 && para >= 0)
                {
                    textBox5.Text = para.ToString();
                }
                if (sira == 2 && para >= 0)
                {
                    textBox6.Text = para.ToString();
                }
                if (sira == 3 && para >= 0)
                {
                    textBox7.Text = para.ToString();
                }

                altinMiktari[posx, posy] = 0;
                Form1.hedef_altin_var_mi[posx, posy] = false;

                Form1.sari_altin--;

            }
            else if (altinMiktari[posx, posy] != 0 && Form1.kirmiziAltin[posx, posy] == 1 && flag == true)
            {
                value = 1;

                Form1.kirmiziAltin[posx, posy] = 0;
                Form1.gizli_altin--;
            }
            flag = true;

            if (sira == 0)
            {
                writer_a.WriteLine("x:" + (posx) + "y:" + (posy));
            }
            if (sira == 1)
            {
                writer_b.WriteLine("x:" + (posx) + "y:" + (posy));
            }
            if (sira == 2)
            {
                writer_c.WriteLine("x:" + (posx) + "y:" + (posy));
            }
            if (sira == 3)
            {
                writer_d.WriteLine("x:" + (posx) + "y:" + (posy));
            }

            await Task.Delay(1000);
        }

        private async Task Asagi_hareketAsync(Player player,int sira)
        {
            para = player.get_number_of_gold();
            para -= 5;
            player.set_number_of_gold(para);

            if (sira == 0 && para >= 0)
            {
                textBox4.Text = para.ToString();
            }
            if (sira == 1 && para >= 0)
            {
                textBox5.Text = para.ToString();
            }
            if (sira == 2 && para >= 0)
            {
                textBox6.Text = para.ToString();
            }
            if (sira == 3 && para >= 0)
            {
                textBox7.Text = para.ToString();
            }

            posx = player.get_x_coordinate();
            posy = player.get_y_coordinate();

            if (value == 1)
            {
                BoardPanels[posx, posy].BackColor = Color.Gold;
                value = 0;
            }
            else
            {
                if (posx % 2 == 0)
                    BoardPanels[posx, posy].BackColor = posy % 2 != 0 ? Color.Cyan : Color.Magenta;
                else
                    BoardPanels[posx, posy].BackColor = posy % 2 != 0 ? Color.Magenta : Color.Cyan;
            }
            posy++;
            if (posy > mSize - 1)
            {
                flag = false;
                posy--;

                para = player.get_number_of_gold();
                para += 5;
                player.set_number_of_gold(para);
                textBox4.Text = para.ToString();

                Console.WriteLine("u hit the wall ! get back!");
            }

            BoardPanels[posx, posy].BackColor = player.color;
            player.set_y_coordinate(posy);

            if (altinMiktari[posx, posy] != 0 && Form1.kirmiziAltin[posx, posy] == 0 && flag == true)
            {
                value = 0;

                para = player.get_number_of_gold();
                para += altinMiktari[posx, posy];
                player.set_number_of_gold(para);

                if(sira == 0 && para >= 0)
                {
                    textBox4.Text = para.ToString();
                }
                if (sira == 1 && para >= 0)
                {
                    textBox5.Text = para.ToString();
                }
                if (sira == 2 && para >= 0)
                {
                    textBox6.Text = para.ToString();
                }
                if (sira == 3 && para >= 0)
                {
                    textBox7.Text = para.ToString();
                }

                altinMiktari[posx, posy] = 0;
                Form1.hedef_altin_var_mi[posx, posy] = false;

                Form1.sari_altin--;

            }
            else if (altinMiktari[posx, posy] != 0 && Form1.kirmiziAltin[posx, posy] == 1 && flag == true)
            {
                value = 1;

                Form1.kirmiziAltin[posx, posy] = 0;

                Form1.gizli_altin--;
            }
            flag = true;

            if (sira == 0)
            {
                writer_a.WriteLine("x:" + (posx) + "y:" + (posy));
            }
            if (sira == 1)
            {
                writer_b.WriteLine("x:" + (posx) + "y:" + (posy));
            }
            if (sira == 2)
            {
                writer_c.WriteLine("x:" + (posx) + "y:" + (posy));
            }
            if (sira == 3)
            {
                writer_d.WriteLine("x:" + (posx) + "y:" + (posy));
            }

            await Task.Delay(1000);
        }

    }
}
