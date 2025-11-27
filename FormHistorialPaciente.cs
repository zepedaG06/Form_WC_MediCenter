using System.Drawing;
using System.Windows.Forms;

namespace MEDICENTER
{
    public partial class FormHistorialPaciente : Form
    {
        private Paciente paciente;
        private ListBox listBoxHistorial;

        public FormHistorialPaciente(Paciente pacienteParam)
        {
            paciente = pacienteParam;
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.ClientSize = new Size(900, 700);
            this.Text = "Historial Médico";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(230, 230, 250);

            Label lblTitulo = new Label();
            lblTitulo.Text = "Historial Médico";
            lblTitulo.Font = new Font("Segoe UI", 20, FontStyle.Bold);
            lblTitulo.Location = new Point(320, 20);
            lblTitulo.Size = new Size(260, 40);
            this.Controls.Add(lblTitulo);

            if (paciente.Historial.Count == 0)
            {
                Label lblSinRegistros = new Label();
                lblSinRegistros.Text = "No hay registros médicos";
                lblSinRegistros.Font = new Font("Segoe UI", 14);
                lblSinRegistros.Location = new Point(300, 300);
                lblSinRegistros.Size = new Size(300, 40);
                lblSinRegistros.TextAlign = ContentAlignment.MiddleCenter;
                this.Controls.Add(lblSinRegistros);
            }
            else
            {
                listBoxHistorial = new ListBox();
                listBoxHistorial.Location = new Point(50, 80);
                listBoxHistorial.Size = new Size(800, 500);
                listBoxHistorial.Font = new Font("Consolas", 10);
                listBoxHistorial.SelectionMode = SelectionMode.One;

                foreach (var registro in paciente.Historial)
                {
                    listBoxHistorial.Items.Add("──────────────────────────────────────────────");
                    listBoxHistorial.Items.Add($"ID Registro: {registro.IdRegistro}");
                    listBoxHistorial.Items.Add($"Fecha: {registro.Fecha:dd/MM/yyyy HH:mm}");
                    listBoxHistorial.Items.Add($"Hospital: {registro.IdHospital}");
                    listBoxHistorial.Items.Add($"Síntomas: {string.Join(", ", registro.Sintomas)}");
                    listBoxHistorial.Items.Add($"Diagnóstico: {registro.Diagnostico}");
                    if (!string.IsNullOrEmpty(registro.Tratamiento))
                        listBoxHistorial.Items.Add($"Tratamiento: {registro.Tratamiento}");
                    listBoxHistorial.Items.Add(
                        $"Estado: {(registro.Confirmado ? "Confirmado" : "Pendiente")}");
                    if (!string.IsNullOrEmpty(registro.IdMedico))
                        listBoxHistorial.Items.Add($"Médico: {registro.IdMedico}");
                    if (!string.IsNullOrEmpty(registro.ObservacionDoctor))
                        listBoxHistorial.Items.Add($"Observaciones: {registro.ObservacionDoctor}");
                    listBoxHistorial.Items.Add("");
                }

                this.Controls.Add(listBoxHistorial);
            }

            Button btnCerrar = new Button();
            btnCerrar.Text = "Cerrar";
            btnCerrar.Font = new Font("Segoe UI", 12);
            btnCerrar.Location = new Point(390, 610);
            btnCerrar.Size = new Size(120, 45);
            btnCerrar.BackColor = Color.White;
            btnCerrar.Click += (s, e) => this.Close();
            this.Controls.Add(btnCerrar);
        }
    }
}