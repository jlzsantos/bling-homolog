using AppHomolog.Entities;
using System.Text;

namespace AppHomolog
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
            
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            InitiateHomolog();
            
        }

        private void frmMain_Activated(object sender, EventArgs e)
        {
            InitiateForm();
        }

        private void InitiateForm()
        {
            txtClientId.Text = "a728e2741017a05b2399acd94c532041559b6c5d";
            txtClientSecret.Text = "961c60aa246519d68a40c2b15b5c10ad78017216f30e04eb64b815f3d935";
            
            txtReq1.ReadOnly = true;
            txtReq2.ReadOnly = true;
            txtReq3.ReadOnly = true;
            txtReq4.ReadOnly = true;
            txtReq5.ReadOnly = true;

            rtbResume.ReadOnly = true;

            btnStart.Focus();
        }

        private bool ValidateForm()
        {
            if (string.IsNullOrWhiteSpace(txtBaseUrl.Text))
            {
                MessageBox.Show("Informe a url base para obtenção de novo token.", "Bling Homolog");
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtApiBaseUrl.Text))
            {
                MessageBox.Show("Informe a url base para os endpoints da api.", "Bling Homolog");
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtToken.Text))
            {
                MessageBox.Show("Informe o token atual.", "Bling Homolog");
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtRefreshToken.Text))
            {
                MessageBox.Show("Informe o refresh token atual.", "Bling Homolog");
                return false;
            }

            return true;
        }

        private void InitiateHomolog()
        {
            rtbResume.Lines = [];

            if (!ValidateForm())
            {
                return;
            }

            btnStart.Enabled = false;

            var helper = new BlingHomologHelpers(
                txtBaseUrl.Text,
                txtApiBaseUrl.Text, 
                txtClientId.Text,
                txtClientSecret.Text,
                txtToken.Text, 
                txtRefreshToken.Text);

            string getProductUrl = $"{txtApiBaseUrl.Text}{txtReq1.Text}";
            var startHomologRS = helper.StartHomolog<Data>(getProductUrl);

            LoadResume(startHomologRS);
            CheckNewToken(startHomologRS);

            btnStart.Enabled = true;
        }

        private void CheckNewToken(BlingResponse<Data> response)
        {
            if (!string.IsNullOrWhiteSpace(response.Token) && !string.IsNullOrWhiteSpace(response.RefreshToken))
            {
                txtToken.Text = response.Token;
                txtRefreshToken.Text = response.RefreshToken;
            }
        }

        private void LoadResume(BlingResponse<Data> response)
        {
            if (response.Success)
            {
                var sbSuccess = new StringBuilder();

                sbSuccess.AppendLine($"Request 1 -> sucesso");
                sbSuccess.AppendLine($"Request 2 -> sucesso");
                sbSuccess.AppendLine($"Request 3 -> sucesso");
                sbSuccess.AppendLine($"Request 4 -> sucesso");
                sbSuccess.AppendLine($"Request 5 -> sucesso");

                SetResume(sbSuccess.ToString(), Color.Green);

                return;
            }

            if (response.ErrorData != null)
            {
                var sbSuccess = new StringBuilder();
                var sbError = new StringBuilder();

                for (int i = 1; i <= 5; i++)
                {
                    if (i > response.RequestNumber)
                    {
                        break;
                    }

                    if (response.RequestNumber == i)
                    {
                        sbError.AppendLine(GetErrorDescription(i, response.ErrorData));
                    }
                    else
                    {
                        sbSuccess.AppendLine($"Request {i} -> sucesso");
                    }
                }

                if (!string.IsNullOrWhiteSpace(sbSuccess.ToString()))
                {
                    SetResume(sbSuccess.ToString(), Color.Green);
                }

                if (!string.IsNullOrWhiteSpace(sbError.ToString()))
                {
                    SetResume(sbError.ToString(), Color.Red);
                }
            }
            else
            {
                SetResume("Erro na homologação.\r\nNão foi possível obter o erro.", Color.Red);
            }
        }

        private void ResetTextBoxColor(TextBox textBox)
        {
            FontDialog fd = new FontDialog();

            textBox.Font = fd.Font;
            textBox.BackColor = textBox.BackColor;
            textBox.ForeColor = fd.Color;
        }

        private string GetErrorDescription(int requestNumber, ErrorData errorData)
        {
            if (errorData != null && errorData.Error != null)
            {
                if ((errorData.Error.Fields?.Count() ?? 0) > 0)
                {
                    string errorMessage = errorData.Error.Fields.FirstOrDefault().Message;
                    return $"Request {requestNumber} -> [{errorData.Error.Type}] {errorMessage}";
                }

                return $"Request {requestNumber} -> [{errorData.Error.Type}] {errorData.Error.Description}";
            }

            return $"Request {requestNumber} -> Não foi possível obter o erro";
        }

        private void SetResume(string text, Color color)
        {
            rtbResume.SelectionStart = rtbResume.TextLength;
            rtbResume.SelectionLength = 0;
            rtbResume.SelectionColor = color;
            rtbResume.AppendText(text);
            rtbResume.SelectionColor = rtbResume.ForeColor; // Reset color
        }

        private void SetRequestStatus(TextBox textBox, string messageError, bool success)
        {
            FontDialog fd = new FontDialog();
            fd.ShowColor = true;

            if (success)
            {
                fd.Color = Color.Green;
                textBox.Text = "Sucesso";
            }
            else
            {
                fd.Color = Color.Red;
                textBox.Text = messageError;
            }

            textBox.Font = fd.Font;
            textBox.BackColor = textBox.BackColor;
            textBox.ForeColor = fd.Color;
        }
    }
}
