using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using cLibrary.Enums;
using System.Text.RegularExpressions;
using System.ComponentModel;
//using cLibrary.Utility;

namespace cLibrary.WinCustomControl
{
    public class cTextBox : TextBox
    {
        #region attribute

        private cImageButton _btnCanc;
        private ToolTip _toolTip;
        private int _minLength;
        private bool _isOpenFileDialog = false;
        private bool _isFolderBrowserDialog = false;
        private List<string> _fileNames;
        private ErrorProvider _error;
        private bool _isValidate;
        private IContainer components;

        private cRegExp _regExp;

        #endregion

        #region property
        [DefaultValue(cRegExp.Custom)]
        public cRegExp RegExpression
        {
            get { return _regExp; }
            set
            {
                if ((_isOpenFileDialog || _isFolderBrowserDialog) && value != cRegExp.Custom)
                    MessageBox.Show("Se si usa una funzione di dialog deve essere associata alla Regular Expression Custom.", "Attenzione", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                else
                    _regExp = value;
            }
        }

        public bool IsValidate() { return Valida(); }
        private bool Valida()
        {
            _error.Clear();

            if (IgoreValidator) { _isValidate = true; return _isValidate; }

            _isValidate = !string.IsNullOrEmpty(this.Text);

            if (!_isValidate)
                _error.SetError(this, "Il campo non puo essere vuoto.");

            if (_minLength > 0 && _isValidate)
            {
                _isValidate = !(Text.Length < _minLength);
                if (!_isValidate)
                    _error.SetError(this, string.Format("Il campo deve avere un minimo di caratteri pari a {0}.", _minLength));
            }

            if (_regExp != cRegExp.Custom && _isValidate)
            {
                _isValidate = Regex.IsMatch(this.Text, cEnumManager.GetStringValue(_regExp));

                if (!_isValidate)
                    _error.SetError(this, cEnumManager.GetDescription(_regExp));
            }
            return _isValidate;
        }
        public List<string> FileNames { get { return _fileNames; } }
        public bool IsFolderBrowserDialog
        {
            get { return this._isFolderBrowserDialog; }
            set
            {
                if (_isOpenFileDialog && value)
                    IsOpenFileDialog = false;

                this._isFolderBrowserDialog = value;
                this._regExp = cRegExp.Custom;
                this.ReadOnly = value;
            }
        }
        public bool IsOpenFileDialog
        {
            get { return this._isOpenFileDialog; }
            set
            {
                if (_isFolderBrowserDialog && value)
                    IsFolderBrowserDialog = false;

                this._isOpenFileDialog = value;
                this._regExp = cRegExp.Custom;
                this.ReadOnly = value;
            }
        }
        [DefaultValue(eFileFilter.None)]
        public eFileFilter FileDialogFilter { get; set; }
        [DefaultValue(false)]
        public bool IgoreValidator { get; set; }
        public int MinLenght { get { return _minLength; } set { if (value > MaxLength) MaxLength = value; _minLength = value; } }
        public override int MaxLength
        {
            get { return base.MaxLength; }
            set { if (value < _minLength)                    _minLength = value; base.MaxLength = value; }
        }
        #endregion

        public cTextBox()
        {
            SetButton();
            _error = new ErrorProvider();
            _toolTip = new ToolTip();
            _toolTip.InitialDelay = 2;
            _toolTip.ShowAlways = true;
            this.Controls.Add(_btnCanc);
        }

        #region override method

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);

