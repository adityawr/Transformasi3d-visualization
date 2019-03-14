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
    /// Interaction logic for Rotating.xaml
    /// </summary>
    public partial class Rotating : Window
    {
        Model3DGroup RotationModel = new Model3DGroup();

        private double[,] RoX = new double[4, 8];
        private double[,] RoF = new double[4, 12];
        private double[,] RoA = new double[4, 4];
        private double[,] RoB = new double[4, 4];
        private double[,] RoC = new double[4, 4];
        private double[,] RoD = new double[4, 8];
        private double[,] RoE = new double[4, 8];
        private double[,] RoM = new double[4, 4];
        private double[,] RoY = new double[4, 8];

        public Rotating()
        {
            InitializeComponent();

            Matrixnol(RoX);
            Matrixnol(RoA);
            Matrixnol(RoB);
            Matrixnol(RoC);
            Matrixnol(RoD);
            Matrixnol(RoE);
            Matrixnol(RoM);
            Matrixnol(RoY);
            RotateInput();
            RoDrawCartesianAxis();
        }

        private void RotateInput()
        {
            textBox_or_x_ro.IsEnabled = false;
            textBox_or_y_ro.IsEnabled = false;
            textBox_or_z_ro.IsEnabled = false;
            button_or_ro.IsEnabled = false;

            checkBox_X.IsEnabled = false;
            checkBox_Y.IsEnabled = false;
            checkBox_Z.IsEnabled = false;
            button_set_ro.IsEnabled = false;

            button_ro.IsEnabled = false;
            button_res_ro.IsEnabled = false;
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
            txtRtLine.Text = String.Empty;

            for (int i = 0; i < txtRtMat1.Text.Length; i++)
            {
                if (i == 0 || i == txtRtMat1.Text.Length - 1)
                {
                    txtRtLine.Text += "^";
                }
                else
                {
                    txtRtLine.Text += "*";
                }
            }
        }
        private void RoDrawCartesianAxis()
        {
            var axisBuilder = new MeshBuilder(false, false);
            axisBuilder.AddPipe(new Point3D(-100, 0, 0), new Point3D(100, 0, 0), 0, 0.1, 360);
            axisBuilder.AddPipe(new Point3D(0, -100, 0), new Point3D(0, 100, 0), 0, 0.1, 360);
            axisBuilder.AddPipe(new Point3D(0, 0, -100), new Point3D(0, 0, 100), 0, 0.1, 360);

            var mesh = axisBuilder.ToMesh(true);
            var material = MaterialHelper.CreateMaterial(Colors.Black);
            RotationModel.Children.Add(new GeometryModel3D
            {
                Geometry = mesh,
                Material = material
            });
            RoModelVisual.Content = RotationModel;
        }
        public void RoDrawObjectOrigin()
        {
            var meshBuilder = new MeshBuilder(false, false);

            meshBuilder.AddPipe(new Point3D(RoX[0, 4], RoX[1, 4], RoX[2, 4]), new Point3D(RoX[0, 5], RoX[1, 5], RoX[2, 5]), 0, 0.2, 360);
            meshBuilder.AddPipe(new Point3D(RoX[0, 4], RoX[1, 4], RoX[2, 4]), new Point3D(RoX[0, 7], RoX[1, 7], RoX[2, 7]), 0, 0.2, 360);
            meshBuilder.AddPipe(new Point3D(RoX[0, 6], RoX[1, 6], RoX[2, 6]), new Point3D(RoX[0, 5], RoX[1, 5], RoX[2, 5]), 0, 0.2, 360);
            meshBuilder.AddPipe(new Point3D(RoX[0, 6], RoX[1, 6], RoX[2, 6]), new Point3D(RoX[0, 7], RoX[1, 7], RoX[2, 7]), 0, 0.2, 360);

            meshBuilder.AddPipe(new Point3D(RoX[0, 0], RoX[1, 0], RoX[2, 0]), new Point3D(RoX[0, 4], RoX[1, 4], RoX[2, 4]), 0, 0.2, 360);
            meshBuilder.AddPipe(new Point3D(RoX[0, 1], RoX[1, 1], RoX[2, 1]), new Point3D(RoX[0, 5], RoX[1, 5], RoX[2, 5]), 0, 0.2, 360);
            meshBuilder.AddPipe(new Point3D(RoX[0, 2], RoX[1, 2], RoX[2, 2]), new Point3D(RoX[0, 6], RoX[1, 6], RoX[2, 6]), 0, 0.2, 360);
            meshBuilder.AddPipe(new Point3D(RoX[0, 3], RoX[1, 3], RoX[2, 3]), new Point3D(RoX[0, 7], RoX[1, 7], RoX[2, 7]), 0, 0.2, 360);

            meshBuilder.AddPipe(new Point3D(RoX[0, 0], RoX[1, 0], RoX[2, 0]), new Point3D(RoX[0, 1], RoX[1, 1], RoX[2, 1]), 0, 0.2, 360); //A & B
            meshBuilder.AddPipe(new Point3D(RoX[0, 1], RoX[1, 1], RoX[2, 1]), new Point3D(RoX[0, 2], RoX[1, 2], RoX[2, 2]), 0, 0.2, 360); //B & C
            meshBuilder.AddPipe(new Point3D(RoX[0, 2], RoX[1, 2], RoX[2, 2]), new Point3D(RoX[0, 3], RoX[1, 3], RoX[2, 3]), 0, 0.2, 360); //C & D
            meshBuilder.AddPipe(new Point3D(RoX[0, 3], RoX[1, 3], RoX[2, 3]), new Point3D(RoX[0, 0], RoX[1, 0], RoX[2, 0]), 0, 0.2, 360); //D & A
            var mesh = meshBuilder.ToMesh(true);
            var blueMaterial = MaterialHelper.CreateMaterial(Colors.Magenta);

            RotationModel.Children.Add(new GeometryModel3D
            {
                Geometry = mesh,
                Material = blueMaterial
            });
        }

        private void checkBox_X_Checked(object sender, RoutedEventArgs e)
        {
            checkBox_Y.IsEnabled = false;
            checkBox_Z.IsEnabled = false;
        }
        private void checkBox_Y_Checked(object sender, RoutedEventArgs e)
        {
            checkBox_X.IsEnabled = false;
            checkBox_Z.IsEnabled = false;
        }

        private void checkBox_Z_Checked(object sender, RoutedEventArgs e)
        {
            checkBox_X.IsEnabled = false;
            checkBox_Y.IsEnabled = false;
        }

        private void button_set_ro_Click(object sender, RoutedEventArgs e)
        {
            var axisBuilder = new MeshBuilder(false, false);
            button_set_ro.IsEnabled = false;
            textbox_fin_deg_ro.IsEnabled = false;
            button_ro.IsEnabled = true;
            double sudutPutar = Int32.Parse(textbox_fin_deg_ro.Text) * (Math.PI / 180);
            if (checkBox_X.IsChecked == true)
            {
                axisBuilder.AddPipe(new Point3D(-100, Int32.Parse(textBox_or_y_ro.Text), Int32.Parse(textBox_or_z_ro.Text)), new Point3D(100, Int32.Parse(textBox_or_y_ro.Text), Int32.Parse(textBox_or_z_ro.Text)), 0, 0.2, 360);
                RoF[0, 4] = 1;
                RoF[1, 5] = Math.Cos(sudutPutar);
                RoF[1, 6] = -Math.Sin(sudutPutar);
                RoF[2, 5] = Math.Sin(sudutPutar);
                RoF[2, 6] = Math.Cos(sudutPutar);
                RoF[3, 7] = 1;           
                button_set_ro.IsEnabled = true;
            }
            if(checkBox_Y.IsChecked== true)
            {
                axisBuilder.AddPipe(new Point3D(Int32.Parse(textBox_or_x_ro.Text), -100, Int32.Parse(textBox_or_z_ro.Text)), new Point3D(Int32.Parse(textBox_or_x_ro.Text), 100, Int32.Parse(textBox_or_z_ro.Text)), 0, 0.2, 360);
                RoF[0, 4] = Math.Cos(sudutPutar);
                RoF[0, 6] = Math.Sin(sudutPutar);
                RoF[1, 5] = 1;
                RoF[2, 4] = -Math.Sin(sudutPutar);
                RoF[2, 6] = Math.Cos(sudutPutar);
                RoF[3, 7] = 1;
                button_set_ro.IsEnabled = true;
            }
            if (checkBox_Z.IsChecked==true)
            {
                axisBuilder.AddPipe(new Point3D(Int32.Parse(textBox_or_x_ro.Text), Int32.Parse(textBox_or_y_ro.Text), -100), new Point3D(Int32.Parse(textBox_or_x_ro.Text), Int32.Parse(textBox_or_y_ro.Text), 100), 0, 0.2, 360);
                RoF[0, 4] = Math.Cos(sudutPutar);
                RoF[0, 5] = -Math.Sin(sudutPutar);
                RoF[1, 4] = Math.Sin(sudutPutar);
                RoF[1, 5] = Math.Cos(sudutPutar);
                RoF[2, 6] = 1;
                RoF[3, 7] = 1;
                button_set_ro.IsEnabled = true;
            }
            var mesh = axisBuilder.ToMesh(true);
            var material = MaterialHelper.CreateMaterial(Colors.Green);
            RotationModel.Children.Add(new GeometryModel3D
            {
                Geometry = mesh,
                Material = material
            });
            RoModelVisual.Content = RotationModel;

            DeskripsiRot.Text = String.Format("akan dirotasi {0} derajat dengan sumbu putar yang telah di centang", textbox_fin_deg_ro.Text);
        }
        public void RoDrawObjectDestination()
        {
            var meshBuilder = new MeshBuilder(false, false);

            meshBuilder.AddPipe(new Point3D(RoY[0, 4], RoY[1, 4], RoY[2, 4]), new Point3D(RoY[0, 5], RoY[1, 5], RoY[2, 5]), 0, 0.2, 360);
            meshBuilder.AddPipe(new Point3D(RoY[0, 4], RoY[1, 4], RoY[2, 4]), new Point3D(RoY[0, 7], RoY[1, 7], RoY[2, 7]), 0, 0.2, 360);
            meshBuilder.AddPipe(new Point3D(RoY[0, 6], RoY[1, 6], RoY[2, 6]), new Point3D(RoY[0, 5], RoY[1, 5], RoY[2, 5]), 0, 0.2, 360);
            meshBuilder.AddPipe(new Point3D(RoY[0, 6], RoY[1, 6], RoY[2, 6]), new Point3D(RoY[0, 7], RoY[1, 7], RoY[2, 7]), 0, 0.2, 360);

            meshBuilder.AddPipe(new Point3D(RoY[0, 0], RoY[1, 0], RoY[2, 0]), new Point3D(RoY[0, 4], RoY[1, 4], RoY[2, 4]), 0, 0.2, 360);
            meshBuilder.AddPipe(new Point3D(RoY[0, 1], RoY[1, 1], RoY[2, 1]), new Point3D(RoY[0, 5], RoY[1, 5], RoY[2, 5]), 0, 0.2, 360);
            meshBuilder.AddPipe(new Point3D(RoY[0, 2], RoY[1, 2], RoY[2, 2]), new Point3D(RoY[0, 6], RoY[1, 6], RoY[2, 6]), 0, 0.2, 360);
            meshBuilder.AddPipe(new Point3D(RoY[0, 3], RoY[1, 3], RoY[2, 3]), new Point3D(RoY[0, 7], RoY[1, 7], RoY[2, 7]), 0, 0.2, 360);

            meshBuilder.AddPipe(new Point3D(RoY[0, 0], RoY[1, 0], RoY[2, 0]), new Point3D(RoY[0, 1], RoY[1, 1], RoY[2, 1]), 0, 0.2, 360); //A & B
            meshBuilder.AddPipe(new Point3D(RoY[0, 1], RoY[1, 1], RoY[2, 1]), new Point3D(RoY[0, 2], RoY[1, 2], RoY[2, 2]), 0, 0.2, 360); //B & C
            meshBuilder.AddPipe(new Point3D(RoY[0, 2], RoY[1, 2], RoY[2, 2]), new Point3D(RoY[0, 3], RoY[1, 3], RoY[2, 3]), 0, 0.2, 360); //C & D
            meshBuilder.AddPipe(new Point3D(RoY[0, 3], RoY[1, 3], RoY[2, 3]), new Point3D(RoY[0, 0], RoY[1, 0], RoY[2, 0]), 0, 0.2, 360); //D & A
            var mesh = meshBuilder.ToMesh(true);
            var redMaterial = MaterialHelper.CreateMaterial(Colors.Red);

            RotationModel.Children.Add(new GeometryModel3D
            {
                Geometry = mesh,
                Material = redMaterial
            });
        }
        private void RoObjectSelection(object sender, SelectionChangedEventArgs e)
        {
            txtRtPointE.Visibility = Visibility.Visible;
            txtRtPointF.Visibility = Visibility.Visible;
            txtRtPointG.Visibility = Visibility.Visible;
            txtRtPointH.Visibility = Visibility.Visible;
        }

        private void button_obj_ro_Click(object sender, RoutedEventArgs e)
        {
            textBox_obj_pjg_ro.IsEnabled = false;
            textBox_obj_lbr_ro.IsEnabled = false;
            textBox_obj_tgg_ro.IsEnabled = false;
            button_obj_ro.IsEnabled = false;
            textBox_or_x_ro.IsEnabled = true;
            textBox_or_y_ro.IsEnabled = true;
            textBox_or_z_ro.IsEnabled = true;
            button_or_ro.IsEnabled = true;
            DeskripsiObjectRot.Text = String.Format("-> Balok ABCD EFGH dengan panjang {0}, lebar {1}, dan tinggi {2}", textBox_obj_pjg_ro.Text, textBox_obj_lbr_ro.Text, textBox_obj_tgg_ro.Text);
        }

        private void button_or_ro_Click(object sender, RoutedEventArgs e)
        {
            textBox_or_x_ro.IsEnabled = false;
            textBox_or_y_ro.IsEnabled = false;
            textBox_or_z_ro.IsEnabled = false;
            button_or_ro.IsEnabled = false;
            checkBox_X.IsEnabled = true;
            checkBox_Y.IsEnabled = true;
            checkBox_Z.IsEnabled = true;
            textbox_fin_deg_ro.IsEnabled = true;
            button_set_ro.IsEnabled = true;

            RoX[0, 0] = Int32.Parse(textBox_or_x_ro.Text);
            RoX[1, 0] = Int32.Parse(textBox_or_y_ro.Text);
            RoX[2, 0] = Int32.Parse(textBox_or_z_ro.Text);
            RoX[3, 0] = 1;

            RoX[0, 1] = Int32.Parse(textBox_or_x_ro.Text) + Int32.Parse(textBox_obj_pjg_ro.Text);
            RoX[1, 1] = Int32.Parse(textBox_or_y_ro.Text);
            RoX[2, 1] = Int32.Parse(textBox_or_z_ro.Text);
            RoX[3, 1] = 1;

            RoX[0, 2] = Int32.Parse(textBox_or_x_ro.Text) + Int32.Parse(textBox_obj_pjg_ro.Text);
            RoX[1, 2] = Int32.Parse(textBox_or_y_ro.Text) + Int32.Parse(textBox_obj_lbr_ro.Text);
            RoX[2, 2] = Int32.Parse(textBox_or_z_ro.Text);
            RoX[3, 2] = 1;

            RoX[0, 3] = Int32.Parse(textBox_or_x_ro.Text);
            RoX[1, 3] = Int32.Parse(textBox_or_y_ro.Text) + Int32.Parse(textBox_obj_lbr_ro.Text);
            RoX[2, 3] = Int32.Parse(textBox_or_z_ro.Text);
            RoX[3, 3] = 1;

            RoX[0, 4] = Int32.Parse(textBox_or_x_ro.Text);
            RoX[1, 4] = Int32.Parse(textBox_or_y_ro.Text);
            RoX[2, 4] = Int32.Parse(textBox_or_z_ro.Text) + Int32.Parse(textBox_obj_tgg_ro.Text);
            RoX[3, 4] = 1;

            RoX[0, 5] = Int32.Parse(textBox_or_x_ro.Text) + Int32.Parse(textBox_obj_pjg_ro.Text);
            RoX[1, 5] = Int32.Parse(textBox_or_y_ro.Text);
            RoX[2, 5] = Int32.Parse(textBox_or_z_ro.Text) + Int32.Parse(textBox_obj_tgg_ro.Text);
            RoX[3, 5] = 1;

            RoX[0, 6] = Int32.Parse(textBox_or_x_ro.Text) + Int32.Parse(textBox_obj_pjg_ro.Text);
            RoX[1, 6] = Int32.Parse(textBox_or_y_ro.Text) + Int32.Parse(textBox_obj_lbr_ro.Text);
            RoX[2, 6] = Int32.Parse(textBox_or_z_ro.Text) + Int32.Parse(textBox_obj_tgg_ro.Text);
            RoX[3, 6] = 1;

            RoX[0, 7] = Int32.Parse(textBox_or_x_ro.Text);
            RoX[1, 7] = Int32.Parse(textBox_or_y_ro.Text) + Int32.Parse(textBox_obj_lbr_ro.Text);
            RoX[2, 7] = Int32.Parse(textBox_or_z_ro.Text) + Int32.Parse(textBox_obj_tgg_ro.Text);
            RoX[3, 7] = 1;

            RoF[0, 8] = 1;
            RoF[0, 11] = -RoX[0, 0];

            RoF[1, 9] = 1;
            RoF[1, 11] = -RoX[1, 0];

            RoF[2, 10] = 1;
            RoF[2, 11] = -RoX[2, 0];

            RoF[3, 11] = 1;

            RoF[0, 0] = 1;
            RoF[0, 3] = RoX[0, 0];

            RoF[1, 1] = 1;
            RoF[1, 3] = RoX[1, 0];

            RoF[2, 2] = 1;
            RoF[2, 3] = RoX[2, 0];

            RoF[3, 3] = 1;

            txtRtPointA.Text = String.Format("A ({0}, {1}, {2})", RoX[0, 0], RoX[1, 0], RoX[2, 0]);
            txtRtPointB.Text = String.Format("B ({0}, {1}, {2})", RoX[0, 1], RoX[1, 1], RoX[2, 1]);
            txtRtPointC.Text = String.Format("C ({0}, {1}, {2})", RoX[0, 2], RoX[1, 2], RoX[2, 2]);
            txtRtPointD.Text = String.Format("D ({0}, {1}, {2})", RoX[0, 3], RoX[1, 3], RoX[2, 3]);
            txtRtPointE.Text = String.Format("E ({0}, {1}, {2})", RoX[0, 4], RoX[1, 4], RoX[2, 4]);
            txtRtPointF.Text = String.Format("F ({0}, {1}, {2})", RoX[0, 5], RoX[1, 5], RoX[2, 5]);
            txtRtPointG.Text = String.Format("G ({0}, {1}, {2})", RoX[0, 6], RoX[1, 6], RoX[2, 6]);
            txtRtPointH.Text = String.Format("H ({0}, {1}, {2})", RoX[0, 7], RoX[1, 7], RoX[2, 7]);
            RoDrawObjectOrigin();
        }
        private void InitializeMatrix()
        {
            for (int row = 0; row < 4; row++)
            {
                for (int col = 0; col < 4; col++)
                {
                    RoA[row, col] = RoF[row, col];
                    RoB[row, col] = RoF[row, col + 4];
                    RoC[row, col] = RoF[row, col + 8];
                }
            }
        }

        private void button_ro_Click(object sender, RoutedEventArgs e)
        {
            button_ro.IsEnabled = false;
            button_res_ro.IsEnabled = true;

            InitializeMatrix();
            MultiplyMatrix(RoD, RoC, RoX);
            MultiplyMatrix(RoE, RoB, RoD);
            MultiplyMatrix(RoY, RoA, RoE);

            for (int i = 0; i < 4; i++)
            {
                for (int j = 4; j < 8; j++)
                {
                    RoF[i, j] = Math.Round(RoF[i, j], 2);
                }
            }

            txtRtMat0.Text = "-> [T'] * [R] * [T]";
            txtRtMat1.Text = String.Format("   | {0,3} {1,3} {2,3} {3,3} |   | {4,5} {5,5} {6,5} {7,5} |   | {8,3} {9,3} {10,3} {11,3} |",
                                            RoF[0, 0], RoF[0, 1], RoF[0, 2], RoF[0, 3],
                                            RoF[0, 4], RoF[0, 5], RoF[0, 6], RoF[0, 7],
                                            RoF[0, 8], RoF[0, 9], RoF[0, 10], RoF[0, 11]);
            txtRtMat2.Text = String.Format("   | {0,3} {1,3} {2,3} {3,3} |   | {4,5} {5,5} {6,5} {7,5} |   | {8,3} {9,3} {10,3} {11,3} |",
                                            RoF[1, 0], RoF[1, 1], RoF[1, 2], RoF[1, 3],
                                            RoF[1, 4], RoF[1, 5], RoF[1, 6], RoF[1, 7],
                                            RoF[1, 8], RoF[1, 9], RoF[1, 10], RoF[1, 11]);
            txtRtMat3.Text = String.Format("   | {0,3} {1,3} {2,3} {3,3} |   | {4,5} {5,5} {6,5} {7,5} |   | {8,3} {9,3} {10,3} {11,3} |",
                                            RoF[2, 0], RoF[2, 1], RoF[2, 2], RoF[2, 3],
                                            RoF[2, 4], RoF[2, 5], RoF[2, 6], RoF[2, 7],
                                            RoF[2, 8], RoF[2, 9], RoF[2, 10], RoF[2, 11]);
            txtRtMat4.Text = String.Format("   | {0,3} {1,3} {2,3} {3,3} |   | {4,5} {5,5} {6,5} {7,5} |   | {8,3} {9,3} {10,3} {11,3} |",
                                            RoF[3, 0], RoF[3, 1], RoF[3, 2], RoF[3, 3],
                                            RoF[3, 4], RoF[3, 5], RoF[3, 6], RoF[3, 7],
                                            RoF[3, 8], RoF[3, 9], RoF[3, 10], RoF[3, 11]);

            GarisPemisah();

            RoDrawObjectDestination();
        }

        private void button_res_ro_Click(object sender, RoutedEventArgs e)
        {
            Matrixnol(RoX);
            Matrixnol(RoA);
            Matrixnol(RoB);
            Matrixnol(RoC);
            Matrixnol(RoD);
            Matrixnol(RoE);
            Matrixnol(RoF);
            Matrixnol(RoM);
            Matrixnol(RoY);

            textBox_obj_pjg_ro.IsEnabled = true;
            textBox_obj_lbr_ro.IsEnabled = true;
            textBox_obj_tgg_ro.IsEnabled = true;
            button_obj_ro.IsEnabled = true;

            checkBox_X.IsChecked = false;
            checkBox_Y.IsChecked = false;
            checkBox_Z.IsChecked = false;

            textBox_obj_pjg_ro.Text = "1";
            textBox_obj_lbr_ro.Text = "1";
            textBox_obj_tgg_ro.Text = "1";

            textBox_or_x_ro.Text = "0";
            textBox_or_y_ro.Text = "0";
            textBox_or_z_ro.Text = "0";

            textbox_fin_deg_ro.Text = "0";

            RotationModel.Children.Clear();
            RoDrawCartesianAxis();
        }
    }
}
