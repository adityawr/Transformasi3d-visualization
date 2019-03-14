using System;
using HelixToolkit.Wpf;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Shapes;

namespace TransformasiGGWP
{
    /// <summary>
    /// Interaction logic for Shearing.xaml
    /// </summary>
    public partial class Shearing : Window
    {
        Model3DGroup ModelShearing = new Model3DGroup();

        private int[,] ShX = new int[4, 8];
        private int[,] ShF = new int[4, 4];
        private int[,] ShY = new int[4, 8];
        public Shearing()
        {
            InitializeComponent();
            Matrixnol(ShX);
            Matrixnol(ShF);
            Matrixnol(ShY);
            ShearingDisableInput();
            CartesianforShearing();
        }
        private void Matrixnol(int[,] matrix)
        {
            for (int row = 0; row < matrix.GetLength(0); row++)
            {
                for (int col = 0; col < matrix.GetLength(1); col++)
                {
                    matrix[row, col] = 0;
                }
            }
        }

        private void Matrixnol(double[,] matrix)
        {
            for (int row = 0; row < matrix.GetLength(0); row++)
            {
                for (int col = 0; col < matrix.GetLength(1); col++)
                {
                    matrix[row, col] = 0;
                }
            }
        }
        private void MultiplyMatrix(int[,] resultMatrix, int[,] firstMatrix, int[,] secondMatrix)
        {
            for (int i = 0; i < resultMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < resultMatrix.GetLength(1); j++)
                {
                    for (int k = 0; k < resultMatrix.GetLength(0); k++)
                    {
                        resultMatrix[i, j] += firstMatrix[i, k] * secondMatrix[k, j];
                    }
                }
            }
        }

