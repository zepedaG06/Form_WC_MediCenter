using System;
using System.Drawing;
using System.Windows.Forms;

namespace MEDICENTER
{
    public partial class FormIngresarSintomas : Form
    {
        private Sistema sistema;
        private Paciente paciente;
        private Hospital hospital;
        private CheckedListBox checkedListSintomas;

        public FormIngresarSintomas(Sistema sistemaParam, Paciente pacienteParam, Hospital hospitalParam)
        {
            sistema = sistemaParam;
            paciente = pacienteParam;
            hospital = hospitalParam;
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.ClientSize = new Size(800, 700);
            this.Text = "Ingresar Síntomas";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(230, 230, 250);

            Label lblTitulo = new Label();
            lblTitulo.Text = "Ingresar Síntomas";
            lblTitulo.Font = new Font("Segoe UI", 20, FontStyle.Bold);
            lblTitulo.Location = new Point(280, 20);
            lblTitulo.Size = new Size(250, 40);
            this.Controls.Add(lblTitulo);

            Label lblHospital = new Label();
            lblHospital.Text = $"Hospital: {hospital.Nombre}";
            lblHospital.Font = new Font("Segoe UI", 12);
            lblHospital.Location = new Point(50, 70);
            lblHospital.Size = new Size(700, 30);
            this.Controls.Add(lblHospital);

            Label lblInstruccion = new Label();
            lblInstruccion.Text = "Seleccione los síntomas que presenta:";
            lblInstruccion.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            lblInstruccion.Location = new Point(50, 110);
            lblInstruccion.Size = new Size(400, 30);
            this.Controls.Add(lblInstruccion);

            checkedListSintomas = new CheckedListBox();
            checkedListSintomas.Location = new Point(50, 150);
            checkedListSintomas.Size = new Size(700, 400);
            checkedListSintomas.Font = new Font("Segoe UI", 11);
            checkedListSintomas.CheckOnClick = true;

            string[] sintomas = {
                "Fiebre", "Tos", "Dolor de cabeza", "Dolor de garganta",
                "Fatiga", "Náuseas", "Dolor abdominal", "Dificultad para respirar",
                "Mareos", "Dolor muscular"
            };

            checkedListSintomas.Items.AddRange(sintomas);
            this.Controls.Add(checkedListSintomas);

            Button btnContinuar = new Button();
            btnContinuar.Text = "Continuar al Diagnóstico";
            btnContinuar.Font = new Font("Segoe UI", 12);
            btnContinuar.Location = new Point(250, 580);
            btnContinuar.Size = new Size(220, 50);
            btnContinuar.BackColor = Color.White;
            btnContinuar.Click += BtnContinuar_Click;
            this.Controls.Add(btnContinuar);

            Button btnCancelar = new Button();
            btnCancelar.Text = "Cancelar";
            btnCancelar.Font = new Font("Segoe UI", 12);
            btnCancelar.Location = new Point(490, 580);
            btnCancelar.Size = new Size(120, 50);
            btnCancelar.BackColor = Color.White;
            btnCancelar.Click += (s, e) => this.Close();
            this.Controls.Add(btnCancelar);
        }

        private void BtnContinuar_Click(object sender, EventArgs e)
        {
            if (checkedListSintomas.CheckedItems.Count == 0)
            {
                MessageBox.Show("Debe seleccionar al menos un síntoma", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            RegistroMedico nuevoRegistro = new RegistroMedico();
            nuevoRegistro.IdRegistro = sistema.GenerarIdRegistro();
            nuevoRegistro.IdPaciente = paciente.Id;
            nuevoRegistro.IdHospital = hospital.Id;

            foreach (object item in checkedListSintomas.CheckedItems)
            {
                nuevoRegistro.Sintomas.Add(item.ToString());
            }

            FormDiagnostico formDiagnostico =
                new FormDiagnostico(sistema, paciente, hospital, nuevoRegistro);
            this.Hide();
            formDiagnostico.ShowDialog();
            this.Close();
        }
    }
}