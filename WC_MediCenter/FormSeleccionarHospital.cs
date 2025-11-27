using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace MEDICENTER
{
    public partial class FormSeleccionarHospital : Form
    {
        private Sistema sistema;
        private Paciente paciente;
        private ListView listViewHospitales;

        public FormSeleccionarHospital(Sistema sistemaParam, Paciente pacienteParam)
        {
            sistema = sistemaParam;
            paciente = pacienteParam;
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.ClientSize = new Size(900, 700);
            this.Text = "Seleccionar Hospital";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(230, 230, 250);

            Label lblTitulo = new Label();
            lblTitulo.Text = "Seleccionar Hospital";
            lblTitulo.Font = new Font("Segoe UI", 20, FontStyle.Bold);
            lblTitulo.Location = new Point(300, 20);
            lblTitulo.Size = new Size(300, 40);
            this.Controls.Add(lblTitulo);

            Label lblDisponibles = new Label();
            lblDisponibles.Text = "Hospitales Disponibles (según su seguro):";
            lblDisponibles.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            lblDisponibles.Location = new Point(50, 80);
            lblDisponibles.Size = new Size(400, 30);
            this.Controls.Add(lblDisponibles);

            listViewHospitales = new ListView();
            listViewHospitales.Location = new Point(50, 120);
            listViewHospitales.Size = new Size(800, 450);
            listViewHospitales.View = View.Details;
            listViewHospitales.FullRowSelect = true;
            listViewHospitales.GridLines = true;
            listViewHospitales.Font = new Font("Segoe UI", 10);

            listViewHospitales.Columns.Add("ID", 80);
            listViewHospitales.Columns.Add("Hospital", 300);
            listViewHospitales.Columns.Add("Tipo", 120);
            listViewHospitales.Columns.Add("Precisión", 100);
            listViewHospitales.Columns.Add("Tiempo", 100);
            listViewHospitales.Columns.Add("Costo", 100);

            CargarHospitales();
            this.Controls.Add(listViewHospitales);

            Button btnSeleccionar = new Button();
            btnSeleccionar.Text = "Seleccionar y Continuar";
            btnSeleccionar.Font = new Font("Segoe UI", 12);
            btnSeleccionar.Location = new Point(300, 600);
            btnSeleccionar.Size = new Size(200, 45);
            btnSeleccionar.BackColor = Color.White;
            btnSeleccionar.Click += BtnSeleccionar_Click;
            this.Controls.Add(btnSeleccionar);

            Button btnCancelar = new Button();
            btnCancelar.Text = "Cancelar";
            btnCancelar.Font = new Font("Segoe UI", 12);
            btnCancelar.Location = new Point(520, 600);
            btnCancelar.Size = new Size(120, 45);
            btnCancelar.BackColor = Color.White;
            btnCancelar.Click += (s, e) => this.Close();
            this.Controls.Add(btnCancelar);
        }

        private void CargarHospitales()
        {
            List<Hospital> disponibles = sistema.ObtenerHospitalesDisponibles(paciente.TipoSeguro);

            foreach (var hospital in disponibles)
            {
                ListViewItem item = new ListViewItem(hospital.Id);
                item.SubItems.Add(hospital.Nombre);
                item.SubItems.Add(hospital.EsPublico ? "Público" : "Privado");
                item.SubItems.Add(hospital.PrecisionDiagnostico + "%");
                item.SubItems.Add(hospital.TiempoPromedioMin + " min");
                item.SubItems.Add(hospital.EsPublico ? "Gratis" :
                    "$" + hospital.CostoConsulta.ToString("F2"));
                item.Tag = hospital;
                listViewHospitales.Items.Add(item);
            }

            if (paciente.TipoSeguro != TipoSeguro.SeguroCompleto)
            {
                List<Hospital> privados = sistema.ObtenerHospitalesPrivados();
                foreach (var hospital in privados)
                {
                    if (!disponibles.Contains(hospital))
                    {
                        ListViewItem item = new ListViewItem(hospital.Id);
                        item.SubItems.Add(hospital.Nombre + " (Requiere pago)");
                        item.SubItems.Add("Privado");
                        item.SubItems.Add(hospital.PrecisionDiagnostico + "%");
                        item.SubItems.Add(hospital.TiempoPromedioMin + " min");
                        item.SubItems.Add("$" + hospital.CostoConsulta.ToString("F2"));
                        item.Tag = hospital;
                        item.BackColor = Color.LightYellow;
                        listViewHospitales.Items.Add(item);
                    }
                }
            }
        }

        private void BtnSeleccionar_Click(object sender, System.EventArgs e)
        {
            if (listViewHospitales.SelectedItems.Count == 0)
            {
                MessageBox.Show("Por favor seleccione un hospital", "Selección requerida",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Hospital hospitalSeleccionado = (Hospital)listViewHospitales.SelectedItems[0].Tag;

            List<Hospital> disponibles = sistema.ObtenerHospitalesDisponibles(paciente.TipoSeguro);
            bool requierePago = !disponibles.Contains(hospitalSeleccionado);

            if (requierePago)
            {
                DialogResult result = MessageBox.Show(
                    $"Este hospital requiere pago de consulta: ${hospitalSeleccionado.CostoConsulta:F2}\n\n¿Desea continuar?",
                    "Confirmación de pago",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result != DialogResult.Yes)
                    return;
            }

            FormIngresarSintomas formSintomas =
                new FormIngresarSintomas(sistema, paciente, hospitalSeleccionado);
            this.Hide();
            formSintomas.ShowDialog();
            this.Close();
        }
    }
}