            if (string.IsNullOrEmpty(this.Text))
            {
                if (_isOpenFileDialog)
                {
                    OpenFileDialog ofd = new OpenFileDialog();
                    ofd.Multiselect = this.Multiline;
                    if (FileDialogFilter != eFileFilter.None)
                        ofd.Filter = cEnumManager.GetStringValue(FileDialogFilter);
                    _fileNames = new List<string>();

                    if (ofd.ShowDialog() == DialogResult.OK)
                        if (this.Multiline)
                        {
                            _fileNames.AddRange(ofd.FileNames);
                            foreach (var str in ofd.FileNames)
                                this.Text += str + " \r\n";
                        }
                        else
                        {
                            _fileNames.Add(ofd.FileName);
                            this.Text = ofd.FileName;
                        }
                }
                else if (_isFolderBrowserDialog)
                {
                    FolderBrowserDialog fbd = new FolderBrowserDialog();
                    if (fbd.ShowDialog() == DialogResult.OK)
                        this.Text = fbd.SelectedPath.EndsWith("\\") ? fbd.SelectedPath : fbd.SelectedPath += "\\";
                }
            }
        }
        protected override void OnDoubleClick(EventArgs e)
        {
            base.OnDoubleClick(e);

            if (_isOpenFileDialog)
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Multiselect = false;
                if (ofd.ShowDialog() == DialogResult.OK)
                    this.Text = ofd.FileName;

            }
            else if (_isFolderBrowserDialog)
            {
                FolderBrowserDialog fbd = new FolderBrowserDialog();
                if (fbd.ShowDialog() == DialogResult.OK)
                    this.Text = fbd.SelectedPath.EndsWith("\\") ? fbd.SelectedPath : fbd.SelectedPath += "\\";
            }
        }
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            RelocationButton();
        }
        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);

            bool isVisible = false;
            if (!string.IsNullOrEmpty(this.Text))
                isVisible = true;

            _btnCanc.Enabled = _btnCanc.Visible = isVisible;

            if (!this.UseSystemPasswordChar)
                _toolTip.SetToolTip(this, Text);
        }
        protected override void OnLeave(EventArgs e)
        {
            base.OnLeave(e);

            Valida();
            #region comment
            //_error.Clear();

            //if (IgoreValidator) return;

            //_isValidate = !string.IsNullOrEmpty(this.Text);

            //if (!_isValidate)
            //    _error.SetError(this, "Il campo non puo essere vuoto.");

            //if (_minLength > 0 && _isValidate)
            //{
            //    _isValidate = !(Text.Length < _minLength);
            //    if (!_isValidate)
            //        _error.SetError(this, string.Format("Il campo deve avere un minimo di caratteri pari a {0}.", _minLength));
            //}

            //if (_regExp != cRegExp.Custom && _isValidate)
            //{
            //    _isValidate = Regex.IsMatch(this.Text, cEnumManager.GetStringValue(_regExp));

            //    if (!_isValidate)
            //        _error.SetError(this, cEnumManager.GetDescription(_regExp));
            //}

            //MessageBox.Show(cEnumManager.GetDescription(_regExp), "Attenzione", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            #endregion
        }

        #endregion

        private void SetButton()
        {
            _btnCanc = new cImageButton();
            _btnCanc.BackColor = Color.Transparent;
            _btnCanc.BackgroundImageLayout = ImageLayout.Stretch;
            //_btnCanc.NormalImage = Properties.Resources.delete;
            //_btnCanc.HoverImage = Properties.Resources.deleteHover;
            //_btnCanc.DownImage = Properties.Resources.deleteUnder;
            _btnCanc.Visible = false;
            _btnCanc.Text = "";
            _btnCanc.Cursor = System.Windows.Forms.Cursors.Hand;
            _btnCanc.Click += new EventHandler(CancellaText);

            RelocationButton();
        }
        private void RelocationButton()
        {
            Rectangle rect = this.ClientRectangle;
            //// set checkbox header to center of header cell. +1 pixel to position correctly.
            rect.X = Width - 22;
            rect.Y = Height / 2 - 11;
            _btnCanc.Size = new Size(18, 16);
            _btnCanc.Location = rect.Location;
        }
        private void CancellaText(object sender, EventArgs e)
        {
            this.Text = "";
        }
        public void SetError(string error) { _error.SetError(this, error); }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this._error = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this._error)).BeginInit();
            this.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._error)).EndInit();
            this.ResumeLayout(false);

        }
    }

}
