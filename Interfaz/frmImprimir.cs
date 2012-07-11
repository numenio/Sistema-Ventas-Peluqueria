using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Drawing.Printing;
using System.Text;
using System.Windows.Forms;
using ProcesosNegocio;

namespace Interfaz
{
 public partial class frmImprimir : Form
 {
     List<filaParaImprimirVenta> listaImprimir=new List<filaParaImprimirVenta>();

  public frmImprimir(List<filaParaImprimirVenta> listaParaImprimir)
  {
      listaImprimir = listaParaImprimir;
      InitializeComponent();
  }
  // The DataGridView Control which will be printed.
  //DataGridView MyDataGridView = new DataGridView();
  // The PrintDocument to be used for printing.
  //PrintDocument MyPrintDocument = new PrintDocument();
  // The class that will do the printing process.
  DataGridViewPrinter MyDataGridViewPrinter;


  // The printing setup function
  private bool SetupThePrinting()
  {
   PrintDialog MyPrintDialog = new PrintDialog();
   MyPrintDialog.AllowCurrentPage = false;
   MyPrintDialog.AllowPrintToFile = false;
   MyPrintDialog.AllowSelection = false;
   MyPrintDialog.AllowSomePages = false;
   MyPrintDialog.PrintToFile = false;
   MyPrintDialog.ShowHelp = false;
   MyPrintDialog.ShowNetwork = false;

   if (MyPrintDialog.ShowDialog() != DialogResult.OK)
    return false;
   MyPrintDocument.DocumentName = "Planilla de ventas peluquería";
   MyPrintDialog.PrinterSettings.DefaultPageSettings.Landscape = true; //página horizontal
   //MyPrintDialog.PrinterSettings.DefaultPageSettings.Landscape = false; //página vertical
   MyPrintDocument.PrinterSettings = MyPrintDialog.PrinterSettings;
   MyPrintDocument.DefaultPageSettings = MyPrintDialog.PrinterSettings.DefaultPageSettings;
   MyPrintDocument.DefaultPageSettings.Margins = new Margins(40, 40, 40, 40);

   //if (MessageBox.Show("¿Desea centrar el reporte en la pagina?",
   // "InvoiceManager - Center on Page", MessageBoxButtons.YesNo,
   // MessageBoxIcon.Question) == DialogResult.Yes)
   // MyDataGridViewPrinter = new DataGridViewPrinter(MyDataGridView,
   // MyPrintDocument, true, true, "Listado Completo de Facturas", new Font("Tahoma", 15,
   // FontStyle.Bold, GraphicsUnit.Point), Color.Black, true);
   //else
   string encabezado = "Vendedor: Cecilia Toscani             Zona: " + txtZona.Text + "               Fecha: " + DateTime.Now.ToLongDateString();
    MyDataGridViewPrinter = new DataGridViewPrinter(MyDataGridView,
    MyPrintDocument, false, true, encabezado, new Font("Tahoma", 13,
    FontStyle.Bold, GraphicsUnit.Point), Color.Black, true);
   return true;
  }

  // The Print Button
  private void btnPrint_Click(object sender, EventArgs e)
  {
   if (SetupThePrinting()) MyPrintDocument.Print();
   this.Close();
  }

  // The PrintPage action for the PrintDocument control
  private void MyPrintDocument_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
  {
   bool more = MyDataGridViewPrinter.DrawDataGridView(e.Graphics);
   if (more == true)
    e.HasMorePages = true;
  }

  // The Print Preview Button
  private void btnPrintPreview_Click(object sender, EventArgs e)
  {
   if (SetupThePrinting())
   {
    PrintPreviewDialog MyPrintPreviewDialog = new PrintPreviewDialog();
    MyPrintPreviewDialog.Document = MyPrintDocument;
    MyPrintPreviewDialog.ShowDialog();
    this.Close();
   }
  }

  private void TodasFacturas_Load(object sender, EventArgs e)
  {
      

      //List<artículo> misArtículos = artículo.cargarTodos();

      //MyDataGridView.DataSource = listaImprimir; // misArtículos;
      
      foreach (filaParaImprimirVenta fila in listaImprimir)
      {
          MyDataGridView.Rows.Add(fila.Nº,fila.Cliente,fila.Detalle_compra,fila.Debe,fila.Haber,fila.Saldo);
      }

      double altoMáximoCelda = 0;
      for (int i = 0; i < MyDataGridView.Rows.Count; i++)
          if (MyDataGridView.Rows[i].Height > altoMáximoCelda) altoMáximoCelda = MyDataGridView.Rows[i].Height;

   // Changing the last column alignment to be in the Right alignment   
   //MyDataGridView.Columns[MyDataGridView.Columns.Count - 1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
   // Adjusting each column to be fit as the content of all its cells, including the header cell
   //MyDataGridView.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
   
   
    
  }    
 }
}
