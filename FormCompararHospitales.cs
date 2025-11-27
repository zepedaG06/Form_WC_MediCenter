using System.Drawing;
using System.Windows.Forms;

namespace MEDICENTER
{
    public partial class FormCompararHospitales : Form
    {
        private Sistema sistema;
        private DataGridView dgvHospitales;

        public FormCompararHospitales(Sistema sistemaParam)
        {
            sistema = sistemaParam;
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.ClientSize = new Size(1000, 600);
            this.Text = "Comparación de Hospitales";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(230, 230, 250);

            Label lblTitulo = new Label();
            lblTitulo.Text = "Comparación de Hospitales";
            lblTitulo.Font = new Font("Segoe UI", 20, FontStyle.Bold);
            lblTitulo.Location = new Point(320, 20);
            lblTitulo.Size = new Size(360, 40);
            this.Controls.Add(lblTitulo);

            dgvHospitales = new DataGridView();
            dgvHospitales.Location = new Point(50, 80);
            dgvHospitales.Size = new Size(900, 420);
            dgvHospitales.ReadOnly = true;
            dgvHospitales.AllowUserToAddRows = false;
            dgvHospitales.AllowUserToDeleteRows = false;
            dgvHospitales.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvHospitales.Font = new Font("Segoe UI", 10);

            dgvHospitales.Columns.Add("ID", "ID");
            dgvHospitales.Columns.Add("Hospital", "Hospital");
            dgvHospitales.Columns.Add("Tipo", "Tipo");
            dgvHospitales.Columns.Add("Personal", "Personal");
            dgvHospitales.Columns.Add("Pacientes", "Pacientes");

            foreach (var hospital in sistema.Hospitales)
            {
                dgvHospitales.Rows.Add(
                    hospital.Id,
                    hospital.Nombre,
                    hospital.EsPublico ? "Público" : "Privado",
                    hospital.PersonalIds.Count,
                    hospital.PacientesAtendidos.Count
                );
            }

            this.Controls.Add(dgvHospitales);

            Button btnCerrar = new Button();
            btnCerrar.Text = "Cerrar";
            btnCerrar.Font = new Font("Segoe UI", 12);
            btnCerrar.Location = new Point(440, 520);
            btnCerrar.Size = new Size(120, 45);
            btnCerrar.BackColor = Color.White;
            btnCerrar.Click += (s, e) => this.Close();
            this.Controls.Add(btnCerrar);
        }
    }
}