using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace MEDICENTER
{
    public partial class FormColaPacientes : Form
    {
        private Sistema sistema;
        private string idHospital;
        private ListBox listBoxCola;

        public FormColaPacientes(Sistema sistemaParam, string idHospitalParam)
        {
            sistema = sistemaParam;
            idHospital = idHospitalParam;
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.ClientSize = new Size(800, 600);
            this.Text = "Cola de Pacientes";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(230, 230, 250);

            Label lblTitulo = new Label();
            lblTitulo.Text = "Cola de Pacientes";
            lblTitulo.Font = new Font("Segoe UI", 18, FontStyle.Bold);
            lblTitulo.Location = new Point(280, 20);
            lblTitulo.Size = new Size(240, 40);
            this.Controls.Add(lblTitulo);

            listBoxCola = new ListBox();
            listBoxCola.Location = new Point(50, 80);
            listBoxCola.Size = new Size(700, 450);
            listBoxCola.Font = new Font("Consolas", 10);
            this.Controls.Add(listBoxCola);

            CargarCola();

            Button btnCerrar = new Button();
            btnCerrar.Text = "Cerrar";
            btnCerrar.Font = new Font("Segoe UI", 12);
            btnCerrar.Location = new Point(340, 540);
            btnCerrar.Size = new Size(120, 45);
            btnCerrar.BackColor = Color.White;
            btnCerrar.Click += (s, e) => this.Close();
            this.Controls.Add(btnCerrar);
        }

        private void CargarCola()
        {
            if (!sistema.ColasPorHospital.ContainsKey(idHospital) ||
                sistema.ColasPorHospital[idHospital].Count == 0)
            {
                listBoxCola.Items.Add("No hay pacientes en cola");
                return;
            }

            Queue<string> cola = sistema.ColasPorHospital[idHospital];
            string[] colaArray = cola.ToArray();

            listBoxCola.Items.Add($"Total de pacientes en espera: {colaArray.Length}");
            listBoxCola.Items.Add("═════════════════════════════════════════════════");
            listBoxCola.Items.Add("");

            for (int i = 0; i < colaArray.Length; i++)
            {
                string[] partes = colaArray[i].Split('|');
                string idPaciente = partes[0];
                string idRegistro = partes[1];

                Paciente paciente = sistema.Pacientes.Find(p => p.Id == idPaciente);

                listBoxCola.Items.Add($"{i + 1}. Paciente: {paciente?.Nombre ?? idPaciente}");
                listBoxCola.Items.Add($"   ID: {idPaciente} | Registro: {idRegistro}");
                listBoxCola.Items.Add("");
            }
        }
    }
}