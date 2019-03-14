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
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        Model3DGroup translationModel = new Model3DGroup();
        private int[,] TrX = new int[4, 8];
        private int[,] TrF = new int[4, 4];
        private int[,] TrY = new int[4, 8];
        public Window1()
        {
            InitializeComponent();

            Matrixnol(TrX);
            Matrixnol(TrF);
            Matrixnol(TrY);
            TrDisableSomeInput();
            TrDrawCartesianAxis();
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
        private void TrDisableSomeInput()
        {
            textBox_obj_pjg_ts.IsEnabled = true;
            textBox_obj_lbr_ts.IsEnabled = true;
            textBox_obj_tgg_ts.IsEnabled = true;
            btn_ukuran_ts.IsEnabled = true;

            textBox_or_pjg_ts.IsEnabled = false;
            textBox_or_lbr_ts.IsEnabled = false;
            textBox_or_tgg_ts.IsEnabled = false;
            btn_origin_ts.IsEnabled = false;

            textBox_fin_pjg_ts.IsEnabled = false;
            textBox_fin_lbr_ts.IsEnabled = false;
            textBox_fin_tgg_ts.IsEnabled = false;
            btn_final_ts.IsEnabled = false;

            button_tr.IsEnabled = false;
            button_tr_reset.IsEnabled = false;
        }
        private void TrDrawCartesianAxis()
        {
            var axisBuilder = new MeshBuilder(true, true, true);
            axisBuilder.AddPipe(new Point3D(-100, 0, 0), new Point3D(100, 0, 0), 0, 0.1, 360);
            axisBuilder.AddPipe(new Point3D(0, -100, 0), new Point3D(0, 100, 0), 0, 0.1, 360);
            axisBuilder.AddPipe(new Point3D(0, 0, -100), new Point3D(0, 0, 100), 0, 0.1, 360);

            var mesh = axisBuilder.ToMesh(true);
            var material = MaterialHelper.CreateMaterial(Colors.Black);
            translationModel.Children.Add(new GeometryModel3D
            {
                Geometry = mesh,
                Material = material
            });
            TrModelVisual.Content = translationModel;
        }
        public void TranslationDrawOrigin()
        {
            var meshBuilder = new MeshBuilder(false, false);
            meshBuilder.AddBox(new Rect3D(TrX[0, 0], TrX[1, 0], TrX[2, 0], Int32.Parse(textBox_obj_pjg_ts.Text), Int32.Parse(textBox_obj_lbr_ts.Text), Int32.Parse(textBox_obj_tgg_ts.Text)));

            var mesh = meshBuilder.ToMesh(true);

            var mgMaterial = MaterialHelper.CreateMaterial(Colors.Magenta);

            translationModel.Children.Add(new GeometryModel3D
            {
                Geometry = mesh,
                Material = mgMaterial
            });
        }

        public void TranslationDestination()
        {
            var meshBuilder = new MeshBuilder(false, false);
            meshBuilder.AddBox(new Rect3D(TrY[0, 0], TrY[1, 0], TrY[2, 0], Int32.Parse(textBox_obj_pjg_ts.Text), Int32.Parse(textBox_obj_lbr_ts.Text), Int32.Parse(textBox_obj_tgg_ts.Text)));

            var mesh = meshBuilder.ToMesh(true);

            var olMaterial = MaterialHelper.CreateMaterial(Colors.Olive);

            translationModel.Children.Add(new GeometryModel3D
            {
                Geometry = mesh,
                Material = olMaterial
            });
        }

        private void GarisPemisah()
        {
            txtTrLine.Text = String.Empty;

                for (int i = 0; i < txtTrMat1.Text.Length; i++)
                {
                    if (i == 0 || i == txtTrMat1.Text.Length - 1)
                    {
                        txtTrLine.Text += "^";
                    }
                    else
                    {
                        txtTrLine.Text += "*";
                    }
                }
            }
        private void TrObjectSelection(object sender, SelectionChangedEventArgs e)
        {
            txtTrPointE.Visibility = Visibility.Visible;
            txtTrPointF.Visibility = Visibility.Visible;
            txtTrPointG.Visibility = Visibility.Visible;
            txtTrPointH.Visibility = Visibility.Visible;
        }
        private void btn_ukuran_ts_Click(object sender, RoutedEventArgs e)
        {
            textBox_or_pjg_ts.IsEnabled = true;
            textBox_or_lbr_ts.IsEnabled = true;
            textBox_or_tgg_ts.IsEnabled = true;
            btn_origin_ts.IsEnabled = true;

            textBox_obj_pjg_ts.IsEnabled = false;
            textBox_obj_lbr_ts.IsEnabled = false;
            textBox_obj_tgg_ts.IsEnabled = false;
            btn_ukuran_ts.IsEnabled = false;

            DeskripsiObjectTrans.Text = String.Format("Balok ABCD EFGH yang memiliki panjang {0}, lebar {1}, dan tinggi {2}", textBox_obj_pjg_ts.Text, textBox_obj_lbr_ts.Text, textBox_obj_tgg_ts.Text);
        }
        private void btn_origin_ts_Click(object sender, RoutedEventArgs e)
        {
            textBox_fin_pjg_ts.IsEnabled = true;
            textBox_fin_lbr_ts.IsEnabled = true;
            textBox_fin_tgg_ts.IsEnabled = true;
            btn_final_ts.IsEnabled = true;

            textBox_or_pjg_ts.IsEnabled = false;
            textBox_or_lbr_ts.IsEnabled = false;
            textBox_or_tgg_ts.IsEnabled = false;
            btn_origin_ts.IsEnabled = false;

            TrX[0, 0] = Int32.Parse(textBox_or_pjg_ts.Text);
            TrX[1, 0] = Int32.Parse(textBox_or_lbr_ts.Text);
            TrX[2, 0] = Int32.Parse(textBox_or_tgg_ts.Text);
            TrX[3, 0] = 1;

            TrX[0, 1] = Int32.Parse(textBox_or_pjg_ts.Text) + Int32.Parse(textBox_obj_pjg_ts.Text);
            TrX[1, 1] = Int32.Parse(textBox_or_lbr_ts.Text);
            TrX[2, 1] = Int32.Parse(textBox_or_tgg_ts.Text);
            TrX[3, 1] = 1;

            TrX[0, 2] = Int32.Parse(textBox_or_pjg_ts.Text) + Int32.Parse(textBox_obj_pjg_ts.Text);
            TrX[1, 2] = Int32.Parse(textBox_or_lbr_ts.Text) + Int32.Parse(textBox_obj_lbr_ts.Text);
            TrX[2, 2] = Int32.Parse(textBox_or_tgg_ts.Text);
            TrX[3, 2] = 1;

            TrX[0, 3] = Int32.Parse(textBox_or_pjg_ts.Text);
            TrX[1, 3] = Int32.Parse(textBox_or_lbr_ts.Text) + Int32.Parse(textBox_obj_lbr_ts.Text);
            TrX[2, 3] = Int32.Parse(textBox_or_tgg_ts.Text);
            TrX[3, 3] = 1;

            TrX[0, 4] = Int32.Parse(textBox_or_pjg_ts.Text);
            TrX[1, 4] = Int32.Parse(textBox_or_lbr_ts.Text);
            TrX[2, 4] = Int32.Parse(textBox_or_tgg_ts.Text) + Int32.Parse(textBox_obj_tgg_ts.Text);
            TrX[3, 4] = 1;

            TrX[0, 5] = Int32.Parse(textBox_or_pjg_ts.Text) + Int32.Parse(textBox_obj_pjg_ts.Text);
            TrX[1, 5] = Int32.Parse(textBox_or_lbr_ts.Text);
            TrX[2, 5] = Int32.Parse(textBox_or_tgg_ts.Text) + Int32.Parse(textBox_obj_tgg_ts.Text);
            TrX[3, 5] = 1;

            TrX[0, 6] = Int32.Parse(textBox_or_pjg_ts.Text) + Int32.Parse(textBox_obj_pjg_ts.Text);
            TrX[1, 6] = Int32.Parse(textBox_or_lbr_ts.Text) + Int32.Parse(textBox_obj_lbr_ts.Text);
            TrX[2, 6] = Int32.Parse(textBox_or_tgg_ts.Text) + Int32.Parse(textBox_obj_tgg_ts.Text);
            TrX[3, 6] = 1;

            TrX[0, 7] = Int32.Parse(textBox_or_pjg_ts.Text);
            TrX[1, 7] = Int32.Parse(textBox_or_lbr_ts.Text) + Int32.Parse(textBox_obj_lbr_ts.Text);
            TrX[2, 7] = Int32.Parse(textBox_or_tgg_ts.Text) + Int32.Parse(textBox_obj_tgg_ts.Text);
            TrX[3, 7] = 1;

            txtTrPointA.Text = String.Format("A ({0}, {1}, {2})",
                                             TrX[0, 0], TrX[1, 0], TrX[2, 0]);
            txtTrPointB.Text = String.Format("B ({0}, {1}, {2})",
                                             TrX[0, 1], TrX[1, 1], TrX[2, 1]);
            txtTrPointC.Text = String.Format("C ({0}, {1}, {2})",
                                             TrX[0, 2], TrX[1, 2], TrX[2, 2]);
            txtTrPointD.Text = String.Format("D ({0}, {1}, {2})",
                                             TrX[0, 3], TrX[1, 3], TrX[2, 3]);
            txtTrPointE.Text = String.Format("E ({0}, {1}, {2})",
                                             TrX[0, 4], TrX[1, 4], TrX[2, 4]);
            txtTrPointF.Text = String.Format("F ({0}, {1}, {2})",
                                             TrX[0, 5], TrX[1, 5], TrX[2, 5]);
            txtTrPointG.Text = String.Format("G ({0}, {1}, {2})",
                                             TrX[0, 6], TrX[1, 6], TrX[2, 6]);
            txtTrPointH.Text = String.Format("H ({0}, {1}, {2})",
                                             TrX[0, 7], TrX[1, 7], TrX[2, 7]);
            TranslationDrawOrigin();
        }

        private void btn_final_ts_Click(object sender, RoutedEventArgs e)
        {
            textBox_fin_pjg_ts.IsEnabled = false;
            textBox_fin_lbr_ts.IsEnabled = false;
            textBox_fin_tgg_ts.IsEnabled = false;
            btn_final_ts.IsEnabled = false;
            button_tr.IsEnabled = true;
            button_tr_reset.IsEnabled = true;

            TrF[0, 0] = 1;
            TrF[1, 1] = 1;
            TrF[2, 2] = 1;
            TrF[3, 3] = 1;

            TrF[0, 3] = Int32.Parse(textBox_fin_pjg_ts.Text);
            TrF[1, 3] = Int32.Parse(textBox_fin_lbr_ts.Text);
            TrF[2, 3] = Int32.Parse(textBox_fin_tgg_ts.Text);

            DeskripsiTrans.Text = String.Format(" akan ditranslasi sejauh {0} pada sumbu x, {1} pada sumbu y, dan {2} pada sumbu z",TrF[0, 3], TrF[1, 3], TrF[2, 3]);
        }

        private void button_tr_Click(object sender, RoutedEventArgs e)
        {
            button_tr.IsEnabled = false;
            button_tr_reset.IsEnabled = true;

            MultiplyMatrix(TrY, TrF, TrX);

            txtTrMat0.Text = "-> [A' B' C' D' E' F' G' H'] = [T] * [A B C D E F G H]";
            txtTrMat1.Text = String.Format("   | {0,3} {1,3} {2,3} {3,3} {4,3} {5,3} {6,3} {7,3} |    | {8,3} {9,3} {10,3} {11,3} |   | {12,3} {13,3} {14,3} {15,3} {16,3} {17,3} {18,3} {19,3} |",
                                           TrY[0, 0], TrY[0, 1], TrY[0, 2], TrY[0, 3], TrY[0, 4], TrY[0, 5], TrY[0, 6], TrY[0, 7],
                                           TrF[0, 0], TrF[0, 1], TrF[0, 2], TrF[0, 3],
                                           TrX[0, 0], TrX[0, 1], TrX[0, 2], TrX[0, 3], TrX[0, 4], TrX[0, 5], TrX[0, 6], TrX[0, 7]);
            txtTrMat2.Text = String.Format("   | {0,3} {1,3} {2,3} {3,3} {4,3} {5,3} {6,3} {7,3} | = | {8,3} {9,3} {10,3} {11,3} | * | {12,3} {13,3} {14,3} {15,3} {16,3} {17,3} {18,3} {19,3} |",
                                           TrY[1, 0], TrY[1, 1], TrY[1, 2], TrY[1, 3], TrY[1, 4], TrY[1, 5], TrY[1, 6], TrY[1, 7],
                                           TrF[1, 0], TrF[1, 1], TrF[1, 2], TrF[1, 3],
                                           TrX[1, 0], TrX[1, 1], TrX[1, 2], TrX[1, 3], TrX[1, 4], TrX[1, 5], TrX[1, 6], TrX[1, 7]);
            txtTrMat3.Text = String.Format("   | {0,3} {1,3} {2,3} {3,3} {4,3} {5,3} {6,3} {7,3} |    | {8,3} {9,3} {10,3} {11,3} |   | {12,3} {13,3} {14,3} {15,3} {16,3} {17,3} {18,3} {19,3} |",
                                           TrY[2, 0], TrY[2, 1], TrY[2, 2], TrY[2, 3], TrY[2, 4], TrY[2, 5], TrY[2, 6], TrY[2, 7],
                                           TrF[2, 0], TrF[2, 1], TrF[2, 2], TrF[2, 3],
                                           TrX[2, 0], TrX[2, 1], TrX[2, 2], TrX[2, 3], TrX[2, 4], TrX[2, 5], TrX[2, 6], TrX[2, 7]);
            txtTrMat4.Text = String.Format("   | {0,3} {1,3} {2,3} {3,3} {4,3} {5,3} {6,3} {7,3} |    | {8,3} {9,3} {10,3} {11,3} |   | {12,3} {13,3} {14,3} {15,3} {16,3} {17,3} {18,3} {19,3} |",
                                           TrY[3, 0], TrY[3, 1], TrY[3, 2], TrY[3, 3], TrY[3, 4], TrY[3, 5], TrY[3, 6], TrY[3, 7],
                                           TrF[3, 0], TrF[3, 1], TrF[3, 2], TrF[3, 3],
                                           TrX[3, 0], TrX[3, 1], TrX[3, 2], TrX[3, 3], TrX[3, 4], TrX[3, 5], TrX[3, 6], TrX[3, 7]);
            GarisPemisah();

            TranslationDestination();
        }

        private void button_tr_reset_Click(object sender, RoutedEventArgs e)
        {
            Matrixnol(TrX);
            Matrixnol(TrF);
            Matrixnol(TrY);
            TrDisableSomeInput();

            textBox_obj_pjg_ts.Text = "1";
            textBox_obj_lbr_ts.Text = "1";
            textBox_obj_tgg_ts.Text = "1";

            textBox_or_pjg_ts.Text = "0";
            textBox_or_lbr_ts.Text = "0";
            textBox_or_tgg_ts.Text = "0";

            textBox_fin_pjg_ts.Text = "0";
            textBox_fin_lbr_ts.Text = "0";
            textBox_fin_tgg_ts.Text = "0";

            translationModel.Children.Clear();
            TrDrawCartesianAxis();
        }
    }
}
