using System.Drawing;
using System.Windows.Forms;

namespace MEDICENTER
{
    public class FormInfoPacientesDoctores : Form
    {
        private readonly Sistema sistema;

        public FormInfoPacientesDoctores(Sistema sistemaParam)
        {
            sistema = sistemaParam;
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.ClientSize = new Size(900, 600);
            this.Text = "Información de Pacientes y Doctores";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(230, 230, 250);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Icon = SystemIcons.Application;

            TabControl tabs = new TabControl();
            tabs.Location = new Point(20, 20);
            tabs.Size = new Size(860, 540);
            this.Controls.Add(tabs);

            TabPage tabPacientes = new TabPage("Pacientes");
            TabPage tabDoctores = new TabPage("Personal Médico");
            tabs.TabPages.Add(tabPacientes);
            tabs.TabPages.Add(tabDoctores);

            // Grid de pacientes
            DataGridView dgvPacientes = new DataGridView();
            dgvPacientes.Dock = DockStyle.Fill;
            dgvPacientes.ReadOnly = true;
            dgvPacientes.AllowUserToAddRows = false;
            dgvPacientes.AllowUserToDeleteRows = false;
            dgvPacientes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvPacientes.Columns.Add("Id", "ID");
            dgvPacientes.Columns.Add("Nombre", "Nombre");
            dgvPacientes.Columns.Add("Email", "Email");
            dgvPacientes.Columns.Add("Edad", "Edad");
            dgvPacientes.Columns.Add("Genero", "Género");
            dgvPacientes.Columns.Add("Registros", "Registros");

            foreach (var p in sistema.Pacientes)
            {
                dgvPacientes.Rows.Add(p.Id, p.Nombre, p.Email, p.Edad, p.Genero, p.Historial.Count);
            }

            tabPacientes.Controls.Add(dgvPacientes);

            // Grid de doctores/personal
            DataGridView dgvDoctores = new DataGridView();
            dgvDoctores.Dock = DockStyle.Fill;
            dgvDoctores.ReadOnly = true;
            dgvDoctores.AllowUserToAddRows = false;
            dgvDoctores.AllowUserToDeleteRows = false;
            dgvDoctores.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvDoctores.Columns.Add("Id", "ID");
            dgvDoctores.Columns.Add("Nombre", "Nombre");
            dgvDoctores.Columns.Add("Email", "Email");
            dgvDoctores.Columns.Add("Nivel", "Nivel");
            dgvDoctores.Columns.Add("Hospital", "Hospital");
            dgvDoctores.Columns.Add("Pacientes", "Pacientes asignados");

            foreach (var d in sistema.Personal)
            {
                dgvDoctores.Rows.Add(d.Id, d.Nombre, d.Email, d.NivelAcceso, d.IdHospital, d.PacientesAsignados.Count);
            }

            tabDoctores.Controls.Add(dgvDoctores);
        }
    }
}