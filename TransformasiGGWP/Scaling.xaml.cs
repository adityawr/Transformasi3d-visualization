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
    /// Interaction logic for Scaling.xaml
    /// </summary>
    public partial class Scaling : Window
    {
        Model3DGroup ModelScaling = new Model3DGroup();
        private int[,] ScX = new int[4, 8];
        private int[,] ScF = new int[4, 4];
        private int[,] ScY = new int[4, 8];
        public Scaling()
        {
            InitializeComponent();
            Matrixnol(ScX);
            Matrixnol(ScF);
            Matrixnol(ScY);
            ScToDisableSomeInput();
            DrawCartesianAxisforScale();
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
            txtScLine.Text = String.Empty;

            for (int i = 0; i < txtScMat1.Text.Length; i++)
            {
                if (i == 0 || i == txtScMat1.Text.Length - 1)
                {
                    txtScLine.Text += "^";
                }
                else
                {
                    txtScLine.Text += "*";
                }
            }
        }
        private void ScToDisableSomeInput()
        {
            textBox_obj_pjg_sc.IsEnabled = true;
            textBox_obj_lbr_sc.IsEnabled = true;
            textBox_obj_tgg_sc.IsEnabled = true;
            btn_ukuran_sc.IsEnabled = true;

            textBox_or_pjg_sc.IsEnabled = false;
            textBox_or_lbr_sc.IsEnabled = false;
            textBox_or_tgg_sc.IsEnabled = false;
            btn_origin_sc.IsEnabled = false;

            textBox_fin_pjg_sc.IsEnabled = false;
            textBox_fin_lbr_sc.IsEnabled = false;
            textBox_fin_tgg_sc.IsEnabled = false;
            btn_final_sc.IsEnabled = false;

            button_sc.IsEnabled = false;
            button_reset_sc.IsEnabled = false;
        }
        private void DrawCartesianAxisforScale()
        {
            var axisBuilder = new MeshBuilder(true, true, true);
            axisBuilder.AddPipe(new Point3D(-100, 0, 0), new Point3D(100, 0, 0), 0, 0.1, 360);
            axisBuilder.AddPipe(new Point3D(0, -100, 0), new Point3D(0, 100, 0), 0, 0.1, 360);
            axisBuilder.AddPipe(new Point3D(0, 0, -100), new Point3D(0, 0, 100), 0, 0.1, 360);

            var mesh = axisBuilder.ToMesh(true);
            var material = MaterialHelper.CreateMaterial(Colors.Black);
            ModelScaling.Children.Add(new GeometryModel3D
            {
                Geometry = mesh,
                Material = material
            });
            ScModelVisual.Content = ModelScaling;
        }
        public void ScalingDrawOrigin()
        {
            var meshBuilder = new MeshBuilder(false, false);
            meshBuilder.AddBox(new Rect3D(ScX[0, 0], ScX[1, 0], ScX[2, 0], Int32.Parse(textBox_obj_pjg_sc.Text), Int32.Parse(textBox_obj_lbr_sc.Text), Int32.Parse(textBox_obj_tgg_sc.Text)));

            var mesh = meshBuilder.ToMesh(true);

            var mgMaterial = MaterialHelper.CreateMaterial(Colors.Magenta);

            ModelScaling.Children.Add(new GeometryModel3D
            {
                Geometry = mesh,
                Material = mgMaterial
            });
        }

        public void ScalingDestination()
        {
            var meshBuilder = new MeshBuilder(false, false);
            meshBuilder.AddBox(new Rect3D(ScX[0, 0], ScX[1, 0], ScX[2, 0], Int32.Parse(textBox_obj_pjg_sc.Text) * ScF[0, 0], Int32.Parse(textBox_obj_lbr_sc.Text) * ScF[1, 1], Int32.Parse(textBox_obj_tgg_sc.Text) * ScF[2, 2]));

            var mesh = meshBuilder.ToMesh(true);

            var olMaterial = MaterialHelper.CreateMaterial(Colors.Olive);

            ModelScaling.Children.Add(new GeometryModel3D
            {
                Geometry = mesh,
                Material = olMaterial
            });
        }
        private void TrObjectSelection(object sender, SelectionChangedEventArgs e)
        {
            txtScPointE.Visibility = Visibility.Visible;
            txtScPointF.Visibility = Visibility.Visible;
            txtScPointG.Visibility = Visibility.Visible;
            txtScPointH.Visibility = Visibility.Visible;
        }
        private void btn_ukuran_sc_Click(object sender, RoutedEventArgs e)
        {
            textBox_or_pjg_sc.IsEnabled = true;
            textBox_or_lbr_sc.IsEnabled = true;
            textBox_or_tgg_sc.IsEnabled = true;
            btn_origin_sc.IsEnabled = true;
            textBox_obj_pjg_sc.IsEnabled = false;
            textBox_obj_lbr_sc.IsEnabled = false;
            textBox_obj_tgg_sc.IsEnabled = false;
            btn_ukuran_sc.IsEnabled = false;
            DeskripsiObjectScale.Text = String.Format("Balok ABCD EFGH yang memiliki panjang {0}, lebar {1}, dan tinggi {2}", textBox_obj_pjg_sc.Text, textBox_obj_lbr_sc.Text, textBox_obj_tgg_sc.Text);
        }
        private void btn_origin_sc_Click(object sender, RoutedEventArgs e)
        {
            textBox_fin_pjg_sc.IsEnabled = true;
            textBox_fin_lbr_sc.IsEnabled = true;
            textBox_fin_tgg_sc.IsEnabled = true;
            btn_final_sc.IsEnabled = true;

            textBox_or_pjg_sc.IsEnabled = false;
            textBox_or_lbr_sc.IsEnabled = false;
            textBox_or_tgg_sc.IsEnabled = false;
            btn_origin_sc.IsEnabled = false;

            ScX[0, 0] = Int32.Parse(textBox_or_pjg_sc.Text);
            ScX[1, 0] = Int32.Parse(textBox_or_lbr_sc.Text);
            ScX[2, 0] = Int32.Parse(textBox_or_tgg_sc.Text);
            ScX[3, 0] = 1;

            ScX[0, 1] = Int32.Parse(textBox_or_pjg_sc.Text) + Int32.Parse(textBox_obj_pjg_sc.Text);
            ScX[1, 1] = Int32.Parse(textBox_or_lbr_sc.Text);
            ScX[2, 1] = Int32.Parse(textBox_or_tgg_sc.Text);
            ScX[3, 1] = 1;

            ScX[0, 2] = Int32.Parse(textBox_or_pjg_sc.Text) + Int32.Parse(textBox_obj_pjg_sc.Text);
            ScX[1, 2] = Int32.Parse(textBox_or_lbr_sc.Text) + Int32.Parse(textBox_obj_lbr_sc.Text);
            ScX[2, 2] = Int32.Parse(textBox_or_tgg_sc.Text);
            ScX[3, 2] = 1;

            ScX[0, 3] = Int32.Parse(textBox_or_pjg_sc.Text);
            ScX[1, 3] = Int32.Parse(textBox_or_lbr_sc.Text) + Int32.Parse(textBox_obj_lbr_sc.Text);
            ScX[2, 3] = Int32.Parse(textBox_or_tgg_sc.Text);
            ScX[3, 3] = 1;

            ScX[0, 4] = Int32.Parse(textBox_or_pjg_sc.Text);
            ScX[1, 4] = Int32.Parse(textBox_or_lbr_sc.Text);
            ScX[2, 4] = Int32.Parse(textBox_or_tgg_sc.Text) + Int32.Parse(textBox_obj_tgg_sc.Text);
            ScX[3, 4] = 1;

            ScX[0, 5] = Int32.Parse(textBox_or_pjg_sc.Text) + Int32.Parse(textBox_obj_pjg_sc.Text);
            ScX[1, 5] = Int32.Parse(textBox_or_lbr_sc.Text);
            ScX[2, 5] = Int32.Parse(textBox_or_tgg_sc.Text) + Int32.Parse(textBox_obj_tgg_sc.Text);
            ScX[3, 5] = 1;

            ScX[0, 6] = Int32.Parse(textBox_or_pjg_sc.Text) + Int32.Parse(textBox_obj_pjg_sc.Text);
            ScX[1, 6] = Int32.Parse(textBox_or_lbr_sc.Text) + Int32.Parse(textBox_obj_lbr_sc.Text);
            ScX[2, 6] = Int32.Parse(textBox_or_tgg_sc.Text) + Int32.Parse(textBox_obj_tgg_sc.Text);
            ScX[3, 6] = 1;

            ScX[0, 7] = Int32.Parse(textBox_or_pjg_sc.Text);
            ScX[1, 7] = Int32.Parse(textBox_or_lbr_sc.Text) + Int32.Parse(textBox_obj_lbr_sc.Text);
            ScX[2, 7] = Int32.Parse(textBox_or_tgg_sc.Text) + Int32.Parse(textBox_obj_tgg_sc.Text);
            ScX[3, 7] = 1;

            txtScPointA.Text = String.Format("A ({0}, {1}, {2})",
                                             ScX[0, 0], ScX[1, 0], ScX[2, 0]);
            txtScPointB.Text = String.Format("B ({0}, {1}, {2})",
                                             ScX[0, 1], ScX[1, 1], ScX[2, 1]);
            txtScPointC.Text = String.Format("C ({0}, {1}, {2})",
                                             ScX[0, 2], ScX[1, 2], ScX[2, 2]);
            txtScPointD.Text = String.Format("D ({0}, {1}, {2})",
                                             ScX[0, 3], ScX[1, 3], ScX[2, 3]);
            txtScPointE.Text = String.Format("E ({0}, {1}, {2})",
                                             ScX[0, 4], ScX[1, 4], ScX[2, 4]);
            txtScPointF.Text = String.Format("F ({0}, {1}, {2})",
                                             ScX[0, 5], ScX[1, 5], ScX[2, 5]);
            txtScPointG.Text = String.Format("G ({0}, {1}, {2})",
                                             ScX[0, 6], ScX[1, 6], ScX[2, 6]);
            txtScPointH.Text = String.Format("H ({0}, {1}, {2})",
                                             ScX[0, 7], ScX[1, 7], ScX[2, 7]);

            ScalingDrawOrigin();
        }
        private void btn_final_sc_Click(object sender, RoutedEventArgs e)
        {
            textBox_fin_pjg_sc.IsEnabled = false;
            textBox_fin_lbr_sc.IsEnabled = false;
            textBox_fin_tgg_sc.IsEnabled = false;
            btn_final_sc.IsEnabled = false;
            button_sc.IsEnabled = true;
            button_reset_sc.IsEnabled = false;

            ScF[0, 0] = Int32.Parse(textBox_fin_pjg_sc.Text);
            ScF[1, 1] = Int32.Parse(textBox_fin_lbr_sc.Text);
            ScF[2, 2] = Int32.Parse(textBox_fin_tgg_sc.Text);
            ScF[3, 3] = 1;

            DeskripsiScale.Text = String.Format(" akan diperbesar dengan perbesaran {0} pada sumbu x, {1} pada sumbu y, dan {2} pada sumbu z", ScF[0, 0], ScF[1, 1], ScF[2, 2]);
        }

        private void button_sc_Click(object sender, RoutedEventArgs e)
        {
            button_sc.IsEnabled = false;
            button_reset_sc.IsEnabled = true;

            MultiplyMatrix(ScY, ScF, ScX);

            txtScMat0.Text = "-> [A' B' C' D' E' F' G' H'] = [S] * [A B C D E F G H]";
            txtScMat1.Text = String.Format("   | {0,3} {1,3} {2,3} {3,3} {4,3} {5,3} {6,3} {7,3} |    | {8,3} {9,3} {10,3} {11,3} |    | {12,3} {13,3} {14,3} {15,3} {16,3} {17,3} {18,3} {19,3} |",
                                           ScY[0, 0], ScY[0, 1], ScY[0, 2], ScY[0, 3], ScY[0, 4], ScY[0, 5], ScY[0, 6], ScY[0, 7],
                                           ScF[0, 0], ScF[0, 1], ScF[0, 2], ScF[0, 3],
                                           ScX[0, 0], ScX[0, 1], ScX[0, 2], ScX[0, 3], ScX[0, 4], ScX[0, 5], ScX[0, 6], ScX[0, 7]);
            txtScMat2.Text = String.Format("   | {0,3} {1,3} {2,3} {3,3} {4,3} {5,3} {6,3} {7,3} | = | {8,3} {9,3} {10,3} {11,3} | * | {12,3} {13,3} {14,3} {15,3} {16,3} {17,3} {18,3} {19,3} |",
                                           ScY[1, 0], ScY[1, 1], ScY[1, 2], ScY[1, 3], ScY[1, 4], ScY[1, 5], ScY[1, 6], ScY[1, 7],
                                           ScF[1, 0], ScF[1, 1], ScF[1, 2], ScF[1, 3],
                                           ScX[1, 0], ScX[1, 1], ScX[1, 2], ScX[1, 3], ScX[1, 4], ScX[1, 5], ScX[1, 6], ScX[1, 7]);
            txtScMat3.Text = String.Format("   | {0,3} {1,3} {2,3} {3,3} {4,3} {5,3} {6,3} {7,3} |    | {8,3} {9,3} {10,3} {11,3} |    | {12,3} {13,3} {14,3} {15,3} {16,3} {17,3} {18,3} {19,3} |",
                                           ScY[2, 0], ScY[2, 1], ScY[2, 2], ScY[2, 3], ScY[2, 4], ScY[2, 5], ScY[2, 6], ScY[2, 7],
                                           ScF[2, 0], ScF[2, 1], ScF[2, 2], ScF[2, 3],
                                           ScX[2, 0], ScX[2, 1], ScX[2, 2], ScX[2, 3], ScX[2, 4], ScX[2, 5], ScX[2, 6], ScX[2, 7]);
            txtScMat4.Text = String.Format("   | {0,3} {1,3} {2,3} {3,3} {4,3} {5,3} {6,3} {7,3} |    | {8,3} {9,3} {10,3} {11,3} |    | {12,3} {13,3} {14,3} {15,3} {16,3} {17,3} {18,3} {19,3} |",
                                           ScY[3, 0], ScY[3, 1], ScY[3, 2], ScY[3, 3], ScY[3, 4], ScY[3, 5], ScY[3, 6], ScY[3, 7],
                                           ScF[3, 0], ScF[3, 1], ScF[3, 2], ScF[3, 3],
                                           ScX[3, 0], ScX[3, 1], ScX[3, 2], ScX[3, 3], ScX[3, 4], ScX[3, 5], ScX[3, 6], ScX[3, 7]);
            GarisPemisah();

            ScalingDestination();
        }

        private void button_reset_sc_Click(object sender, RoutedEventArgs e)
        {
            Matrixnol(ScX);
            Matrixnol(ScF);
            Matrixnol(ScY);
            ScToDisableSomeInput();

            textBox_obj_pjg_sc.Text = "1";
            textBox_obj_lbr_sc.Text = "1";
            textBox_obj_tgg_sc.Text = "1";

            textBox_or_pjg_sc.Text = "0";
            textBox_or_lbr_sc.Text = "0";
            textBox_or_tgg_sc.Text = "0";

            textBox_fin_pjg_sc.Text = "0";
            textBox_fin_lbr_sc.Text = "0";
            textBox_fin_tgg_sc.Text = "0";

            ModelScaling.Children.Clear();
            DrawCartesianAxisforScale();
        }
    }
}
