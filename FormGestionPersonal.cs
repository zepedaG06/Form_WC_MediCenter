using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace MEDICENTER
{
    public partial class FormGestionPersonal : Form
    {
        private Sistema sistema;
        private PersonalHospitalario admin;
        private ListBox listBoxPersonal;

        public FormGestionPersonal(Sistema sistemaParam, PersonalHospitalario adminParam)
        {
            sistema = sistemaParam;
            admin = adminParam;
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.ClientSize = new Size(900, 700);
            this.Text = "Gestión de Personal del Hospital";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(230, 230, 250);

            Hospital hospital = sistema.BuscarHospital(admin.IdHospital);

            Label lblTitulo = new Label();
            lblTitulo.Text = "Gestión de Personal del Hospital";
            lblTitulo.Font = new Font("Segoe UI", 18, FontStyle.Bold);
            lblTitulo.Location = new Point(250, 20);
            lblTitulo.Size = new Size(400, 40);
            this.Controls.Add(lblTitulo);

            Label lblHospital = new Label();
            lblHospital.Text = $"Hospital: {hospital.Nombre}";
            lblHospital.Font = new Font("Segoe UI", 12);
            lblHospital.Location = new Point(50, 70);
            lblHospital.Size = new Size(600, 30);
            this.Controls.Add(lblHospital);

            listBoxPersonal = new ListBox();
            listBoxPersonal.Location = new Point(50, 110);
            listBoxPersonal.Size = new Size(800, 500);
            listBoxPersonal.Font = new Font("Consolas", 10);
            this.Controls.Add(listBoxPersonal);

            CargarPersonal();

            Button btnCerrar = new Button();
            btnCerrar.Text = "Cerrar";
            btnCerrar.Font = new Font("Segoe UI", 12);
            btnCerrar.Location = new Point(390, 630);
            btnCerrar.Size = new Size(120, 45);
            btnCerrar.BackColor = Color.White;
            btnCerrar.Click += (s, e) => this.Close();
            this.Controls.Add(btnCerrar);
        }

        private void CargarPersonal()
        {
            List<PersonalHospitalario> personalHospital = sistema.Personal.FindAll(
                p => p.IdHospital == admin.IdHospital);

            if (personalHospital.Count == 0)
            {
                listBoxPersonal.Items.Add("No hay personal registrado en este hospital");
                return;
            }

            listBoxPersonal.Items.Add("═══════════════════════════════════════════════════════════════");
            listBoxPersonal.Items.Add("                  PERSONAL DEL HOSPITAL");
            listBoxPersonal.Items.Add("═══════════════════════════════════════════════════════════════");
            listBoxPersonal.Items.Add("");

            int contador = 1;
            foreach (var p in personalHospital)
            {
                listBoxPersonal.Items.Add($"{contador}. [{p.Id}] {p.Nombre}");
                listBoxPersonal.Items.Add($"   Nivel: {p.NivelAcceso}");
                if (!string.IsNullOrEmpty(p.Especialidad))
                    listBoxPersonal.Items.Add($"   Especialidad: {p.Especialidad}");
                listBoxPersonal.Items.Add($"   Pacientes asignados: {p.PacientesAsignados.Count}");
                listBoxPersonal.Items.Add("");
                contador++;
            }
        }
    }
}