using System.Drawing;
using System.Windows.Forms;

namespace MEDICENTER
{
    public partial class FormMisPacientes : Form
    {
        private Sistema sistema;
        private PersonalHospitalario medico;
        private ListBox listBoxPacientes;

        public FormMisPacientes(Sistema sistemaParam, PersonalHospitalario medicoParam)
        {
            sistema = sistemaParam;
            medico = medicoParam;
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.ClientSize = new Size(800, 600);
            this.Text = "Mis Pacientes Asignados";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(230, 230, 250);

            Label lblTitulo = new Label();
            lblTitulo.Text = "Mis Pacientes Asignados";
            lblTitulo.Font = new Font("Segoe UI", 18, FontStyle.Bold);
            lblTitulo.Location = new Point(250, 20);
            lblTitulo.Size = new Size(300, 40);
            this.Controls.Add(lblTitulo);

            listBoxPacientes = new ListBox();
            listBoxPacientes.Location = new Point(50, 80);
            listBoxPacientes.Size = new Size(700, 450);
            listBoxPacientes.Font = new Font("Consolas", 10);
            this.Controls.Add(listBoxPacientes);

            CargarPacientes();

            Button btnCerrar = new Button();
            btnCerrar.Text = "Cerrar";
            btnCerrar.Font = new Font("Segoe UI", 12);
            btnCerrar.Location = new Point(340, 540);
            btnCerrar.Size = new Size(120, 45);
            btnCerrar.BackColor = Color.White;
            btnCerrar.Click += (s, e) => this.Close();
            this.Controls.Add(btnCerrar);
        }

        private void CargarPacientes()
        {
            if (medico.PacientesAsignados.Count == 0)
            {
                listBoxPacientes.Items.Add("No tiene pacientes asignados");
                return;
            }

            listBoxPacientes.Items.Add($"Total de pacientes: {medico.PacientesAsignados.Count}");
            listBoxPacientes.Items.Add("═══════════════════════════════════════════════");
            listBoxPacientes.Items.Add("");

            foreach (string idPaciente in medico.PacientesAsignados)
            {
                Paciente paciente = sistema.Pacientes.Find(p => p.Id == idPaciente);
                if (paciente != null)
                {
                    listBoxPacientes.Items.Add($"[{paciente.Id}] {paciente.Nombre}");
                    listBoxPacientes.Items.Add(
                        $"Edad: {paciente.Edad} | Registros: {paciente.Historial.Count}");
                    listBoxPacientes.Items.Add("");
                }
            }
        }
    }
}