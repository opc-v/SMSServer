using System.Windows.Forms;

namespace SMSServerTest
{
    public partial class frmMain : Form
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.btnClientSend = new System.Windows.Forms.Button();
            this.btnTest_1_50 = new System.Windows.Forms.Button();
            this.btnTest_2_25 = new System.Windows.Forms.Button();
            this.btnClientConnect = new System.Windows.Forms.Button();
            this.pnlOptions = new System.Windows.Forms.Panel();
            this.nudPort = new System.Windows.Forms.NumericUpDown();
            this.txtServerIp = new System.Windows.Forms.TextBox();
            this.lblServerIp = new System.Windows.Forms.Label();
            this.lblPort = new System.Windows.Forms.Label();
            this.txtClientCode = new System.Windows.Forms.TextBox();
            this.lblClientCode = new System.Windows.Forms.Label();
            this.txtPhoneNumber = new System.Windows.Forms.TextBox();
            this.lblPhoneNumber = new System.Windows.Forms.Label();
            this.txtText = new System.Windows.Forms.TextBox();
            this.scMain = new System.Windows.Forms.SplitContainer();
            this.sMS4JsonDataGridView = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sMS4JsonBS = new System.Windows.Forms.BindingSource(this.components);
            this.dgvResponse = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.responseBS = new System.Windows.Forms.BindingSource(this.components);
            this.scNavigators = new System.Windows.Forms.SplitContainer();
            this.sMS4JsonBN = new System.Windows.Forms.BindingNavigator(this.components);
            this.bindingNavigatorAddNewItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorCountItem = new System.Windows.Forms.ToolStripLabel();
            this.bindingNavigatorDeleteItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMoveFirstItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMovePreviousItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorPositionItem = new System.Windows.Forms.ToolStripTextBox();
            this.bindingNavigatorSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorMoveNextItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMoveLastItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.sMS4JsonBindingNavigatorSaveItem = new System.Windows.Forms.ToolStripButton();
            this.responseBN = new System.Windows.Forms.BindingNavigator(this.components);
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton4 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripTextBox1 = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton5 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton6 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.responseBindingNavigatorSaveItem = new System.Windows.Forms.ToolStripButton();
            this.pnlOptions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudPort)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.scMain)).BeginInit();
            this.scMain.Panel1.SuspendLayout();
            this.scMain.Panel2.SuspendLayout();
            this.scMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sMS4JsonDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sMS4JsonBS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResponse)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.responseBS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.scNavigators)).BeginInit();
            this.scNavigators.Panel1.SuspendLayout();
            this.scNavigators.Panel2.SuspendLayout();
            this.scNavigators.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sMS4JsonBN)).BeginInit();
            this.sMS4JsonBN.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.responseBN)).BeginInit();
            this.responseBN.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnClientSend
            // 
            this.btnClientSend.Location = new System.Drawing.Point(3, 4);
            this.btnClientSend.Name = "btnClientSend";
            this.btnClientSend.Size = new System.Drawing.Size(84, 51);
            this.btnClientSend.TabIndex = 0;
            this.btnClientSend.Text = "Client Send";
            this.btnClientSend.UseVisualStyleBackColor = true;
            this.btnClientSend.Click += new System.EventHandler(this.btnClientSend_Click);
            // 
            // btnTest_1_50
            // 
            this.btnTest_1_50.Location = new System.Drawing.Point(93, 61);
            this.btnTest_1_50.Name = "btnTest_1_50";
            this.btnTest_1_50.Size = new System.Drawing.Size(75, 23);
            this.btnTest_1_50.TabIndex = 2;
            this.btnTest_1_50.Text = "Test 1 * 50";
            this.btnTest_1_50.UseVisualStyleBackColor = true;
            this.btnTest_1_50.Click += new System.EventHandler(this.btnTest_1_50_Click);
            // 
            // btnTest_2_25
            // 
            this.btnTest_2_25.Location = new System.Drawing.Point(174, 61);
            this.btnTest_2_25.Name = "btnTest_2_25";
            this.btnTest_2_25.Size = new System.Drawing.Size(75, 23);
            this.btnTest_2_25.TabIndex = 3;
            this.btnTest_2_25.Text = "Test 2 * 25";
            this.btnTest_2_25.UseVisualStyleBackColor = true;
            this.btnTest_2_25.Click += new System.EventHandler(this.btnTest_2_25_Click);
            // 
            // btnClientConnect
            // 
            this.btnClientConnect.Location = new System.Drawing.Point(691, 61);
            this.btnClientConnect.Name = "btnClientConnect";
            this.btnClientConnect.Size = new System.Drawing.Size(85, 23);
            this.btnClientConnect.TabIndex = 1;
            this.btnClientConnect.Text = "Client Connect";
            this.btnClientConnect.UseVisualStyleBackColor = true;
            this.btnClientConnect.Click += new System.EventHandler(this.btnClientConnect_Click);
            // 
            // pnlOptions
            // 
            this.pnlOptions.Controls.Add(this.nudPort);
            this.pnlOptions.Controls.Add(this.txtServerIp);
            this.pnlOptions.Controls.Add(this.lblServerIp);
            this.pnlOptions.Controls.Add(this.lblPort);
            this.pnlOptions.Controls.Add(this.txtClientCode);
            this.pnlOptions.Controls.Add(this.lblClientCode);
            this.pnlOptions.Controls.Add(this.txtPhoneNumber);
            this.pnlOptions.Controls.Add(this.lblPhoneNumber);
            this.pnlOptions.Controls.Add(this.txtText);
            this.pnlOptions.Controls.Add(this.btnClientConnect);
            this.pnlOptions.Controls.Add(this.btnTest_2_25);
            this.pnlOptions.Controls.Add(this.btnClientSend);
            this.pnlOptions.Controls.Add(this.btnTest_1_50);
            this.pnlOptions.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlOptions.Location = new System.Drawing.Point(0, 0);
            this.pnlOptions.Name = "pnlOptions";
            this.pnlOptions.Size = new System.Drawing.Size(1046, 89);
            this.pnlOptions.TabIndex = 5;
            // 
            // nudPort
            // 
            this.nudPort.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.nudPort.Location = new System.Drawing.Point(982, 61);
            this.nudPort.Maximum = new decimal(new int[] {
            64000,
            0,
            0,
            0});
            this.nudPort.Minimum = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            this.nudPort.Name = "nudPort";
            this.nudPort.Size = new System.Drawing.Size(61, 20);
            this.nudPort.TabIndex = 12;
            this.nudPort.Value = new decimal(new int[] {
            9111,
            0,
            0,
            0});
            // 
            // txtServerIp
            // 
            this.txtServerIp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtServerIp.Location = new System.Drawing.Point(830, 60);
            this.txtServerIp.Name = "txtServerIp";
            this.txtServerIp.Size = new System.Drawing.Size(89, 20);
            this.txtServerIp.TabIndex = 11;
            this.txtServerIp.Text = "127.0.0.1";
            // 
            // lblServerIp
            // 
            this.lblServerIp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblServerIp.AutoSize = true;
            this.lblServerIp.Location = new System.Drawing.Point(782, 63);
            this.lblServerIp.Name = "lblServerIp";
            this.lblServerIp.Size = new System.Drawing.Size(51, 13);
            this.lblServerIp.TabIndex = 10;
            this.lblServerIp.Text = "Server IP";
            // 
            // lblPort
            // 
            this.lblPort.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPort.AutoSize = true;
            this.lblPort.Location = new System.Drawing.Point(923, 63);
            this.lblPort.Name = "lblPort";
            this.lblPort.Size = new System.Drawing.Size(59, 13);
            this.lblPort.TabIndex = 8;
            this.lblPort.Text = "Server port";
            // 
            // txtClientCode
            // 
            this.txtClientCode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtClientCode.Location = new System.Drawing.Point(901, 30);
            this.txtClientCode.Name = "txtClientCode";
            this.txtClientCode.Size = new System.Drawing.Size(142, 20);
            this.txtClientCode.TabIndex = 7;
            this.txtClientCode.Text = "Client-Code";
            // 
            // lblClientCode
            // 
            this.lblClientCode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblClientCode.AutoSize = true;
            this.lblClientCode.Location = new System.Drawing.Point(816, 33);
            this.lblClientCode.Name = "lblClientCode";
            this.lblClientCode.Size = new System.Drawing.Size(61, 13);
            this.lblClientCode.TabIndex = 6;
            this.lblClientCode.Text = "Client Code";
            // 
            // txtPhoneNumber
            // 
            this.txtPhoneNumber.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPhoneNumber.Location = new System.Drawing.Point(901, 4);
            this.txtPhoneNumber.Name = "txtPhoneNumber";
            this.txtPhoneNumber.Size = new System.Drawing.Size(142, 20);
            this.txtPhoneNumber.TabIndex = 5;
            this.txtPhoneNumber.Text = "+1234567890";
            // 
            // lblPhoneNumber
            // 
            this.lblPhoneNumber.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPhoneNumber.AutoSize = true;
            this.lblPhoneNumber.Location = new System.Drawing.Point(816, 6);
            this.lblPhoneNumber.Name = "lblPhoneNumber";
            this.lblPhoneNumber.Size = new System.Drawing.Size(78, 13);
            this.lblPhoneNumber.TabIndex = 4;
            this.lblPhoneNumber.Text = "Phone Number";
            // 
            // txtText
            // 
            this.txtText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtText.Location = new System.Drawing.Point(95, 4);
            this.txtText.Multiline = true;
            this.txtText.Name = "txtText";
            this.txtText.Size = new System.Drawing.Size(714, 51);
            this.txtText.TabIndex = 2;
            // 
            // scMain
            // 
            this.scMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scMain.Location = new System.Drawing.Point(0, 89);
            this.scMain.Name = "scMain";
            this.scMain.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // scMain.Panel1
            // 
            this.scMain.Panel1.Controls.Add(this.sMS4JsonDataGridView);
            // 
            // scMain.Panel2
            // 
            this.scMain.Panel2.Controls.Add(this.dgvResponse);
            this.scMain.Panel2.Controls.Add(this.scNavigators);
            this.scMain.Size = new System.Drawing.Size(1046, 500);
            this.scMain.SplitterDistance = 208;
            this.scMain.TabIndex = 6;
            // 
            // sMS4JsonDataGridView
            // 
            this.sMS4JsonDataGridView.AllowUserToAddRows = false;
            this.sMS4JsonDataGridView.AllowUserToOrderColumns = true;
            this.sMS4JsonDataGridView.AutoGenerateColumns = false;
            this.sMS4JsonDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.sMS4JsonDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn4,
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3,
            this.dataGridViewTextBoxColumn5});
            this.sMS4JsonDataGridView.DataSource = this.sMS4JsonBS;
            this.sMS4JsonDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sMS4JsonDataGridView.Location = new System.Drawing.Point(0, 0);
            this.sMS4JsonDataGridView.Name = "sMS4JsonDataGridView";
            this.sMS4JsonDataGridView.ReadOnly = true;
            this.sMS4JsonDataGridView.Size = new System.Drawing.Size(1046, 208);
            this.sMS4JsonDataGridView.TabIndex = 5;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.DataPropertyName = "OriginalId";
            this.dataGridViewTextBoxColumn4.HeaderText = "OriginalId";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "PhoneNumber";
            this.dataGridViewTextBoxColumn1.HeaderText = "PhoneNumber";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "Text";
            this.dataGridViewTextBoxColumn2.HeaderText = "Text";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Width = 300;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.DataPropertyName = "ClientCode";
            this.dataGridViewTextBoxColumn3.HeaderText = "ClientCode";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.DataPropertyName = "OriginalDT";
            dataGridViewCellStyle3.Format = "dd.MM.yyyy HH.mm.ss:fff";
            dataGridViewCellStyle3.NullValue = null;
            this.dataGridViewTextBoxColumn5.DefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridViewTextBoxColumn5.HeaderText = "OriginalDT";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            this.dataGridViewTextBoxColumn5.Width = 150;
            // 
            // sMS4JsonBS
            // 
            this.sMS4JsonBS.DataSource = typeof(SMSExchange.SMS4Json);
            // 
            // dgvResponse
            // 
            this.dgvResponse.AutoGenerateColumns = false;
            this.dgvResponse.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvResponse.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn6,
            this.dataGridViewTextBoxColumn7,
            this.dataGridViewTextBoxColumn8});
            this.dgvResponse.DataSource = this.responseBS;
            this.dgvResponse.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvResponse.Location = new System.Drawing.Point(0, 25);
            this.dgvResponse.Name = "dgvResponse";
            this.dgvResponse.Size = new System.Drawing.Size(1046, 263);
            this.dgvResponse.TabIndex = 5;
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.DataPropertyName = "SMSOriginalId";
            this.dataGridViewTextBoxColumn6.HeaderText = "SMSOriginalId";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.DataPropertyName = "Kind";
            this.dataGridViewTextBoxColumn7.HeaderText = "Kind";
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            // 
            // dataGridViewTextBoxColumn8
            // 
            this.dataGridViewTextBoxColumn8.DataPropertyName = "Text";
            this.dataGridViewTextBoxColumn8.HeaderText = "Text";
            this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
            this.dataGridViewTextBoxColumn8.Width = 600;
            // 
            // responseBS
            // 
            this.responseBS.DataSource = typeof(SMSExchange.Response);
            // 
            // scNavigators
            // 
            this.scNavigators.Dock = System.Windows.Forms.DockStyle.Top;
            this.scNavigators.Location = new System.Drawing.Point(0, 0);
            this.scNavigators.Name = "scNavigators";
            // 
            // scNavigators.Panel1
            // 
            this.scNavigators.Panel1.Controls.Add(this.sMS4JsonBN);
            // 
            // scNavigators.Panel2
            // 
            this.scNavigators.Panel2.Controls.Add(this.responseBN);
            this.scNavigators.Size = new System.Drawing.Size(1046, 25);
            this.scNavigators.SplitterDistance = 491;
            this.scNavigators.TabIndex = 0;
            // 
            // sMS4JsonBN
            // 
            this.sMS4JsonBN.AddNewItem = this.bindingNavigatorAddNewItem;
            this.sMS4JsonBN.BindingSource = this.sMS4JsonBS;
            this.sMS4JsonBN.CountItem = this.bindingNavigatorCountItem;
            this.sMS4JsonBN.DeleteItem = this.bindingNavigatorDeleteItem;
            this.sMS4JsonBN.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bindingNavigatorMoveFirstItem,
            this.bindingNavigatorMovePreviousItem,
            this.bindingNavigatorSeparator,
            this.bindingNavigatorPositionItem,
            this.bindingNavigatorCountItem,
            this.bindingNavigatorSeparator1,
            this.bindingNavigatorMoveNextItem,
            this.bindingNavigatorMoveLastItem,
            this.bindingNavigatorSeparator2,
            this.bindingNavigatorAddNewItem,
            this.bindingNavigatorDeleteItem,
            this.sMS4JsonBindingNavigatorSaveItem});
            this.sMS4JsonBN.Location = new System.Drawing.Point(0, 0);
            this.sMS4JsonBN.MoveFirstItem = this.bindingNavigatorMoveFirstItem;
            this.sMS4JsonBN.MoveLastItem = this.bindingNavigatorMoveLastItem;
            this.sMS4JsonBN.MoveNextItem = this.bindingNavigatorMoveNextItem;
            this.sMS4JsonBN.MovePreviousItem = this.bindingNavigatorMovePreviousItem;
            this.sMS4JsonBN.Name = "sMS4JsonBN";
            this.sMS4JsonBN.PositionItem = this.bindingNavigatorPositionItem;
            this.sMS4JsonBN.Size = new System.Drawing.Size(491, 25);
            this.sMS4JsonBN.TabIndex = 7;
            this.sMS4JsonBN.Text = "bindingNavigator1";
            // 
            // bindingNavigatorAddNewItem
            // 
            this.bindingNavigatorAddNewItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorAddNewItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorAddNewItem.Image")));
            this.bindingNavigatorAddNewItem.Name = "bindingNavigatorAddNewItem";
            this.bindingNavigatorAddNewItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorAddNewItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorAddNewItem.Text = "Add new";
            this.bindingNavigatorAddNewItem.Visible = false;
            // 
            // bindingNavigatorCountItem
            // 
            this.bindingNavigatorCountItem.Name = "bindingNavigatorCountItem";
            this.bindingNavigatorCountItem.Size = new System.Drawing.Size(35, 22);
            this.bindingNavigatorCountItem.Text = "of {0}";
            this.bindingNavigatorCountItem.ToolTipText = "Total number of items";
            // 
            // bindingNavigatorDeleteItem
            // 
            this.bindingNavigatorDeleteItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorDeleteItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorDeleteItem.Image")));
            this.bindingNavigatorDeleteItem.Name = "bindingNavigatorDeleteItem";
            this.bindingNavigatorDeleteItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorDeleteItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorDeleteItem.Text = "Delete";
            // 
            // bindingNavigatorMoveFirstItem
            // 
            this.bindingNavigatorMoveFirstItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveFirstItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveFirstItem.Image")));
            this.bindingNavigatorMoveFirstItem.Name = "bindingNavigatorMoveFirstItem";
            this.bindingNavigatorMoveFirstItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveFirstItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMoveFirstItem.Text = "Move first";
            // 
            // bindingNavigatorMovePreviousItem
            // 
            this.bindingNavigatorMovePreviousItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMovePreviousItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMovePreviousItem.Image")));
            this.bindingNavigatorMovePreviousItem.Name = "bindingNavigatorMovePreviousItem";
            this.bindingNavigatorMovePreviousItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMovePreviousItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMovePreviousItem.Text = "Move previous";
            // 
            // bindingNavigatorSeparator
            // 
            this.bindingNavigatorSeparator.Name = "bindingNavigatorSeparator";
            this.bindingNavigatorSeparator.Size = new System.Drawing.Size(6, 25);
            // 
            // bindingNavigatorPositionItem
            // 
            this.bindingNavigatorPositionItem.AccessibleName = "Position";
            this.bindingNavigatorPositionItem.AutoSize = false;
            this.bindingNavigatorPositionItem.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.bindingNavigatorPositionItem.Name = "bindingNavigatorPositionItem";
            this.bindingNavigatorPositionItem.Size = new System.Drawing.Size(50, 23);
            this.bindingNavigatorPositionItem.Text = "0";
            this.bindingNavigatorPositionItem.ToolTipText = "Current position";
            // 
            // bindingNavigatorSeparator1
            // 
            this.bindingNavigatorSeparator1.Name = "bindingNavigatorSeparator1";
            this.bindingNavigatorSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // bindingNavigatorMoveNextItem
            // 
            this.bindingNavigatorMoveNextItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveNextItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveNextItem.Image")));
            this.bindingNavigatorMoveNextItem.Name = "bindingNavigatorMoveNextItem";
            this.bindingNavigatorMoveNextItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveNextItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMoveNextItem.Text = "Move next";
            // 
            // bindingNavigatorMoveLastItem
            // 
            this.bindingNavigatorMoveLastItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveLastItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveLastItem.Image")));
            this.bindingNavigatorMoveLastItem.Name = "bindingNavigatorMoveLastItem";
            this.bindingNavigatorMoveLastItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveLastItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMoveLastItem.Text = "Move last";
            // 
            // bindingNavigatorSeparator2
            // 
            this.bindingNavigatorSeparator2.Name = "bindingNavigatorSeparator2";
            this.bindingNavigatorSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // sMS4JsonBindingNavigatorSaveItem
            // 
            this.sMS4JsonBindingNavigatorSaveItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.sMS4JsonBindingNavigatorSaveItem.Enabled = false;
            this.sMS4JsonBindingNavigatorSaveItem.Image = ((System.Drawing.Image)(resources.GetObject("sMS4JsonBindingNavigatorSaveItem.Image")));
            this.sMS4JsonBindingNavigatorSaveItem.Name = "sMS4JsonBindingNavigatorSaveItem";
            this.sMS4JsonBindingNavigatorSaveItem.Size = new System.Drawing.Size(23, 22);
            this.sMS4JsonBindingNavigatorSaveItem.Text = "Save Data";
            this.sMS4JsonBindingNavigatorSaveItem.Visible = false;
            // 
            // responseBN
            // 
            this.responseBN.AddNewItem = this.toolStripButton1;
            this.responseBN.BindingSource = this.responseBS;
            this.responseBN.CountItem = this.toolStripLabel1;
            this.responseBN.DeleteItem = this.toolStripButton2;
            this.responseBN.Dock = System.Windows.Forms.DockStyle.Fill;
            this.responseBN.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton3,
            this.toolStripButton4,
            this.toolStripSeparator1,
            this.toolStripTextBox1,
            this.toolStripLabel1,
            this.toolStripSeparator2,
            this.toolStripButton5,
            this.toolStripButton6,
            this.toolStripSeparator3,
            this.toolStripButton1,
            this.toolStripButton2,
            this.responseBindingNavigatorSaveItem});
            this.responseBN.Location = new System.Drawing.Point(0, 0);
            this.responseBN.MoveFirstItem = this.toolStripButton3;
            this.responseBN.MoveLastItem = this.toolStripButton6;
            this.responseBN.MoveNextItem = this.toolStripButton5;
            this.responseBN.MovePreviousItem = this.toolStripButton4;
            this.responseBN.Name = "responseBN";
            this.responseBN.PositionItem = this.toolStripTextBox1;
            this.responseBN.Size = new System.Drawing.Size(551, 25);
            this.responseBN.TabIndex = 7;
            this.responseBN.Text = "bindingNavigator1";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.RightToLeftAutoMirrorImage = true;
            this.toolStripButton1.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton1.Text = "Add new";
            this.toolStripButton1.Visible = false;
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(35, 22);
            this.toolStripLabel1.Text = "of {0}";
            this.toolStripLabel1.ToolTipText = "Total number of items";
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton2.Image")));
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.RightToLeftAutoMirrorImage = true;
            this.toolStripButton2.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton2.Text = "Delete";
            // 
            // toolStripButton3
            // 
            this.toolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton3.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton3.Image")));
            this.toolStripButton3.Name = "toolStripButton3";
            this.toolStripButton3.RightToLeftAutoMirrorImage = true;
            this.toolStripButton3.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton3.Text = "Move first";
            // 
            // toolStripButton4
            // 
            this.toolStripButton4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton4.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton4.Image")));
            this.toolStripButton4.Name = "toolStripButton4";
            this.toolStripButton4.RightToLeftAutoMirrorImage = true;
            this.toolStripButton4.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton4.Text = "Move previous";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripTextBox1
            // 
            this.toolStripTextBox1.AccessibleName = "Position";
            this.toolStripTextBox1.AutoSize = false;
            this.toolStripTextBox1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.toolStripTextBox1.Name = "toolStripTextBox1";
            this.toolStripTextBox1.Size = new System.Drawing.Size(50, 23);
            this.toolStripTextBox1.Text = "0";
            this.toolStripTextBox1.ToolTipText = "Current position";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButton5
            // 
            this.toolStripButton5.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton5.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton5.Image")));
            this.toolStripButton5.Name = "toolStripButton5";
            this.toolStripButton5.RightToLeftAutoMirrorImage = true;
            this.toolStripButton5.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton5.Text = "Move next";
            // 
            // toolStripButton6
            // 
            this.toolStripButton6.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton6.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton6.Image")));
            this.toolStripButton6.Name = "toolStripButton6";
            this.toolStripButton6.RightToLeftAutoMirrorImage = true;
            this.toolStripButton6.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton6.Text = "Move last";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // responseBindingNavigatorSaveItem
            // 
            this.responseBindingNavigatorSaveItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.responseBindingNavigatorSaveItem.Enabled = false;
            this.responseBindingNavigatorSaveItem.Image = ((System.Drawing.Image)(resources.GetObject("responseBindingNavigatorSaveItem.Image")));
            this.responseBindingNavigatorSaveItem.Name = "responseBindingNavigatorSaveItem";
            this.responseBindingNavigatorSaveItem.Size = new System.Drawing.Size(23, 22);
            this.responseBindingNavigatorSaveItem.Text = "Save Data";
            this.responseBindingNavigatorSaveItem.Visible = false;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1046, 589);
            this.Controls.Add(this.scMain);
            this.Controls.Add(this.pnlOptions);
            this.Name = "frmMain";
            this.Text = "SMS";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.pnlOptions.ResumeLayout(false);
            this.pnlOptions.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudPort)).EndInit();
            this.scMain.Panel1.ResumeLayout(false);
            this.scMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.scMain)).EndInit();
            this.scMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.sMS4JsonDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sMS4JsonBS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResponse)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.responseBS)).EndInit();
            this.scNavigators.Panel1.ResumeLayout(false);
            this.scNavigators.Panel1.PerformLayout();
            this.scNavigators.Panel2.ResumeLayout(false);
            this.scNavigators.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scNavigators)).EndInit();
            this.scNavigators.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.sMS4JsonBN)).EndInit();
            this.sMS4JsonBN.ResumeLayout(false);
            this.sMS4JsonBN.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.responseBN)).EndInit();
            this.responseBN.ResumeLayout(false);
            this.responseBN.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnClientSend;
        private System.Windows.Forms.Button btnTest_1_50;
        private System.Windows.Forms.Button btnTest_2_25;
        private System.Windows.Forms.Button btnClientConnect;
        private System.Windows.Forms.Panel pnlOptions;
        private System.Windows.Forms.TextBox txtText;
        private System.Windows.Forms.SplitContainer scMain;
        private System.Windows.Forms.Label lblPhoneNumber;
        private System.Windows.Forms.TextBox txtClientCode;
        private System.Windows.Forms.Label lblClientCode;
        private System.Windows.Forms.TextBox txtPhoneNumber;
        private System.Windows.Forms.BindingSource sMS4JsonBS;
        private System.Windows.Forms.BindingNavigator sMS4JsonBN;
        private System.Windows.Forms.ToolStripButton bindingNavigatorAddNewItem;
        private System.Windows.Forms.ToolStripLabel bindingNavigatorCountItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorDeleteItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveFirstItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMovePreviousItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator;
        private System.Windows.Forms.ToolStripTextBox bindingNavigatorPositionItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator1;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveNextItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveLastItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator2;
        private System.Windows.Forms.ToolStripButton sMS4JsonBindingNavigatorSaveItem;
        private System.Windows.Forms.DataGridView sMS4JsonDataGridView;
        private System.Windows.Forms.SplitContainer scNavigators;
        private System.Windows.Forms.BindingSource responseBS;
        private System.Windows.Forms.BindingNavigator responseBN;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.ToolStripButton toolStripButton3;
        private System.Windows.Forms.ToolStripButton toolStripButton4;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBox1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton toolStripButton5;
        private System.Windows.Forms.ToolStripButton toolStripButton6;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton responseBindingNavigatorSaveItem;
        private System.Windows.Forms.DataGridView dgvResponse;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn9;
        private System.Windows.Forms.TextBox txtServerIp;
        private System.Windows.Forms.Label lblServerIp;
        private System.Windows.Forms.Label lblPort;
        private System.Windows.Forms.NumericUpDown nudPort;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
    }
}