        private void MultiplyMatrix(double[,] resultMatrix, double[,] firstMatrix, double[,] secondMatrix)
        {
            for (int i = 0; i < resultMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < resultMatrix.GetLength(1); j++)
                {
                    for (int k = 0; k < resultMatrix.GetLength(0); k++)
                    {
                        resultMatrix[i, j] += firstMatrix[i, k] * secondMatrix[k, j];
                    }
                }
            }
        }

        private void GarisPemisah()
        {
            txtShLine.Text = String.Empty;

            for (int i = 0; i < txtShMat1.Text.Length; i++)
            {
                if (i == 0 || i == txtShMat1.Text.Length - 1)
                {
                    txtShLine.Text += "^";
                }
                else
                {
                    txtShLine.Text += "*";
                }
            }
        }
        private void ShearingDisableInput()
        {
            textBox_obj_sh_pjg.IsEnabled = true;
            textBox_obj_sh_lbr.IsEnabled = true;
            textBox_obj_sh_tgg.IsEnabled = true;
            button_set_obj.IsEnabled = true;

            textBox_or_sh_x.IsEnabled = false;
            textBox_or_sh_y.IsEnabled = false;
            textBox_or_sh_z.IsEnabled = false;
            button_set_or.IsEnabled = false;

            textBox_sh_xtoy.IsEnabled = false;
            textBox_sh_ytoz.IsEnabled = false;
            textBox_sh_ztox.IsEnabled = false;
            textBox_sh_ytox.IsEnabled = false;
            textBox_sh_ztoy.IsEnabled = false;
            textBox_sh_xtoz.IsEnabled = false;
            button_set_sh.IsEnabled = false;

            button_disp_sh.IsEnabled = false;
            button_res_sh.IsEnabled = false;
        }
        private void CartesianforShearing()
        {
            var axisBuilder = new MeshBuilder(true, true, true);
            axisBuilder.AddPipe(new Point3D(-100, 0, 0), new Point3D(100, 0, 0), 0, 0.1, 360);
            axisBuilder.AddPipe(new Point3D(0, -100, 0), new Point3D(0, 100, 0), 0, 0.1, 360);
            axisBuilder.AddPipe(new Point3D(0, 0, -100), new Point3D(0, 0, 100), 0, 0.1, 360);

            var mesh = axisBuilder.ToMesh(true);
            var material = MaterialHelper.CreateMaterial(Colors.Black);
            ModelShearing.Children.Add(new GeometryModel3D
            {
                Geometry = mesh,
                Material = material
            });
            ShModelVisual.Content = ModelShearing;
        }

        public void DrawShearingOrigin()
        {
            var meshBuilder = new MeshBuilder(false, false);
            meshBuilder.AddPipe(new Point3D(ShX[0, 4], ShX[1, 4], ShX[2, 4]), new Point3D(ShX[0, 5], ShX[1, 5], ShX[2, 5]), 0, 0.2, 360);
            meshBuilder.AddPipe(new Point3D(ShX[0, 4], ShX[1, 4], ShX[2, 4]), new Point3D(ShX[0, 7], ShX[1, 7], ShX[2, 7]), 0, 0.2, 360);
            meshBuilder.AddPipe(new Point3D(ShX[0, 6], ShX[1, 6], ShX[2, 6]), new Point3D(ShX[0, 5], ShX[1, 5], ShX[2, 5]), 0, 0.2, 360);
            meshBuilder.AddPipe(new Point3D(ShX[0, 6], ShX[1, 6], ShX[2, 6]), new Point3D(ShX[0, 7], ShX[1, 7], ShX[2, 7]), 0, 0.2, 360);

            meshBuilder.AddPipe(new Point3D(ShX[0, 0], ShX[1, 0], ShX[2, 0]), new Point3D(ShX[0, 4], ShX[1, 4], ShX[2, 4]), 0, 0.2, 360);
            meshBuilder.AddPipe(new Point3D(ShX[0, 1], ShX[1, 1], ShX[2, 1]), new Point3D(ShX[0, 5], ShX[1, 5], ShX[2, 5]), 0, 0.2, 360);
            meshBuilder.AddPipe(new Point3D(ShX[0, 2], ShX[1, 2], ShX[2, 2]), new Point3D(ShX[0, 6], ShX[1, 6], ShX[2, 6]), 0, 0.2, 360);
            meshBuilder.AddPipe(new Point3D(ShX[0, 3], ShX[1, 3], ShX[2, 3]), new Point3D(ShX[0, 7], ShX[1, 7], ShX[2, 7]), 0, 0.2, 360);

            meshBuilder.AddPipe(new Point3D(ShX[0, 0], ShX[1, 0], ShX[2, 0]), new Point3D(ShX[0, 1], ShX[1, 1], ShX[2, 1]), 0, 0.2, 360); //A & B
            meshBuilder.AddPipe(new Point3D(ShX[0, 1], ShX[1, 1], ShX[2, 1]), new Point3D(ShX[0, 2], ShX[1, 2], ShX[2, 2]), 0, 0.2, 360); //B & C
            meshBuilder.AddPipe(new Point3D(ShX[0, 2], ShX[1, 2], ShX[2, 2]), new Point3D(ShX[0, 3], ShX[1, 3], ShX[2, 3]), 0, 0.2, 360); //C & D
            meshBuilder.AddPipe(new Point3D(ShX[0, 3], ShX[1, 3], ShX[2, 3]), new Point3D(ShX[0, 0], ShX[1, 0], ShX[2, 0]), 0, 0.2, 360); //D & A
            var mesh = meshBuilder.ToMesh(true);

            var magentaMaterial = MaterialHelper.CreateMaterial(Colors.Magenta);

            ModelShearing.Children.Add(new GeometryModel3D
            {
                Geometry = mesh,
                Material = magentaMaterial
            });
        }
        public void ShearDestinationFinal()
        {
            var meshBuilder = new MeshBuilder(false, false);
            meshBuilder.AddPipe(new Point3D(ShY[0, 4], ShY[1, 4], ShY[2, 4]), new Point3D(ShY[0, 5], ShY[1, 5], ShY[2, 5]), 0, 0.2, 360);
            meshBuilder.AddPipe(new Point3D(ShY[0, 4], ShY[1, 4], ShY[2, 4]), new Point3D(ShY[0, 7], ShY[1, 7], ShY[2, 7]), 0, 0.2, 360);
            meshBuilder.AddPipe(new Point3D(ShY[0, 6], ShY[1, 6], ShY[2, 6]), new Point3D(ShY[0, 5], ShY[1, 5], ShY[2, 5]), 0, 0.2, 360);
            meshBuilder.AddPipe(new Point3D(ShY[0, 6], ShY[1, 6], ShY[2, 6]), new Point3D(ShY[0, 7], ShY[1, 7], ShY[2, 7]), 0, 0.2, 360);

            meshBuilder.AddPipe(new Point3D(ShY[0, 0], ShY[1, 0], ShY[2, 0]), new Point3D(ShY[0, 4], ShY[1, 4], ShY[2, 4]), 0, 0.2, 360);
            meshBuilder.AddPipe(new Point3D(ShY[0, 1], ShY[1, 1], ShY[2, 1]), new Point3D(ShY[0, 5], ShY[1, 5], ShY[2, 5]), 0, 0.2, 360);
            meshBuilder.AddPipe(new Point3D(ShY[0, 2], ShY[1, 2], ShY[2, 2]), new Point3D(ShY[0, 6], ShY[1, 6], ShY[2, 6]), 0, 0.2, 360);
            meshBuilder.AddPipe(new Point3D(ShY[0, 3], ShY[1, 3], ShY[2, 3]), new Point3D(ShY[0, 7], ShY[1, 7], ShY[2, 7]), 0, 0.2, 360);

            meshBuilder.AddPipe(new Point3D(ShY[0, 0], ShY[1, 0], ShY[2, 0]), new Point3D(ShY[0, 1], ShY[1, 1], ShY[2, 1]), 0, 0.2, 360); //A & B
            meshBuilder.AddPipe(new Point3D(ShY[0, 1], ShY[1, 1], ShY[2, 1]), new Point3D(ShY[0, 2], ShY[1, 2], ShY[2, 2]), 0, 0.2, 360); //B & C
            meshBuilder.AddPipe(new Point3D(ShY[0, 2], ShY[1, 2], ShY[2, 2]), new Point3D(ShY[0, 3], ShY[1, 3], ShY[2, 3]), 0, 0.2, 360); //C & D
            meshBuilder.AddPipe(new Point3D(ShY[0, 3], ShY[1, 3], ShY[2, 3]), new Point3D(ShY[0, 0], ShY[1, 0], ShY[2, 0]), 0, 0.2, 360); //D & A
            var mesh = meshBuilder.ToMesh(true);

            var redMaterial = MaterialHelper.CreateMaterial(Colors.Blue);

            ModelShearing.Children.Add(new GeometryModel3D
            {
                Geometry = mesh,
                Material = redMaterial
            });
        }
        private void button_disp_sh_Click(object sender, RoutedEventArgs e)
        {
            button_disp_sh.IsEnabled = false;
            button_res_sh.IsEnabled = true;

            MultiplyMatrix(ShY, ShF, ShX);

            txtShMat0.Text = "-> [A' B' C' D' E' F' G' H'] = [T] * [A B C D E F G H]";
            txtShMat1.Text = String.Format("   | {0,3} {1,3} {2,3} {3,3} {4,3} {5,3} {6,3} {7,3} |  | {8,3} {9,3} {10,3} {11,3} |   | {12,3} {13,3} {14,3} {15,3} {16,3} {17,3} {18,3} {19,3} |",
                                           ShY[0, 0], ShY[0, 1], ShY[0, 2], ShY[0, 3], ShY[0, 4], ShY[0, 5], ShY[0, 6], ShY[0, 7],
                                           ShF[0, 0], ShF[0, 1], ShF[0, 2], ShF[0, 3],
                                           ShX[0, 0], ShX[0, 1], ShX[0, 2], ShX[0, 3], ShX[0, 4], ShX[0, 5], ShX[0, 6], ShX[0, 7]);
            txtShMat2.Text = String.Format("   | {0,3} {1,3} {2,3} {3,3} {4,3} {5,3} {6,3} {7,3} | = | {8,3} {9,3} {10,3} {11,3} | * | {12,3} {13,3} {14,3} {15,3} {16,3} {17,3} {18,3} {19,3} |",
                                           ShY[1, 0], ShY[1, 1], ShY[1, 2], ShY[1, 3], ShY[1, 4], ShY[1, 5], ShY[1, 6], ShY[1, 7],
                                           ShF[1, 0], ShF[1, 1], ShF[1, 2], ShF[1, 3],
                                           ShX[1, 0], ShX[1, 1], ShX[1, 2], ShX[1, 3], ShX[1, 4], ShX[1, 5], ShX[1, 6], ShX[1, 7]);
            txtShMat3.Text = String.Format("   | {0,3} {1,3} {2,3} {3,3} {4,3} {5,3} {6,3} {7,3} |  | {8,3} {9,3} {10,3} {11,3} |   | {12,3} {13,3} {14,3} {15,3} {16,3} {17,3} {18,3} {19,3} |",
                                           ShY[2, 0], ShY[2, 1], ShY[2, 2], ShY[2, 3], ShY[2, 4], ShY[2, 5], ShY[2, 6], ShY[2, 7],
                                           ShF[2, 0], ShF[2, 1], ShF[2, 2], ShF[2, 3],
                                           ShX[2, 0], ShX[2, 1], ShX[2, 2], ShX[2, 3], ShX[2, 4], ShX[2, 5], ShX[2, 6], ShX[2, 7]);
            txtShMat4.Text = String.Format("   | {0,3} {1,3} {2,3} {3,3} {4,3} {5,3} {6,3} {7,3} |  | {8,3} {9,3} {10,3} {11,3} |   | {12,3} {13,3} {14,3} {15,3} {16,3} {17,3} {18,3} {19,3} |",
                                           ShY[3, 0], ShY[3, 1], ShY[3, 2], ShY[3, 3], ShY[3, 4], ShY[3, 5], ShY[3, 6], ShY[3, 7],
                                           ShF[3, 0], ShF[3, 1], ShF[3, 2], ShF[3, 3],
                                           ShX[3, 0], ShX[3, 1], ShX[3, 2], ShX[3, 3], ShX[3, 4], ShX[3, 5], ShX[3, 6], ShX[3, 7]);
            GarisPemisah();

            ShearDestinationFinal();
        }

        private void button_set_obj_Click(object sender, RoutedEventArgs e)
        {
            textBox_obj_sh_pjg.IsEnabled = false;
            textBox_obj_sh_lbr.IsEnabled = false;
            textBox_obj_sh_tgg.IsEnabled = false;
            button_set_obj.IsEnabled = false;

            textBox_or_sh_x.IsEnabled = true;
            textBox_or_sh_y.IsEnabled = true;
            textBox_or_sh_z.IsEnabled = true;
            button_set_or.IsEnabled = true;

            DeskripsiObjectShear.Text = String.Format ("Balok ABCD EFGH yang memiliki panjang {0}, lebar {1}, dan tinggi {2}", textBox_obj_sh_pjg.Text, textBox_obj_sh_lbr.Text, textBox_obj_sh_tgg.Text);

        }

        private void button_set_or_Click(object sender, RoutedEventArgs e)
        {
            textBox_or_sh_x.IsEnabled = false;
            textBox_or_sh_y.IsEnabled = false;
            textBox_or_sh_z.IsEnabled = false;
            button_set_or.IsEnabled = false;

            textBox_sh_xtoy.IsEnabled = true;
            textBox_sh_ytoz.IsEnabled = true;
            textBox_sh_ztox.IsEnabled = true;
            textBox_sh_ytox.IsEnabled = true;
            textBox_sh_ztoy.IsEnabled = true;
            textBox_sh_xtoz.IsEnabled = true;
            button_set_sh.IsEnabled = true;

            ShX[0, 0] = Int32.Parse(textBox_or_sh_x.Text);
            ShX[1, 0] = Int32.Parse(textBox_or_sh_y.Text);
            ShX[2, 0] = Int32.Parse(textBox_or_sh_z.Text);
            ShX[3, 0] = 1;

            ShX[0, 1] = Int32.Parse(textBox_or_sh_x.Text) + Int32.Parse(textBox_obj_sh_pjg.Text);
            ShX[1, 1] = Int32.Parse(textBox_or_sh_y.Text);
            ShX[2, 1] = Int32.Parse(textBox_or_sh_z.Text);
            ShX[3, 1] = 1;

            ShX[0, 2] = Int32.Parse(textBox_or_sh_x.Text) + Int32.Parse(textBox_obj_sh_pjg.Text);
            ShX[1, 2] = Int32.Parse(textBox_or_sh_y.Text) + Int32.Parse(textBox_obj_sh_lbr.Text);
            ShX[2, 2] = Int32.Parse(textBox_or_sh_z.Text);
            ShX[3, 2] = 1;

            ShX[0, 3] = Int32.Parse(textBox_or_sh_x.Text);
            ShX[1, 3] = Int32.Parse(textBox_or_sh_y.Text) + Int32.Parse(textBox_obj_sh_lbr.Text);
            ShX[2, 3] = Int32.Parse(textBox_or_sh_z.Text);
            ShX[3, 3] = 1;

            ShX[0, 4] = Int32.Parse(textBox_or_sh_x.Text);
            ShX[1, 4] = Int32.Parse(textBox_or_sh_y.Text);
            ShX[2, 4] = Int32.Parse(textBox_or_sh_z.Text) + Int32.Parse(textBox_obj_sh_tgg.Text);
            ShX[3, 4] = 1;

            ShX[0, 5] = Int32.Parse(textBox_or_sh_x.Text) + Int32.Parse(textBox_obj_sh_pjg.Text);
            ShX[1, 5] = Int32.Parse(textBox_or_sh_y.Text);
            ShX[2, 5] = Int32.Parse(textBox_or_sh_z.Text) + Int32.Parse(textBox_obj_sh_tgg.Text);
            ShX[3, 5] = 1;

            ShX[0, 6] = Int32.Parse(textBox_or_sh_x.Text) + Int32.Parse(textBox_obj_sh_pjg.Text);
            ShX[1, 6] = Int32.Parse(textBox_or_sh_y.Text) + Int32.Parse(textBox_obj_sh_lbr.Text);
            ShX[2, 6] = Int32.Parse(textBox_or_sh_z.Text) + Int32.Parse(textBox_obj_sh_tgg.Text);
            ShX[3, 6] = 1;

            ShX[0, 7] = Int32.Parse(textBox_or_sh_x.Text);
            ShX[1, 7] = Int32.Parse(textBox_or_sh_y.Text) + Int32.Parse(textBox_obj_sh_lbr.Text);
            ShX[2, 7] = Int32.Parse(textBox_or_sh_z.Text) + Int32.Parse(textBox_obj_sh_tgg.Text);
            ShX[3, 7] = 1;

            txtShPointA.Text = String.Format("A ({0}, {1}, {2})", ShX[0, 0], ShX[1, 0], ShX[2, 0]);
            txtShPointB.Text = String.Format("B ({0}, {1}, {2})", ShX[0, 1], ShX[1, 1], ShX[2, 1]);
            txtShPointC.Text = String.Format("C ({0}, {1}, {2})", ShX[0, 2], ShX[1, 2], ShX[2, 2]);
            txtShPointD.Text = String.Format("D ({0}, {1}, {2})", ShX[0, 3], ShX[1, 3], ShX[2, 3]);
            txtShPointE.Text = String.Format("E ({0}, {1}, {2})", ShX[0, 4], ShX[1, 4], ShX[2, 4]);
            txtShPointF.Text = String.Format("F ({0}, {1}, {2})", ShX[0, 5], ShX[1, 5], ShX[2, 5]);
            txtShPointG.Text = String.Format("G ({0}, {1}, {2})", ShX[0, 6], ShX[1, 6], ShX[2, 6]);
            txtShPointH.Text = String.Format("H ({0}, {1}, {2})", ShX[0, 7], ShX[1, 7], ShX[2, 7]);

            DrawShearingOrigin();
        }

        private void button_set_sh_Click(object sender, RoutedEventArgs e)
        {
            textBox_sh_xtoy.IsEnabled = false;
            textBox_sh_ytoz.IsEnabled = false;
            textBox_sh_ztox.IsEnabled = false;
            textBox_sh_ytox.IsEnabled = false;
            textBox_sh_ztoy.IsEnabled = false;
            textBox_sh_xtoz.IsEnabled = false;
            button_set_sh.IsEnabled = false;

            button_disp_sh.IsEnabled = true;

            ShF[0, 0] = 1;
            ShF[0, 1] = Int32.Parse(textBox_sh_xtoy.Text);
            ShF[0, 2] = Int32.Parse(textBox_sh_xtoz.Text);

            ShF[1, 1] = 1;
            ShF[1, 0] = Int32.Parse(textBox_sh_ytox.Text);
            ShF[1, 2] = Int32.Parse(textBox_sh_ytoz.Text);

            ShF[2, 2] = 1;
            ShF[2, 0] = Int32.Parse(textBox_sh_ztox.Text);
            ShF[2, 1] = Int32.Parse(textBox_sh_ztoy.Text);

            ShF[3, 3] = 1;
        }

        private void button_res_sh_Click(object sender, RoutedEventArgs e)
        {
            Matrixnol(ShX);
            Matrixnol(ShF);
            Matrixnol(ShY);
            ShearingDisableInput();
            CartesianforShearing();

            textBox_obj_sh_pjg.Text = "1";
            textBox_obj_sh_lbr.Text = "1";
            textBox_obj_sh_tgg.Text = "1";

            textBox_or_sh_x.Text = "0";
            textBox_or_sh_y.Text = "0";
            textBox_or_sh_z.Text = "0";

            textBox_sh_xtoy.Text = "0";
            textBox_sh_ytoz.Text = "0";
            textBox_sh_ztox.Text = "0";
            textBox_sh_ytox.Text = "0";
            textBox_sh_ztoy.Text = "0";
            textBox_sh_xtoz.Text = "0";

            ModelShearing.Children.Clear();

        }
    }
}
