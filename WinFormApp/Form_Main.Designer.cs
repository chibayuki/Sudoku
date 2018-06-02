namespace WinFormApp
{
    partial class Form_Main
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_Main));
            this.Panel_Main = new System.Windows.Forms.Panel();
            this.Panel_Client = new System.Windows.Forms.Panel();
            this.Panel_FunctionArea = new System.Windows.Forms.Panel();
            this.Panel_FunctionAreaOptionsBar = new System.Windows.Forms.Panel();
            this.Label_Tab_Start = new System.Windows.Forms.Label();
            this.Label_Tab_Record = new System.Windows.Forms.Label();
            this.Label_Tab_Options = new System.Windows.Forms.Label();
            this.Label_Tab_About = new System.Windows.Forms.Label();
            this.Panel_FunctionAreaTab = new System.Windows.Forms.Panel();
            this.Panel_Tab_Start = new System.Windows.Forms.Panel();
            this.Panel_EnterGameSelection = new System.Windows.Forms.Panel();
            this.Label_StartNewGame = new System.Windows.Forms.Label();
            this.Label_ContinueLastGame = new System.Windows.Forms.Label();
            this.Panel_Tab_Record = new System.Windows.Forms.Panel();
            this.Panel_Score = new System.Windows.Forms.Panel();
            this.PictureBox_Score = new System.Windows.Forms.PictureBox();
            this.Label_ThisRecord = new System.Windows.Forms.Label();
            this.Label_ThisRecordVal_GameTime = new System.Windows.Forms.Label();
            this.Label_ThisRecordVal_StepCount = new System.Windows.Forms.Label();
            this.Label_BestRecord = new System.Windows.Forms.Label();
            this.Label_BestRecordVal_GameTime = new System.Windows.Forms.Label();
            this.Label_BestRecordVal_StepCount = new System.Windows.Forms.Label();
            this.Panel_GameTime = new System.Windows.Forms.Panel();
            this.PictureBox_GameTime = new System.Windows.Forms.PictureBox();
            this.Label_TotalTimeVal = new System.Windows.Forms.Label();
            this.Label_ThisTimeVal = new System.Windows.Forms.Label();
            this.Label_TotalTime = new System.Windows.Forms.Label();
            this.Label_ThisTime = new System.Windows.Forms.Label();
            this.Panel_Tab_Options = new System.Windows.Forms.Panel();
            this.Panel_Order = new System.Windows.Forms.Panel();
            this.Label_Order = new System.Windows.Forms.Label();
            this.RadioButton_Order3 = new System.Windows.Forms.RadioButton();
            this.RadioButton_Order4 = new System.Windows.Forms.RadioButton();
            this.RadioButton_Order5 = new System.Windows.Forms.RadioButton();
            this.Panel_DifficultyLevel = new System.Windows.Forms.Panel();
            this.Label_DifficultyLevel = new System.Windows.Forms.Label();
            this.Panel_DifficultyLevelAdjustment = new System.Windows.Forms.Panel();
            this.Label_DifficultyLevel_Val = new System.Windows.Forms.Label();
            this.Label_Easy = new System.Windows.Forms.Label();
            this.Label_Difficult = new System.Windows.Forms.Label();
            this.Panel_ShowNotes = new System.Windows.Forms.Panel();
            this.Label_ShowNotes = new System.Windows.Forms.Label();
            this.CheckBox_ShowNotes = new System.Windows.Forms.CheckBox();
            this.Label_ShowNotes_Info = new System.Windows.Forms.Label();
            this.Panel_OperationMode = new System.Windows.Forms.Panel();
            this.Label_OperationMode = new System.Windows.Forms.Label();
            this.RadioButton_Mouse = new System.Windows.Forms.RadioButton();
            this.RadioButton_Touch = new System.Windows.Forms.RadioButton();
            this.RadioButton_Keyboard = new System.Windows.Forms.RadioButton();
            this.CheckBox_AlwaysEnableKeyboard = new System.Windows.Forms.CheckBox();
            this.Panel_Save = new System.Windows.Forms.Panel();
            this.Label_Save = new System.Windows.Forms.Label();
            this.RadioButton_SaveEveryStep = new System.Windows.Forms.RadioButton();
            this.Label_TooSlow = new System.Windows.Forms.Label();
            this.Label_CleanGameStep = new System.Windows.Forms.Label();
            this.Label_CleanGameStepDone = new System.Windows.Forms.Label();
            this.RadioButton_SaveLastStep = new System.Windows.Forms.RadioButton();
            this.Panel_ThemeColor = new System.Windows.Forms.Panel();
            this.Label_ThemeColor = new System.Windows.Forms.Label();
            this.RadioButton_UseRandomThemeColor = new System.Windows.Forms.RadioButton();
            this.RadioButton_UseCustomColor = new System.Windows.Forms.RadioButton();
            this.Label_ThemeColorName = new System.Windows.Forms.Label();
            this.Panel_AntiAlias = new System.Windows.Forms.Panel();
            this.CheckBox_AntiAlias = new System.Windows.Forms.CheckBox();
            this.Label_AntiAlias = new System.Windows.Forms.Label();
            this.Panel_Tab_About = new System.Windows.Forms.Panel();
            this.PictureBox_ApplicationLogo = new System.Windows.Forms.PictureBox();
            this.Label_ApplicationName = new System.Windows.Forms.Label();
            this.Label_ApplicationEdition = new System.Windows.Forms.Label();
            this.Label_Version = new System.Windows.Forms.Label();
            this.Label_Copyright = new System.Windows.Forms.Label();
            this.Panel_GitHub = new System.Windows.Forms.Panel();
            this.Label_GitHub_Part1 = new System.Windows.Forms.Label();
            this.Label_GitHub_Base = new System.Windows.Forms.Label();
            this.Label_GitHub_Part2 = new System.Windows.Forms.Label();
            this.Label_GitHub_Release = new System.Windows.Forms.Label();
            this.Panel_GameUI = new System.Windows.Forms.Panel();
            this.Panel_Current = new System.Windows.Forms.Panel();
            this.Panel_Interrupt = new System.Windows.Forms.Panel();
            this.PictureBox_Erase = new System.Windows.Forms.PictureBox();
            this.PictureBox_Undo = new System.Windows.Forms.PictureBox();
            this.PictureBox_Redo = new System.Windows.Forms.PictureBox();
            this.PictureBox_Interrupt = new System.Windows.Forms.PictureBox();
            this.PictureBox_Restart = new System.Windows.Forms.PictureBox();
            this.PictureBox_ExitGame = new System.Windows.Forms.PictureBox();
            this.Panel_Environment = new System.Windows.Forms.Panel();
            this.Timer_EnterPrompt = new System.Windows.Forms.Timer(this.components);
            this.Timer_Timer = new System.Windows.Forms.Timer(this.components);
            this.ToolTip_InterruptPrompt = new System.Windows.Forms.ToolTip(this.components);
            this.ColorDialog_ThemeColor = new System.Windows.Forms.ColorDialog();
            this.BackgroundWorker_LoadGameStep = new System.ComponentModel.BackgroundWorker();
            this.BackgroundWorker_SaveGameStep = new System.ComponentModel.BackgroundWorker();
            this.Panel_Main.SuspendLayout();
            this.Panel_Client.SuspendLayout();
            this.Panel_FunctionArea.SuspendLayout();
            this.Panel_FunctionAreaOptionsBar.SuspendLayout();
            this.Panel_FunctionAreaTab.SuspendLayout();
            this.Panel_Tab_Start.SuspendLayout();
            this.Panel_EnterGameSelection.SuspendLayout();
            this.Panel_Tab_Record.SuspendLayout();
            this.Panel_Score.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox_Score)).BeginInit();
            this.Panel_GameTime.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox_GameTime)).BeginInit();
            this.Panel_Tab_Options.SuspendLayout();
            this.Panel_Order.SuspendLayout();
            this.Panel_DifficultyLevel.SuspendLayout();
            this.Panel_ShowNotes.SuspendLayout();
            this.Panel_OperationMode.SuspendLayout();
            this.Panel_Save.SuspendLayout();
            this.Panel_ThemeColor.SuspendLayout();
            this.Panel_AntiAlias.SuspendLayout();
            this.Panel_Tab_About.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox_ApplicationLogo)).BeginInit();
            this.Panel_GitHub.SuspendLayout();
            this.Panel_GameUI.SuspendLayout();
            this.Panel_Current.SuspendLayout();
            this.Panel_Interrupt.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox_Erase)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox_Undo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox_Redo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox_Interrupt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox_Restart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox_ExitGame)).BeginInit();
            this.SuspendLayout();
            // 
            // Panel_Main
            // 
            this.Panel_Main.BackColor = System.Drawing.Color.Transparent;
            this.Panel_Main.Controls.Add(this.Panel_Client);
            this.Panel_Main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Panel_Main.Location = new System.Drawing.Point(0, 0);
            this.Panel_Main.Name = "Panel_Main";
            this.Panel_Main.Size = new System.Drawing.Size(585, 420);
            this.Panel_Main.TabIndex = 0;
            // 
            // Panel_Client
            // 
            this.Panel_Client.BackColor = System.Drawing.Color.Transparent;
            this.Panel_Client.Controls.Add(this.Panel_FunctionArea);
            this.Panel_Client.Controls.Add(this.Panel_GameUI);
            this.Panel_Client.Location = new System.Drawing.Point(0, 0);
            this.Panel_Client.Name = "Panel_Client";
            this.Panel_Client.Size = new System.Drawing.Size(585, 420);
            this.Panel_Client.TabIndex = 0;
            // 
            // Panel_FunctionArea
            // 
            this.Panel_FunctionArea.BackColor = System.Drawing.Color.Transparent;
            this.Panel_FunctionArea.Controls.Add(this.Panel_FunctionAreaOptionsBar);
            this.Panel_FunctionArea.Controls.Add(this.Panel_FunctionAreaTab);
            this.Panel_FunctionArea.Location = new System.Drawing.Point(0, 0);
            this.Panel_FunctionArea.Name = "Panel_FunctionArea";
            this.Panel_FunctionArea.Size = new System.Drawing.Size(585, 420);
            this.Panel_FunctionArea.TabIndex = 0;
            // 
            // Panel_FunctionAreaOptionsBar
            // 
            this.Panel_FunctionAreaOptionsBar.BackColor = System.Drawing.Color.Transparent;
            this.Panel_FunctionAreaOptionsBar.Controls.Add(this.Label_Tab_Start);
            this.Panel_FunctionAreaOptionsBar.Controls.Add(this.Label_Tab_Record);
            this.Panel_FunctionAreaOptionsBar.Controls.Add(this.Label_Tab_Options);
            this.Panel_FunctionAreaOptionsBar.Controls.Add(this.Label_Tab_About);
            this.Panel_FunctionAreaOptionsBar.Location = new System.Drawing.Point(0, 0);
            this.Panel_FunctionAreaOptionsBar.MaximumSize = new System.Drawing.Size(150, 65535);
            this.Panel_FunctionAreaOptionsBar.MinimumSize = new System.Drawing.Size(50, 50);
            this.Panel_FunctionAreaOptionsBar.Name = "Panel_FunctionAreaOptionsBar";
            this.Panel_FunctionAreaOptionsBar.Size = new System.Drawing.Size(150, 420);
            this.Panel_FunctionAreaOptionsBar.TabIndex = 0;
            this.Panel_FunctionAreaOptionsBar.Paint += new System.Windows.Forms.PaintEventHandler(this.Panel_FunctionAreaOptionsBar_Paint);
            // 
            // Label_Tab_Start
            // 
            this.Label_Tab_Start.AutoEllipsis = true;
            this.Label_Tab_Start.BackColor = System.Drawing.Color.Transparent;
            this.Label_Tab_Start.Font = new System.Drawing.Font("微软雅黑", 11.25F);
            this.Label_Tab_Start.ForeColor = System.Drawing.Color.White;
            this.Label_Tab_Start.Location = new System.Drawing.Point(0, 0);
            this.Label_Tab_Start.MaximumSize = new System.Drawing.Size(150, 50);
            this.Label_Tab_Start.MinimumSize = new System.Drawing.Size(50, 10);
            this.Label_Tab_Start.Name = "Label_Tab_Start";
            this.Label_Tab_Start.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.Label_Tab_Start.Size = new System.Drawing.Size(150, 50);
            this.Label_Tab_Start.TabIndex = 0;
            this.Label_Tab_Start.Text = "开始";
            this.Label_Tab_Start.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Label_Tab_Start.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Label_Tab_Start_MouseDown);
            this.Label_Tab_Start.MouseEnter += new System.EventHandler(this.Label_Tab_MouseEnter);
            this.Label_Tab_Start.MouseLeave += new System.EventHandler(this.Label_Tab_MouseLeave);
            // 
            // Label_Tab_Record
            // 
            this.Label_Tab_Record.AutoEllipsis = true;
            this.Label_Tab_Record.BackColor = System.Drawing.Color.Transparent;
            this.Label_Tab_Record.Font = new System.Drawing.Font("微软雅黑", 11.25F);
            this.Label_Tab_Record.ForeColor = System.Drawing.Color.White;
            this.Label_Tab_Record.Location = new System.Drawing.Point(0, 50);
            this.Label_Tab_Record.MaximumSize = new System.Drawing.Size(150, 50);
            this.Label_Tab_Record.MinimumSize = new System.Drawing.Size(50, 10);
            this.Label_Tab_Record.Name = "Label_Tab_Record";
            this.Label_Tab_Record.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.Label_Tab_Record.Size = new System.Drawing.Size(150, 50);
            this.Label_Tab_Record.TabIndex = 0;
            this.Label_Tab_Record.Text = "记录";
            this.Label_Tab_Record.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Label_Tab_Record.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Label_Tab_Record_MouseDown);
            this.Label_Tab_Record.MouseEnter += new System.EventHandler(this.Label_Tab_MouseEnter);
            this.Label_Tab_Record.MouseLeave += new System.EventHandler(this.Label_Tab_MouseLeave);
            // 
            // Label_Tab_Options
            // 
            this.Label_Tab_Options.AutoEllipsis = true;
            this.Label_Tab_Options.BackColor = System.Drawing.Color.Transparent;
            this.Label_Tab_Options.Font = new System.Drawing.Font("微软雅黑", 11.25F);
            this.Label_Tab_Options.ForeColor = System.Drawing.Color.White;
            this.Label_Tab_Options.Location = new System.Drawing.Point(0, 100);
            this.Label_Tab_Options.MaximumSize = new System.Drawing.Size(150, 50);
            this.Label_Tab_Options.MinimumSize = new System.Drawing.Size(50, 10);
            this.Label_Tab_Options.Name = "Label_Tab_Options";
            this.Label_Tab_Options.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.Label_Tab_Options.Size = new System.Drawing.Size(150, 50);
            this.Label_Tab_Options.TabIndex = 0;
            this.Label_Tab_Options.Text = "选项";
            this.Label_Tab_Options.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Label_Tab_Options.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Label_Tab_Options_MouseDown);
            this.Label_Tab_Options.MouseEnter += new System.EventHandler(this.Label_Tab_MouseEnter);
            this.Label_Tab_Options.MouseLeave += new System.EventHandler(this.Label_Tab_MouseLeave);
            // 
            // Label_Tab_About
            // 
            this.Label_Tab_About.AutoEllipsis = true;
            this.Label_Tab_About.BackColor = System.Drawing.Color.Transparent;
            this.Label_Tab_About.Font = new System.Drawing.Font("微软雅黑", 11.25F);
            this.Label_Tab_About.ForeColor = System.Drawing.Color.White;
            this.Label_Tab_About.Location = new System.Drawing.Point(0, 150);
            this.Label_Tab_About.MaximumSize = new System.Drawing.Size(150, 50);
            this.Label_Tab_About.MinimumSize = new System.Drawing.Size(50, 10);
            this.Label_Tab_About.Name = "Label_Tab_About";
            this.Label_Tab_About.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.Label_Tab_About.Size = new System.Drawing.Size(150, 50);
            this.Label_Tab_About.TabIndex = 0;
            this.Label_Tab_About.Text = "关于";
            this.Label_Tab_About.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Label_Tab_About.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Label_Tab_About_MouseDown);
            this.Label_Tab_About.MouseEnter += new System.EventHandler(this.Label_Tab_MouseEnter);
            this.Label_Tab_About.MouseLeave += new System.EventHandler(this.Label_Tab_MouseLeave);
            // 
            // Panel_FunctionAreaTab
            // 
            this.Panel_FunctionAreaTab.AutoScroll = true;
            this.Panel_FunctionAreaTab.BackColor = System.Drawing.Color.Transparent;
            this.Panel_FunctionAreaTab.Controls.Add(this.Panel_Tab_Start);
            this.Panel_FunctionAreaTab.Controls.Add(this.Panel_Tab_Record);
            this.Panel_FunctionAreaTab.Controls.Add(this.Panel_Tab_Options);
            this.Panel_FunctionAreaTab.Controls.Add(this.Panel_Tab_About);
            this.Panel_FunctionAreaTab.Location = new System.Drawing.Point(150, 0);
            this.Panel_FunctionAreaTab.Name = "Panel_FunctionAreaTab";
            this.Panel_FunctionAreaTab.Size = new System.Drawing.Size(435, 420);
            this.Panel_FunctionAreaTab.TabIndex = 0;
            // 
            // Panel_Tab_Start
            // 
            this.Panel_Tab_Start.BackColor = System.Drawing.Color.Transparent;
            this.Panel_Tab_Start.Controls.Add(this.Panel_EnterGameSelection);
            this.Panel_Tab_Start.Location = new System.Drawing.Point(0, 0);
            this.Panel_Tab_Start.MinimumSize = new System.Drawing.Size(260, 80);
            this.Panel_Tab_Start.Name = "Panel_Tab_Start";
            this.Panel_Tab_Start.Size = new System.Drawing.Size(435, 420);
            this.Panel_Tab_Start.TabIndex = 0;
            // 
            // Panel_EnterGameSelection
            // 
            this.Panel_EnterGameSelection.BackColor = System.Drawing.Color.Transparent;
            this.Panel_EnterGameSelection.Controls.Add(this.Label_StartNewGame);
            this.Panel_EnterGameSelection.Controls.Add(this.Label_ContinueLastGame);
            this.Panel_EnterGameSelection.Location = new System.Drawing.Point(75, 170);
            this.Panel_EnterGameSelection.Name = "Panel_EnterGameSelection";
            this.Panel_EnterGameSelection.Size = new System.Drawing.Size(260, 80);
            this.Panel_EnterGameSelection.TabIndex = 0;
            this.Panel_EnterGameSelection.Paint += new System.Windows.Forms.PaintEventHandler(this.Panel_EnterGameSelection_Paint);
            // 
            // Label_StartNewGame
            // 
            this.Label_StartNewGame.AutoEllipsis = true;
            this.Label_StartNewGame.BackColor = System.Drawing.Color.Transparent;
            this.Label_StartNewGame.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.Label_StartNewGame.ForeColor = System.Drawing.Color.White;
            this.Label_StartNewGame.Location = new System.Drawing.Point(0, 0);
            this.Label_StartNewGame.Name = "Label_StartNewGame";
            this.Label_StartNewGame.Size = new System.Drawing.Size(260, 40);
            this.Label_StartNewGame.TabIndex = 0;
            this.Label_StartNewGame.Text = "开始新游戏";
            this.Label_StartNewGame.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Label_ContinueLastGame
            // 
            this.Label_ContinueLastGame.AutoEllipsis = true;
            this.Label_ContinueLastGame.BackColor = System.Drawing.Color.Transparent;
            this.Label_ContinueLastGame.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.Label_ContinueLastGame.ForeColor = System.Drawing.Color.White;
            this.Label_ContinueLastGame.Location = new System.Drawing.Point(0, 40);
            this.Label_ContinueLastGame.Name = "Label_ContinueLastGame";
            this.Label_ContinueLastGame.Size = new System.Drawing.Size(260, 40);
            this.Label_ContinueLastGame.TabIndex = 0;
            this.Label_ContinueLastGame.Text = "继续上次的游戏";
            this.Label_ContinueLastGame.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Panel_Tab_Record
            // 
            this.Panel_Tab_Record.BackColor = System.Drawing.Color.Transparent;
            this.Panel_Tab_Record.Controls.Add(this.Panel_Score);
            this.Panel_Tab_Record.Controls.Add(this.Panel_GameTime);
            this.Panel_Tab_Record.Location = new System.Drawing.Point(0, 0);
            this.Panel_Tab_Record.MinimumSize = new System.Drawing.Size(350, 385);
            this.Panel_Tab_Record.Name = "Panel_Tab_Record";
            this.Panel_Tab_Record.Size = new System.Drawing.Size(435, 420);
            this.Panel_Tab_Record.TabIndex = 0;
            this.Panel_Tab_Record.Visible = false;
            // 
            // Panel_Score
            // 
            this.Panel_Score.BackColor = System.Drawing.Color.Transparent;
            this.Panel_Score.Controls.Add(this.PictureBox_Score);
            this.Panel_Score.Controls.Add(this.Label_ThisRecord);
            this.Panel_Score.Controls.Add(this.Label_ThisRecordVal_GameTime);
            this.Panel_Score.Controls.Add(this.Label_ThisRecordVal_StepCount);
            this.Panel_Score.Controls.Add(this.Label_BestRecord);
            this.Panel_Score.Controls.Add(this.Label_BestRecordVal_GameTime);
            this.Panel_Score.Controls.Add(this.Label_BestRecordVal_StepCount);
            this.Panel_Score.Location = new System.Drawing.Point(30, 30);
            this.Panel_Score.Name = "Panel_Score";
            this.Panel_Score.Size = new System.Drawing.Size(360, 120);
            this.Panel_Score.TabIndex = 0;
            this.Panel_Score.Paint += new System.Windows.Forms.PaintEventHandler(this.Panel_Score_Paint);
            // 
            // PictureBox_Score
            // 
            this.PictureBox_Score.BackColor = System.Drawing.Color.Transparent;
            this.PictureBox_Score.ErrorImage = null;
            this.PictureBox_Score.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox_Score.Image")));
            this.PictureBox_Score.InitialImage = null;
            this.PictureBox_Score.Location = new System.Drawing.Point(0, 0);
            this.PictureBox_Score.Name = "PictureBox_Score";
            this.PictureBox_Score.Size = new System.Drawing.Size(20, 20);
            this.PictureBox_Score.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox_Score.TabIndex = 0;
            this.PictureBox_Score.TabStop = false;
            // 
            // Label_ThisRecord
            // 
            this.Label_ThisRecord.AutoSize = true;
            this.Label_ThisRecord.BackColor = System.Drawing.Color.Transparent;
            this.Label_ThisRecord.Font = new System.Drawing.Font("微软雅黑", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Label_ThisRecord.ForeColor = System.Drawing.Color.White;
            this.Label_ThisRecord.Location = new System.Drawing.Point(40, 70);
            this.Label_ThisRecord.Name = "Label_ThisRecord";
            this.Label_ThisRecord.Size = new System.Drawing.Size(99, 20);
            this.Label_ThisRecord.TabIndex = 0;
            this.Label_ThisRecord.Text = "本次完成用时";
            // 
            // Label_ThisRecordVal_GameTime
            // 
            this.Label_ThisRecordVal_GameTime.AutoSize = true;
            this.Label_ThisRecordVal_GameTime.BackColor = System.Drawing.Color.Transparent;
            this.Label_ThisRecordVal_GameTime.Font = new System.Drawing.Font("微软雅黑", 11.25F, System.Drawing.FontStyle.Bold);
            this.Label_ThisRecordVal_GameTime.ForeColor = System.Drawing.Color.White;
            this.Label_ThisRecordVal_GameTime.Location = new System.Drawing.Point(0, 30);
            this.Label_ThisRecordVal_GameTime.Name = "Label_ThisRecordVal_GameTime";
            this.Label_ThisRecordVal_GameTime.Size = new System.Drawing.Size(180, 19);
            this.Label_ThisRecordVal_GameTime.TabIndex = 0;
            this.Label_ThisRecordVal_GameTime.Text = "ThisRecord_GameTime";
            // 
            // Label_ThisRecordVal_StepCount
            // 
            this.Label_ThisRecordVal_StepCount.AutoSize = true;
            this.Label_ThisRecordVal_StepCount.BackColor = System.Drawing.Color.Transparent;
            this.Label_ThisRecordVal_StepCount.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Label_ThisRecordVal_StepCount.ForeColor = System.Drawing.Color.White;
            this.Label_ThisRecordVal_StepCount.Location = new System.Drawing.Point(21, 50);
            this.Label_ThisRecordVal_StepCount.Name = "Label_ThisRecordVal_StepCount";
            this.Label_ThisRecordVal_StepCount.Size = new System.Drawing.Size(138, 17);
            this.Label_ThisRecordVal_StepCount.TabIndex = 0;
            this.Label_ThisRecordVal_StepCount.Text = "ThisRecord_StepCount";
            // 
            // Label_BestRecord
            // 
            this.Label_BestRecord.AutoSize = true;
            this.Label_BestRecord.BackColor = System.Drawing.Color.Transparent;
            this.Label_BestRecord.Font = new System.Drawing.Font("微软雅黑", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Label_BestRecord.ForeColor = System.Drawing.Color.White;
            this.Label_BestRecord.Location = new System.Drawing.Point(220, 70);
            this.Label_BestRecord.Name = "Label_BestRecord";
            this.Label_BestRecord.Size = new System.Drawing.Size(99, 20);
            this.Label_BestRecord.TabIndex = 0;
            this.Label_BestRecord.Text = "最快完成用时";
            // 
            // Label_BestRecordVal_GameTime
            // 
            this.Label_BestRecordVal_GameTime.AutoSize = true;
            this.Label_BestRecordVal_GameTime.BackColor = System.Drawing.Color.Transparent;
            this.Label_BestRecordVal_GameTime.Font = new System.Drawing.Font("微软雅黑", 11.25F, System.Drawing.FontStyle.Bold);
            this.Label_BestRecordVal_GameTime.ForeColor = System.Drawing.Color.White;
            this.Label_BestRecordVal_GameTime.Location = new System.Drawing.Point(180, 30);
            this.Label_BestRecordVal_GameTime.Name = "Label_BestRecordVal_GameTime";
            this.Label_BestRecordVal_GameTime.Size = new System.Drawing.Size(182, 19);
            this.Label_BestRecordVal_GameTime.TabIndex = 0;
            this.Label_BestRecordVal_GameTime.Text = "BestRecord_GameTime";
            // 
            // Label_BestRecordVal_StepCount
            // 
            this.Label_BestRecordVal_StepCount.AutoSize = true;
            this.Label_BestRecordVal_StepCount.BackColor = System.Drawing.Color.Transparent;
            this.Label_BestRecordVal_StepCount.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Label_BestRecordVal_StepCount.ForeColor = System.Drawing.Color.White;
            this.Label_BestRecordVal_StepCount.Location = new System.Drawing.Point(200, 50);
            this.Label_BestRecordVal_StepCount.Name = "Label_BestRecordVal_StepCount";
            this.Label_BestRecordVal_StepCount.Size = new System.Drawing.Size(140, 17);
            this.Label_BestRecordVal_StepCount.TabIndex = 0;
            this.Label_BestRecordVal_StepCount.Text = "BestRecord_StepCount";
            // 
            // Panel_GameTime
            // 
            this.Panel_GameTime.BackColor = System.Drawing.Color.Transparent;
            this.Panel_GameTime.Controls.Add(this.PictureBox_GameTime);
            this.Panel_GameTime.Controls.Add(this.Label_TotalTimeVal);
            this.Panel_GameTime.Controls.Add(this.Label_ThisTimeVal);
            this.Panel_GameTime.Controls.Add(this.Label_TotalTime);
            this.Panel_GameTime.Controls.Add(this.Label_ThisTime);
            this.Panel_GameTime.Location = new System.Drawing.Point(30, 150);
            this.Panel_GameTime.Name = "Panel_GameTime";
            this.Panel_GameTime.Size = new System.Drawing.Size(360, 120);
            this.Panel_GameTime.TabIndex = 0;
            this.Panel_GameTime.Paint += new System.Windows.Forms.PaintEventHandler(this.Panel_GameTime_Paint);
            // 
            // PictureBox_GameTime
            // 
            this.PictureBox_GameTime.BackColor = System.Drawing.Color.Transparent;
            this.PictureBox_GameTime.ErrorImage = null;
            this.PictureBox_GameTime.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox_GameTime.Image")));
            this.PictureBox_GameTime.InitialImage = null;
            this.PictureBox_GameTime.Location = new System.Drawing.Point(0, 0);
            this.PictureBox_GameTime.Name = "PictureBox_GameTime";
            this.PictureBox_GameTime.Size = new System.Drawing.Size(20, 20);
            this.PictureBox_GameTime.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox_GameTime.TabIndex = 0;
            this.PictureBox_GameTime.TabStop = false;
            // 
            // Label_TotalTimeVal
            // 
            this.Label_TotalTimeVal.AutoSize = true;
            this.Label_TotalTimeVal.BackColor = System.Drawing.Color.Transparent;
            this.Label_TotalTimeVal.Font = new System.Drawing.Font("微软雅黑", 11.25F, System.Drawing.FontStyle.Bold);
            this.Label_TotalTimeVal.ForeColor = System.Drawing.Color.White;
            this.Label_TotalTimeVal.Location = new System.Drawing.Point(135, 70);
            this.Label_TotalTimeVal.Name = "Label_TotalTimeVal";
            this.Label_TotalTimeVal.Size = new System.Drawing.Size(128, 19);
            this.Label_TotalTimeVal.TabIndex = 0;
            this.Label_TotalTimeVal.Text = "TotalGameTime";
            // 
            // Label_ThisTimeVal
            // 
            this.Label_ThisTimeVal.AutoSize = true;
            this.Label_ThisTimeVal.BackColor = System.Drawing.Color.Transparent;
            this.Label_ThisTimeVal.Font = new System.Drawing.Font("微软雅黑", 11.25F, System.Drawing.FontStyle.Bold);
            this.Label_ThisTimeVal.ForeColor = System.Drawing.Color.White;
            this.Label_ThisTimeVal.Location = new System.Drawing.Point(135, 30);
            this.Label_ThisTimeVal.Name = "Label_ThisTimeVal";
            this.Label_ThisTimeVal.Size = new System.Drawing.Size(120, 19);
            this.Label_ThisTimeVal.TabIndex = 0;
            this.Label_ThisTimeVal.Text = "ThisGameTime";
            // 
            // Label_TotalTime
            // 
            this.Label_TotalTime.AutoSize = true;
            this.Label_TotalTime.BackColor = System.Drawing.Color.Transparent;
            this.Label_TotalTime.Font = new System.Drawing.Font("微软雅黑", 11.25F);
            this.Label_TotalTime.ForeColor = System.Drawing.Color.White;
            this.Label_TotalTime.Location = new System.Drawing.Point(25, 70);
            this.Label_TotalTime.Name = "Label_TotalTime";
            this.Label_TotalTime.Size = new System.Drawing.Size(103, 20);
            this.Label_TotalTime.TabIndex = 0;
            this.Label_TotalTime.Text = "累计游戏时长:";
            // 
            // Label_ThisTime
            // 
            this.Label_ThisTime.AutoSize = true;
            this.Label_ThisTime.BackColor = System.Drawing.Color.Transparent;
            this.Label_ThisTime.Font = new System.Drawing.Font("微软雅黑", 11.25F);
            this.Label_ThisTime.ForeColor = System.Drawing.Color.White;
            this.Label_ThisTime.Location = new System.Drawing.Point(25, 30);
            this.Label_ThisTime.Name = "Label_ThisTime";
            this.Label_ThisTime.Size = new System.Drawing.Size(103, 20);
            this.Label_ThisTime.TabIndex = 0;
            this.Label_ThisTime.Text = "本次游戏时长:";
            // 
            // Panel_Tab_Options
            // 
            this.Panel_Tab_Options.BackColor = System.Drawing.Color.Transparent;
            this.Panel_Tab_Options.Controls.Add(this.Panel_Order);
            this.Panel_Tab_Options.Controls.Add(this.Panel_DifficultyLevel);
            this.Panel_Tab_Options.Controls.Add(this.Panel_ShowNotes);
            this.Panel_Tab_Options.Controls.Add(this.Panel_OperationMode);
            this.Panel_Tab_Options.Controls.Add(this.Panel_Save);
            this.Panel_Tab_Options.Controls.Add(this.Panel_ThemeColor);
            this.Panel_Tab_Options.Controls.Add(this.Panel_AntiAlias);
            this.Panel_Tab_Options.Location = new System.Drawing.Point(0, 0);
            this.Panel_Tab_Options.MinimumSize = new System.Drawing.Size(410, 815);
            this.Panel_Tab_Options.Name = "Panel_Tab_Options";
            this.Panel_Tab_Options.Size = new System.Drawing.Size(435, 815);
            this.Panel_Tab_Options.TabIndex = 0;
            this.Panel_Tab_Options.Visible = false;
            // 
            // Panel_Order
            // 
            this.Panel_Order.BackColor = System.Drawing.Color.Transparent;
            this.Panel_Order.Controls.Add(this.Label_Order);
            this.Panel_Order.Controls.Add(this.RadioButton_Order3);
            this.Panel_Order.Controls.Add(this.RadioButton_Order4);
            this.Panel_Order.Controls.Add(this.RadioButton_Order5);
            this.Panel_Order.Location = new System.Drawing.Point(30, 30);
            this.Panel_Order.Name = "Panel_Order";
            this.Panel_Order.Size = new System.Drawing.Size(350, 115);
            this.Panel_Order.TabIndex = 0;
            this.Panel_Order.Paint += new System.Windows.Forms.PaintEventHandler(this.Panel_Order_Paint);
            // 
            // Label_Order
            // 
            this.Label_Order.AutoSize = true;
            this.Label_Order.BackColor = System.Drawing.Color.Transparent;
            this.Label_Order.Dock = System.Windows.Forms.DockStyle.Top;
            this.Label_Order.Font = new System.Drawing.Font("微软雅黑", 11.25F);
            this.Label_Order.ForeColor = System.Drawing.Color.White;
            this.Label_Order.Location = new System.Drawing.Point(0, 0);
            this.Label_Order.Name = "Label_Order";
            this.Label_Order.Size = new System.Drawing.Size(69, 20);
            this.Label_Order.TabIndex = 0;
            this.Label_Order.Text = "数独大小";
            // 
            // RadioButton_Order3
            // 
            this.RadioButton_Order3.AutoSize = true;
            this.RadioButton_Order3.BackColor = System.Drawing.Color.Transparent;
            this.RadioButton_Order3.Font = new System.Drawing.Font("微软雅黑", 9.75F);
            this.RadioButton_Order3.ForeColor = System.Drawing.Color.White;
            this.RadioButton_Order3.Location = new System.Drawing.Point(25, 30);
            this.RadioButton_Order3.Name = "RadioButton_Order3";
            this.RadioButton_Order3.Size = new System.Drawing.Size(126, 23);
            this.RadioButton_Order3.TabIndex = 0;
            this.RadioButton_Order3.Text = "3 阶 (9 宫 81 格)";
            this.RadioButton_Order3.UseVisualStyleBackColor = false;
            this.RadioButton_Order3.CheckedChanged += new System.EventHandler(this.RadioButton_Order3_CheckedChanged);
            // 
            // RadioButton_Order4
            // 
            this.RadioButton_Order4.AutoSize = true;
            this.RadioButton_Order4.BackColor = System.Drawing.Color.Transparent;
            this.RadioButton_Order4.Font = new System.Drawing.Font("微软雅黑", 9.75F);
            this.RadioButton_Order4.ForeColor = System.Drawing.Color.White;
            this.RadioButton_Order4.Location = new System.Drawing.Point(25, 55);
            this.RadioButton_Order4.Name = "RadioButton_Order4";
            this.RadioButton_Order4.Size = new System.Drawing.Size(142, 23);
            this.RadioButton_Order4.TabIndex = 0;
            this.RadioButton_Order4.Text = "4 阶 (16 宫 256 格)";
            this.RadioButton_Order4.UseVisualStyleBackColor = false;
            this.RadioButton_Order4.CheckedChanged += new System.EventHandler(this.RadioButton_Order4_CheckedChanged);
            // 
            // RadioButton_Order5
            // 
            this.RadioButton_Order5.AutoSize = true;
            this.RadioButton_Order5.BackColor = System.Drawing.Color.Transparent;
            this.RadioButton_Order5.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.RadioButton_Order5.ForeColor = System.Drawing.Color.White;
            this.RadioButton_Order5.Location = new System.Drawing.Point(25, 80);
            this.RadioButton_Order5.Name = "RadioButton_Order5";
            this.RadioButton_Order5.Size = new System.Drawing.Size(142, 23);
            this.RadioButton_Order5.TabIndex = 0;
            this.RadioButton_Order5.Text = "5 阶 (25 宫 625 格)";
            this.RadioButton_Order5.UseVisualStyleBackColor = false;
            this.RadioButton_Order5.CheckedChanged += new System.EventHandler(this.RadioButton_Order5_CheckedChanged);
            // 
            // Panel_DifficultyLevel
            // 
            this.Panel_DifficultyLevel.BackColor = System.Drawing.Color.Transparent;
            this.Panel_DifficultyLevel.Controls.Add(this.Label_DifficultyLevel);
            this.Panel_DifficultyLevel.Controls.Add(this.Panel_DifficultyLevelAdjustment);
            this.Panel_DifficultyLevel.Controls.Add(this.Label_DifficultyLevel_Val);
            this.Panel_DifficultyLevel.Controls.Add(this.Label_Easy);
            this.Panel_DifficultyLevel.Controls.Add(this.Label_Difficult);
            this.Panel_DifficultyLevel.Location = new System.Drawing.Point(30, 145);
            this.Panel_DifficultyLevel.Name = "Panel_DifficultyLevel";
            this.Panel_DifficultyLevel.Size = new System.Drawing.Size(350, 90);
            this.Panel_DifficultyLevel.TabIndex = 0;
            this.Panel_DifficultyLevel.Paint += new System.Windows.Forms.PaintEventHandler(this.Panel_DifficultyLevel_Paint);
            // 
            // Label_DifficultyLevel
            // 
            this.Label_DifficultyLevel.AutoSize = true;
            this.Label_DifficultyLevel.BackColor = System.Drawing.Color.Transparent;
            this.Label_DifficultyLevel.Dock = System.Windows.Forms.DockStyle.Top;
            this.Label_DifficultyLevel.Font = new System.Drawing.Font("微软雅黑", 11.25F);
            this.Label_DifficultyLevel.ForeColor = System.Drawing.Color.White;
            this.Label_DifficultyLevel.Location = new System.Drawing.Point(0, 0);
            this.Label_DifficultyLevel.Name = "Label_DifficultyLevel";
            this.Label_DifficultyLevel.Size = new System.Drawing.Size(39, 20);
            this.Label_DifficultyLevel.TabIndex = 0;
            this.Label_DifficultyLevel.Text = "难度";
            this.Label_DifficultyLevel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Panel_DifficultyLevelAdjustment
            // 
            this.Panel_DifficultyLevelAdjustment.BackColor = System.Drawing.Color.Transparent;
            this.Panel_DifficultyLevelAdjustment.Location = new System.Drawing.Point(65, 55);
            this.Panel_DifficultyLevelAdjustment.Name = "Panel_DifficultyLevelAdjustment";
            this.Panel_DifficultyLevelAdjustment.Size = new System.Drawing.Size(215, 24);
            this.Panel_DifficultyLevelAdjustment.TabIndex = 0;
            this.Panel_DifficultyLevelAdjustment.Paint += new System.Windows.Forms.PaintEventHandler(this.Panel_DifficultyLevelAdjustment_Paint);
            this.Panel_DifficultyLevelAdjustment.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Panel_DifficultyLevelAdjustment_MouseDown);
            this.Panel_DifficultyLevelAdjustment.MouseEnter += new System.EventHandler(this.Panel_DifficultyLevelAdjustment_MouseEnter);
            this.Panel_DifficultyLevelAdjustment.MouseLeave += new System.EventHandler(this.Panel_DifficultyLevelAdjustment_MouseLeave);
            this.Panel_DifficultyLevelAdjustment.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Panel_DifficultyLevelAdjustment_MouseMove);
            this.Panel_DifficultyLevelAdjustment.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Panel_DifficultyLevelAdjustment_MouseUp);
            this.Panel_DifficultyLevelAdjustment.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.Panel_DifficultyLevelAdjustment_MouseWheel);
            // 
            // Label_DifficultyLevel_Val
            // 
            this.Label_DifficultyLevel_Val.AutoSize = true;
            this.Label_DifficultyLevel_Val.BackColor = System.Drawing.Color.Transparent;
            this.Label_DifficultyLevel_Val.Font = new System.Drawing.Font("微软雅黑", 9.75F);
            this.Label_DifficultyLevel_Val.ForeColor = System.Drawing.Color.White;
            this.Label_DifficultyLevel_Val.Location = new System.Drawing.Point(164, 30);
            this.Label_DifficultyLevel_Val.Name = "Label_DifficultyLevel_Val";
            this.Label_DifficultyLevel_Val.Size = new System.Drawing.Size(17, 19);
            this.Label_DifficultyLevel_Val.TabIndex = 0;
            this.Label_DifficultyLevel_Val.Text = "0";
            this.Label_DifficultyLevel_Val.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Label_Easy
            // 
            this.Label_Easy.AutoSize = true;
            this.Label_Easy.BackColor = System.Drawing.Color.Transparent;
            this.Label_Easy.Font = new System.Drawing.Font("微软雅黑", 9.75F);
            this.Label_Easy.ForeColor = System.Drawing.Color.White;
            this.Label_Easy.Location = new System.Drawing.Point(25, 57);
            this.Label_Easy.Name = "Label_Easy";
            this.Label_Easy.Size = new System.Drawing.Size(35, 19);
            this.Label_Easy.TabIndex = 0;
            this.Label_Easy.Text = "简单";
            this.Label_Easy.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Label_Difficult
            // 
            this.Label_Difficult.AutoSize = true;
            this.Label_Difficult.BackColor = System.Drawing.Color.Transparent;
            this.Label_Difficult.Font = new System.Drawing.Font("微软雅黑", 9.75F);
            this.Label_Difficult.ForeColor = System.Drawing.Color.White;
            this.Label_Difficult.Location = new System.Drawing.Point(285, 57);
            this.Label_Difficult.Name = "Label_Difficult";
            this.Label_Difficult.Size = new System.Drawing.Size(35, 19);
            this.Label_Difficult.TabIndex = 0;
            this.Label_Difficult.Text = "困难";
            this.Label_Difficult.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Panel_ShowNotes
            // 
            this.Panel_ShowNotes.BackColor = System.Drawing.Color.Transparent;
            this.Panel_ShowNotes.Controls.Add(this.Label_ShowNotes);
            this.Panel_ShowNotes.Controls.Add(this.CheckBox_ShowNotes);
            this.Panel_ShowNotes.Controls.Add(this.Label_ShowNotes_Info);
            this.Panel_ShowNotes.Location = new System.Drawing.Point(30, 235);
            this.Panel_ShowNotes.Name = "Panel_ShowNotes";
            this.Panel_ShowNotes.Size = new System.Drawing.Size(350, 90);
            this.Panel_ShowNotes.TabIndex = 0;
            this.Panel_ShowNotes.Paint += new System.Windows.Forms.PaintEventHandler(this.Panel_ShowNotes_Paint);
            // 
            // Label_ShowNotes
            // 
            this.Label_ShowNotes.AutoSize = true;
            this.Label_ShowNotes.BackColor = System.Drawing.Color.Transparent;
            this.Label_ShowNotes.Dock = System.Windows.Forms.DockStyle.Top;
            this.Label_ShowNotes.Font = new System.Drawing.Font("微软雅黑", 11.25F);
            this.Label_ShowNotes.ForeColor = System.Drawing.Color.White;
            this.Label_ShowNotes.Location = new System.Drawing.Point(0, 0);
            this.Label_ShowNotes.Name = "Label_ShowNotes";
            this.Label_ShowNotes.Size = new System.Drawing.Size(39, 20);
            this.Label_ShowNotes.TabIndex = 0;
            this.Label_ShowNotes.Text = "提示";
            // 
            // CheckBox_ShowNotes
            // 
            this.CheckBox_ShowNotes.AutoSize = true;
            this.CheckBox_ShowNotes.BackColor = System.Drawing.Color.Transparent;
            this.CheckBox_ShowNotes.Font = new System.Drawing.Font("微软雅黑", 9.75F);
            this.CheckBox_ShowNotes.ForeColor = System.Drawing.Color.White;
            this.CheckBox_ShowNotes.Location = new System.Drawing.Point(25, 30);
            this.CheckBox_ShowNotes.Name = "CheckBox_ShowNotes";
            this.CheckBox_ShowNotes.Size = new System.Drawing.Size(106, 23);
            this.CheckBox_ShowNotes.TabIndex = 0;
            this.CheckBox_ShowNotes.TabStop = false;
            this.CheckBox_ShowNotes.Text = "显示提示信息";
            this.CheckBox_ShowNotes.UseVisualStyleBackColor = false;
            this.CheckBox_ShowNotes.CheckedChanged += new System.EventHandler(this.CheckBox_ShowNotes_CheckedChanged);
            // 
            // Label_ShowNotes_Info
            // 
            this.Label_ShowNotes_Info.AutoSize = true;
            this.Label_ShowNotes_Info.BackColor = System.Drawing.Color.Transparent;
            this.Label_ShowNotes_Info.Font = new System.Drawing.Font("微软雅黑", 9.75F);
            this.Label_ShowNotes_Info.ForeColor = System.Drawing.Color.White;
            this.Label_ShowNotes_Info.Location = new System.Drawing.Point(41, 55);
            this.Label_ShowNotes_Info.Name = "Label_ShowNotes_Info";
            this.Label_ShowNotes_Info.Size = new System.Drawing.Size(178, 19);
            this.Label_ShowNotes_Info.TabIndex = 0;
            this.Label_ShowNotes_Info.Text = "选择此项将不会保存你的记录";
            this.Label_ShowNotes_Info.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Panel_OperationMode
            // 
            this.Panel_OperationMode.BackColor = System.Drawing.Color.Transparent;
            this.Panel_OperationMode.Controls.Add(this.Label_OperationMode);
            this.Panel_OperationMode.Controls.Add(this.RadioButton_Mouse);
            this.Panel_OperationMode.Controls.Add(this.RadioButton_Touch);
            this.Panel_OperationMode.Controls.Add(this.RadioButton_Keyboard);
            this.Panel_OperationMode.Controls.Add(this.CheckBox_AlwaysEnableKeyboard);
            this.Panel_OperationMode.Location = new System.Drawing.Point(30, 325);
            this.Panel_OperationMode.Name = "Panel_OperationMode";
            this.Panel_OperationMode.Size = new System.Drawing.Size(350, 140);
            this.Panel_OperationMode.TabIndex = 0;
            this.Panel_OperationMode.Paint += new System.Windows.Forms.PaintEventHandler(this.Panel_OperationMode_Paint);
            // 
            // Label_OperationMode
            // 
            this.Label_OperationMode.AutoSize = true;
            this.Label_OperationMode.BackColor = System.Drawing.Color.Transparent;
            this.Label_OperationMode.Dock = System.Windows.Forms.DockStyle.Top;
            this.Label_OperationMode.Font = new System.Drawing.Font("微软雅黑", 11.25F);
            this.Label_OperationMode.ForeColor = System.Drawing.Color.White;
            this.Label_OperationMode.Location = new System.Drawing.Point(0, 0);
            this.Label_OperationMode.Name = "Label_OperationMode";
            this.Label_OperationMode.Size = new System.Drawing.Size(69, 20);
            this.Label_OperationMode.TabIndex = 0;
            this.Label_OperationMode.Text = "操作方式";
            // 
            // RadioButton_Mouse
            // 
            this.RadioButton_Mouse.AutoSize = true;
            this.RadioButton_Mouse.BackColor = System.Drawing.Color.Transparent;
            this.RadioButton_Mouse.Font = new System.Drawing.Font("微软雅黑", 9.75F);
            this.RadioButton_Mouse.ForeColor = System.Drawing.Color.White;
            this.RadioButton_Mouse.Location = new System.Drawing.Point(25, 30);
            this.RadioButton_Mouse.Name = "RadioButton_Mouse";
            this.RadioButton_Mouse.Size = new System.Drawing.Size(130, 23);
            this.RadioButton_Mouse.TabIndex = 0;
            this.RadioButton_Mouse.Text = "鼠标 (点击或单击)";
            this.RadioButton_Mouse.UseVisualStyleBackColor = false;
            // 
            // RadioButton_Touch
            // 
            this.RadioButton_Touch.AutoSize = true;
            this.RadioButton_Touch.BackColor = System.Drawing.Color.Transparent;
            this.RadioButton_Touch.Font = new System.Drawing.Font("微软雅黑", 9.75F);
            this.RadioButton_Touch.ForeColor = System.Drawing.Color.White;
            this.RadioButton_Touch.Location = new System.Drawing.Point(25, 55);
            this.RadioButton_Touch.Name = "RadioButton_Touch";
            this.RadioButton_Touch.Size = new System.Drawing.Size(130, 23);
            this.RadioButton_Touch.TabIndex = 0;
            this.RadioButton_Touch.Text = "触屏 (轻触或拖放)";
            this.RadioButton_Touch.UseVisualStyleBackColor = false;
            // 
            // RadioButton_Keyboard
            // 
            this.RadioButton_Keyboard.AutoSize = true;
            this.RadioButton_Keyboard.BackColor = System.Drawing.Color.Transparent;
            this.RadioButton_Keyboard.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.RadioButton_Keyboard.ForeColor = System.Drawing.Color.White;
            this.RadioButton_Keyboard.Location = new System.Drawing.Point(25, 80);
            this.RadioButton_Keyboard.Name = "RadioButton_Keyboard";
            this.RadioButton_Keyboard.Size = new System.Drawing.Size(156, 23);
            this.RadioButton_Keyboard.TabIndex = 0;
            this.RadioButton_Keyboard.Text = "键盘 (方向键或数字键)";
            this.RadioButton_Keyboard.UseVisualStyleBackColor = false;
            // 
            // CheckBox_AlwaysEnableKeyboard
            // 
            this.CheckBox_AlwaysEnableKeyboard.AutoSize = true;
            this.CheckBox_AlwaysEnableKeyboard.BackColor = System.Drawing.Color.Transparent;
            this.CheckBox_AlwaysEnableKeyboard.Font = new System.Drawing.Font("微软雅黑", 9.75F);
            this.CheckBox_AlwaysEnableKeyboard.ForeColor = System.Drawing.Color.White;
            this.CheckBox_AlwaysEnableKeyboard.Location = new System.Drawing.Point(45, 105);
            this.CheckBox_AlwaysEnableKeyboard.Name = "CheckBox_AlwaysEnableKeyboard";
            this.CheckBox_AlwaysEnableKeyboard.Size = new System.Drawing.Size(249, 23);
            this.CheckBox_AlwaysEnableKeyboard.TabIndex = 0;
            this.CheckBox_AlwaysEnableKeyboard.TabStop = false;
            this.CheckBox_AlwaysEnableKeyboard.Text = "即使选择其他操作方式，键盘仍然可用";
            this.CheckBox_AlwaysEnableKeyboard.UseVisualStyleBackColor = false;
            // 
            // Panel_Save
            // 
            this.Panel_Save.BackColor = System.Drawing.Color.Transparent;
            this.Panel_Save.Controls.Add(this.Label_Save);
            this.Panel_Save.Controls.Add(this.RadioButton_SaveEveryStep);
            this.Panel_Save.Controls.Add(this.Label_TooSlow);
            this.Panel_Save.Controls.Add(this.Label_CleanGameStep);
            this.Panel_Save.Controls.Add(this.Label_CleanGameStepDone);
            this.Panel_Save.Controls.Add(this.RadioButton_SaveLastStep);
            this.Panel_Save.Location = new System.Drawing.Point(30, 465);
            this.Panel_Save.Name = "Panel_Save";
            this.Panel_Save.Size = new System.Drawing.Size(350, 140);
            this.Panel_Save.TabIndex = 0;
            this.Panel_Save.Paint += new System.Windows.Forms.PaintEventHandler(this.Panel_Save_Paint);
            // 
            // Label_Save
            // 
            this.Label_Save.AutoSize = true;
            this.Label_Save.BackColor = System.Drawing.Color.Transparent;
            this.Label_Save.Dock = System.Windows.Forms.DockStyle.Top;
            this.Label_Save.Font = new System.Drawing.Font("微软雅黑", 11.25F);
            this.Label_Save.ForeColor = System.Drawing.Color.White;
            this.Label_Save.Location = new System.Drawing.Point(0, 0);
            this.Label_Save.Name = "Label_Save";
            this.Label_Save.Size = new System.Drawing.Size(39, 20);
            this.Label_Save.TabIndex = 0;
            this.Label_Save.Text = "存档";
            // 
            // RadioButton_SaveEveryStep
            // 
            this.RadioButton_SaveEveryStep.AutoSize = true;
            this.RadioButton_SaveEveryStep.BackColor = System.Drawing.Color.Transparent;
            this.RadioButton_SaveEveryStep.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.RadioButton_SaveEveryStep.ForeColor = System.Drawing.Color.White;
            this.RadioButton_SaveEveryStep.Location = new System.Drawing.Point(25, 30);
            this.RadioButton_SaveEveryStep.Name = "RadioButton_SaveEveryStep";
            this.RadioButton_SaveEveryStep.Size = new System.Drawing.Size(105, 23);
            this.RadioButton_SaveEveryStep.TabIndex = 0;
            this.RadioButton_SaveEveryStep.Text = "保存所有步骤";
            this.RadioButton_SaveEveryStep.UseVisualStyleBackColor = false;
            this.RadioButton_SaveEveryStep.CheckedChanged += new System.EventHandler(this.RadioButton_SaveEveryStep_CheckedChanged);
            // 
            // Label_TooSlow
            // 
            this.Label_TooSlow.AutoSize = true;
            this.Label_TooSlow.BackColor = System.Drawing.Color.Transparent;
            this.Label_TooSlow.Font = new System.Drawing.Font("微软雅黑", 9.75F);
            this.Label_TooSlow.ForeColor = System.Drawing.Color.White;
            this.Label_TooSlow.Location = new System.Drawing.Point(41, 55);
            this.Label_TooSlow.Name = "Label_TooSlow";
            this.Label_TooSlow.Size = new System.Drawing.Size(181, 19);
            this.Label_TooSlow.TabIndex = 0;
            this.Label_TooSlow.Text = "如果打开或保存较慢，你可以:";
            // 
            // Label_CleanGameStep
            // 
            this.Label_CleanGameStep.AutoSize = true;
            this.Label_CleanGameStep.BackColor = System.Drawing.Color.Transparent;
            this.Label_CleanGameStep.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Label_CleanGameStep.ForeColor = System.Drawing.Color.White;
            this.Label_CleanGameStep.Location = new System.Drawing.Point(41, 80);
            this.Label_CleanGameStep.Name = "Label_CleanGameStep";
            this.Label_CleanGameStep.Size = new System.Drawing.Size(87, 19);
            this.Label_CleanGameStep.TabIndex = 0;
            this.Label_CleanGameStep.Text = "清理所有步骤";
            // 
            // Label_CleanGameStepDone
            // 
            this.Label_CleanGameStepDone.AutoSize = true;
            this.Label_CleanGameStepDone.BackColor = System.Drawing.Color.Transparent;
            this.Label_CleanGameStepDone.Font = new System.Drawing.Font("微软雅黑", 9.75F);
            this.Label_CleanGameStepDone.ForeColor = System.Drawing.Color.White;
            this.Label_CleanGameStepDone.Location = new System.Drawing.Point(135, 80);
            this.Label_CleanGameStepDone.Name = "Label_CleanGameStepDone";
            this.Label_CleanGameStepDone.Size = new System.Drawing.Size(35, 19);
            this.Label_CleanGameStepDone.TabIndex = 0;
            this.Label_CleanGameStepDone.Text = "完成";
            // 
            // RadioButton_SaveLastStep
            // 
            this.RadioButton_SaveLastStep.AutoSize = true;
            this.RadioButton_SaveLastStep.BackColor = System.Drawing.Color.Transparent;
            this.RadioButton_SaveLastStep.Font = new System.Drawing.Font("微软雅黑", 9.75F);
            this.RadioButton_SaveLastStep.ForeColor = System.Drawing.Color.White;
            this.RadioButton_SaveLastStep.Location = new System.Drawing.Point(25, 105);
            this.RadioButton_SaveLastStep.Name = "RadioButton_SaveLastStep";
            this.RadioButton_SaveLastStep.Size = new System.Drawing.Size(118, 23);
            this.RadioButton_SaveLastStep.TabIndex = 0;
            this.RadioButton_SaveLastStep.Text = "仅保存最后一步";
            this.RadioButton_SaveLastStep.UseVisualStyleBackColor = false;
            this.RadioButton_SaveLastStep.CheckedChanged += new System.EventHandler(this.RadioButton_SaveLastStep_CheckedChanged);
            // 
            // Panel_ThemeColor
            // 
            this.Panel_ThemeColor.BackColor = System.Drawing.Color.Transparent;
            this.Panel_ThemeColor.Controls.Add(this.Label_ThemeColor);
            this.Panel_ThemeColor.Controls.Add(this.RadioButton_UseRandomThemeColor);
            this.Panel_ThemeColor.Controls.Add(this.RadioButton_UseCustomColor);
            this.Panel_ThemeColor.Controls.Add(this.Label_ThemeColorName);
            this.Panel_ThemeColor.Location = new System.Drawing.Point(30, 605);
            this.Panel_ThemeColor.Name = "Panel_ThemeColor";
            this.Panel_ThemeColor.Size = new System.Drawing.Size(350, 115);
            this.Panel_ThemeColor.TabIndex = 0;
            this.Panel_ThemeColor.Paint += new System.Windows.Forms.PaintEventHandler(this.Panel_ThemeColor_Paint);
            // 
            // Label_ThemeColor
            // 
            this.Label_ThemeColor.AutoSize = true;
            this.Label_ThemeColor.BackColor = System.Drawing.Color.Transparent;
            this.Label_ThemeColor.Dock = System.Windows.Forms.DockStyle.Top;
            this.Label_ThemeColor.Font = new System.Drawing.Font("微软雅黑", 11.25F);
            this.Label_ThemeColor.ForeColor = System.Drawing.Color.White;
            this.Label_ThemeColor.Location = new System.Drawing.Point(0, 0);
            this.Label_ThemeColor.Name = "Label_ThemeColor";
            this.Label_ThemeColor.Size = new System.Drawing.Size(69, 20);
            this.Label_ThemeColor.TabIndex = 0;
            this.Label_ThemeColor.Text = "主题颜色";
            // 
            // RadioButton_UseRandomThemeColor
            // 
            this.RadioButton_UseRandomThemeColor.AutoSize = true;
            this.RadioButton_UseRandomThemeColor.BackColor = System.Drawing.Color.Transparent;
            this.RadioButton_UseRandomThemeColor.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.RadioButton_UseRandomThemeColor.ForeColor = System.Drawing.Color.White;
            this.RadioButton_UseRandomThemeColor.Location = new System.Drawing.Point(25, 30);
            this.RadioButton_UseRandomThemeColor.Name = "RadioButton_UseRandomThemeColor";
            this.RadioButton_UseRandomThemeColor.Size = new System.Drawing.Size(53, 23);
            this.RadioButton_UseRandomThemeColor.TabIndex = 0;
            this.RadioButton_UseRandomThemeColor.Text = "随机";
            this.RadioButton_UseRandomThemeColor.UseVisualStyleBackColor = false;
            this.RadioButton_UseRandomThemeColor.CheckedChanged += new System.EventHandler(this.RadioButton_UseRandomThemeColor_CheckedChanged);
            // 
            // RadioButton_UseCustomColor
            // 
            this.RadioButton_UseCustomColor.AutoSize = true;
            this.RadioButton_UseCustomColor.BackColor = System.Drawing.Color.Transparent;
            this.RadioButton_UseCustomColor.Font = new System.Drawing.Font("微软雅黑", 9.75F);
            this.RadioButton_UseCustomColor.ForeColor = System.Drawing.Color.White;
            this.RadioButton_UseCustomColor.Location = new System.Drawing.Point(25, 55);
            this.RadioButton_UseCustomColor.Name = "RadioButton_UseCustomColor";
            this.RadioButton_UseCustomColor.Size = new System.Drawing.Size(69, 23);
            this.RadioButton_UseCustomColor.TabIndex = 0;
            this.RadioButton_UseCustomColor.Text = "自定义:";
            this.RadioButton_UseCustomColor.UseVisualStyleBackColor = false;
            this.RadioButton_UseCustomColor.CheckedChanged += new System.EventHandler(this.RadioButton_UseCustomColor_CheckedChanged);
            // 
            // Label_ThemeColorName
            // 
            this.Label_ThemeColorName.AutoSize = true;
            this.Label_ThemeColorName.BackColor = System.Drawing.Color.Transparent;
            this.Label_ThemeColorName.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Label_ThemeColorName.ForeColor = System.Drawing.Color.White;
            this.Label_ThemeColorName.Location = new System.Drawing.Point(41, 80);
            this.Label_ThemeColorName.Name = "Label_ThemeColorName";
            this.Label_ThemeColorName.Size = new System.Drawing.Size(83, 19);
            this.Label_ThemeColorName.TabIndex = 0;
            this.Label_ThemeColorName.Text = "ThemeColor";
            // 
            // Panel_AntiAlias
            // 
            this.Panel_AntiAlias.BackColor = System.Drawing.Color.Transparent;
            this.Panel_AntiAlias.Controls.Add(this.CheckBox_AntiAlias);
            this.Panel_AntiAlias.Controls.Add(this.Label_AntiAlias);
            this.Panel_AntiAlias.Location = new System.Drawing.Point(30, 720);
            this.Panel_AntiAlias.Name = "Panel_AntiAlias";
            this.Panel_AntiAlias.Size = new System.Drawing.Size(350, 65);
            this.Panel_AntiAlias.TabIndex = 0;
            this.Panel_AntiAlias.Paint += new System.Windows.Forms.PaintEventHandler(this.Panel_AntiAlias_Paint);
            // 
            // CheckBox_AntiAlias
            // 
            this.CheckBox_AntiAlias.AutoSize = true;
            this.CheckBox_AntiAlias.BackColor = System.Drawing.Color.Transparent;
            this.CheckBox_AntiAlias.Font = new System.Drawing.Font("微软雅黑", 9.75F);
            this.CheckBox_AntiAlias.ForeColor = System.Drawing.Color.White;
            this.CheckBox_AntiAlias.Location = new System.Drawing.Point(25, 30);
            this.CheckBox_AntiAlias.Name = "CheckBox_AntiAlias";
            this.CheckBox_AntiAlias.Size = new System.Drawing.Size(145, 23);
            this.CheckBox_AntiAlias.TabIndex = 0;
            this.CheckBox_AntiAlias.TabStop = false;
            this.CheckBox_AntiAlias.Text = "使用抗锯齿模式绘图";
            this.CheckBox_AntiAlias.UseVisualStyleBackColor = false;
            this.CheckBox_AntiAlias.CheckedChanged += new System.EventHandler(this.CheckBox_AntiAlias_CheckedChanged);
            // 
            // Label_AntiAlias
            // 
            this.Label_AntiAlias.AutoSize = true;
            this.Label_AntiAlias.BackColor = System.Drawing.Color.Transparent;
            this.Label_AntiAlias.Dock = System.Windows.Forms.DockStyle.Top;
            this.Label_AntiAlias.Font = new System.Drawing.Font("微软雅黑", 11.25F);
            this.Label_AntiAlias.ForeColor = System.Drawing.Color.White;
            this.Label_AntiAlias.Location = new System.Drawing.Point(0, 0);
            this.Label_AntiAlias.Name = "Label_AntiAlias";
            this.Label_AntiAlias.Size = new System.Drawing.Size(54, 20);
            this.Label_AntiAlias.TabIndex = 0;
            this.Label_AntiAlias.Text = "抗锯齿";
            // 
            // Panel_Tab_About
            // 
            this.Panel_Tab_About.BackColor = System.Drawing.Color.Transparent;
            this.Panel_Tab_About.Controls.Add(this.PictureBox_ApplicationLogo);
            this.Panel_Tab_About.Controls.Add(this.Label_ApplicationName);
            this.Panel_Tab_About.Controls.Add(this.Label_ApplicationEdition);
            this.Panel_Tab_About.Controls.Add(this.Label_Version);
            this.Panel_Tab_About.Controls.Add(this.Label_Copyright);
            this.Panel_Tab_About.Controls.Add(this.Panel_GitHub);
            this.Panel_Tab_About.Location = new System.Drawing.Point(0, 0);
            this.Panel_Tab_About.MinimumSize = new System.Drawing.Size(430, 315);
            this.Panel_Tab_About.Name = "Panel_Tab_About";
            this.Panel_Tab_About.Size = new System.Drawing.Size(435, 420);
            this.Panel_Tab_About.TabIndex = 0;
            this.Panel_Tab_About.Visible = false;
            // 
            // PictureBox_ApplicationLogo
            // 
            this.PictureBox_ApplicationLogo.BackColor = System.Drawing.Color.Transparent;
            this.PictureBox_ApplicationLogo.ErrorImage = null;
            this.PictureBox_ApplicationLogo.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox_ApplicationLogo.Image")));
            this.PictureBox_ApplicationLogo.InitialImage = null;
            this.PictureBox_ApplicationLogo.Location = new System.Drawing.Point(60, 60);
            this.PictureBox_ApplicationLogo.Name = "PictureBox_ApplicationLogo";
            this.PictureBox_ApplicationLogo.Size = new System.Drawing.Size(64, 64);
            this.PictureBox_ApplicationLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PictureBox_ApplicationLogo.TabIndex = 0;
            this.PictureBox_ApplicationLogo.TabStop = false;
            // 
            // Label_ApplicationName
            // 
            this.Label_ApplicationName.AutoSize = true;
            this.Label_ApplicationName.BackColor = System.Drawing.Color.Transparent;
            this.Label_ApplicationName.Font = new System.Drawing.Font("微软雅黑", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Label_ApplicationName.ForeColor = System.Drawing.Color.White;
            this.Label_ApplicationName.Location = new System.Drawing.Point(155, 65);
            this.Label_ApplicationName.Name = "Label_ApplicationName";
            this.Label_ApplicationName.Size = new System.Drawing.Size(212, 31);
            this.Label_ApplicationName.TabIndex = 0;
            this.Label_ApplicationName.Text = "ApplicationName";
            // 
            // Label_ApplicationEdition
            // 
            this.Label_ApplicationEdition.AutoSize = true;
            this.Label_ApplicationEdition.BackColor = System.Drawing.Color.Transparent;
            this.Label_ApplicationEdition.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Label_ApplicationEdition.ForeColor = System.Drawing.Color.White;
            this.Label_ApplicationEdition.Location = new System.Drawing.Point(157, 100);
            this.Label_ApplicationEdition.Name = "Label_ApplicationEdition";
            this.Label_ApplicationEdition.Size = new System.Drawing.Size(149, 21);
            this.Label_ApplicationEdition.TabIndex = 0;
            this.Label_ApplicationEdition.Text = "ApplicationEdition";
            // 
            // Label_Version
            // 
            this.Label_Version.AutoSize = true;
            this.Label_Version.BackColor = System.Drawing.Color.Transparent;
            this.Label_Version.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Label_Version.ForeColor = System.Drawing.Color.White;
            this.Label_Version.Location = new System.Drawing.Point(60, 185);
            this.Label_Version.Name = "Label_Version";
            this.Label_Version.Size = new System.Drawing.Size(88, 19);
            this.Label_Version.TabIndex = 0;
            this.Label_Version.Text = "版本: Version";
            // 
            // Label_Copyright
            // 
            this.Label_Copyright.AutoSize = true;
            this.Label_Copyright.BackColor = System.Drawing.Color.Transparent;
            this.Label_Copyright.Font = new System.Drawing.Font("微软雅黑", 9.75F);
            this.Label_Copyright.ForeColor = System.Drawing.Color.White;
            this.Label_Copyright.Location = new System.Drawing.Point(60, 210);
            this.Label_Copyright.Name = "Label_Copyright";
            this.Label_Copyright.Size = new System.Drawing.Size(310, 19);
            this.Label_Copyright.TabIndex = 0;
            this.Label_Copyright.Text = "Copyright © 2013-2018 chibayuki@foxmail.com";
            // 
            // Panel_GitHub
            // 
            this.Panel_GitHub.BackColor = System.Drawing.Color.Transparent;
            this.Panel_GitHub.Controls.Add(this.Label_GitHub_Part1);
            this.Panel_GitHub.Controls.Add(this.Label_GitHub_Base);
            this.Panel_GitHub.Controls.Add(this.Label_GitHub_Part2);
            this.Panel_GitHub.Controls.Add(this.Label_GitHub_Release);
            this.Panel_GitHub.Location = new System.Drawing.Point(60, 235);
            this.Panel_GitHub.Name = "Panel_GitHub";
            this.Panel_GitHub.Size = new System.Drawing.Size(270, 19);
            this.Panel_GitHub.TabIndex = 0;
            // 
            // Label_GitHub_Part1
            // 
            this.Label_GitHub_Part1.AutoSize = true;
            this.Label_GitHub_Part1.BackColor = System.Drawing.Color.Transparent;
            this.Label_GitHub_Part1.Font = new System.Drawing.Font("微软雅黑", 9.75F);
            this.Label_GitHub_Part1.ForeColor = System.Drawing.Color.White;
            this.Label_GitHub_Part1.Location = new System.Drawing.Point(0, 0);
            this.Label_GitHub_Part1.Name = "Label_GitHub_Part1";
            this.Label_GitHub_Part1.Size = new System.Drawing.Size(113, 19);
            this.Label_GitHub_Part1.TabIndex = 0;
            this.Label_GitHub_Part1.Text = "访问 GitHub 查看";
            // 
            // Label_GitHub_Base
            // 
            this.Label_GitHub_Base.AutoSize = true;
            this.Label_GitHub_Base.BackColor = System.Drawing.Color.Transparent;
            this.Label_GitHub_Base.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Underline);
            this.Label_GitHub_Base.ForeColor = System.Drawing.Color.White;
            this.Label_GitHub_Base.Location = new System.Drawing.Point(113, 0);
            this.Label_GitHub_Base.Name = "Label_GitHub_Base";
            this.Label_GitHub_Base.Size = new System.Drawing.Size(48, 19);
            this.Label_GitHub_Base.TabIndex = 0;
            this.Label_GitHub_Base.Text = "源代码";
            // 
            // Label_GitHub_Part2
            // 
            this.Label_GitHub_Part2.AutoSize = true;
            this.Label_GitHub_Part2.BackColor = System.Drawing.Color.Transparent;
            this.Label_GitHub_Part2.Font = new System.Drawing.Font("微软雅黑", 9.75F);
            this.Label_GitHub_Part2.ForeColor = System.Drawing.Color.White;
            this.Label_GitHub_Part2.Location = new System.Drawing.Point(161, 0);
            this.Label_GitHub_Part2.Name = "Label_GitHub_Part2";
            this.Label_GitHub_Part2.Size = new System.Drawing.Size(22, 19);
            this.Label_GitHub_Part2.TabIndex = 0;
            this.Label_GitHub_Part2.Text = "或";
            // 
            // Label_GitHub_Release
            // 
            this.Label_GitHub_Release.AutoSize = true;
            this.Label_GitHub_Release.BackColor = System.Drawing.Color.Transparent;
            this.Label_GitHub_Release.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Underline);
            this.Label_GitHub_Release.ForeColor = System.Drawing.Color.White;
            this.Label_GitHub_Release.Location = new System.Drawing.Point(183, 0);
            this.Label_GitHub_Release.Name = "Label_GitHub_Release";
            this.Label_GitHub_Release.Size = new System.Drawing.Size(87, 19);
            this.Label_GitHub_Release.TabIndex = 0;
            this.Label_GitHub_Release.Text = "最新发布版本";
            // 
            // Panel_GameUI
            // 
            this.Panel_GameUI.BackColor = System.Drawing.Color.Transparent;
            this.Panel_GameUI.Controls.Add(this.Panel_Current);
            this.Panel_GameUI.Controls.Add(this.Panel_Environment);
            this.Panel_GameUI.Location = new System.Drawing.Point(0, 0);
            this.Panel_GameUI.Name = "Panel_GameUI";
            this.Panel_GameUI.Size = new System.Drawing.Size(585, 420);
            this.Panel_GameUI.TabIndex = 0;
            this.Panel_GameUI.Visible = false;
            // 
            // Panel_Current
            // 
            this.Panel_Current.BackColor = System.Drawing.Color.Transparent;
            this.Panel_Current.Controls.Add(this.Panel_Interrupt);
            this.Panel_Current.Location = new System.Drawing.Point(0, 0);
            this.Panel_Current.Name = "Panel_Current";
            this.Panel_Current.Size = new System.Drawing.Size(585, 50);
            this.Panel_Current.TabIndex = 0;
            this.Panel_Current.Paint += new System.Windows.Forms.PaintEventHandler(this.Panel_Current_Paint);
            // 
            // Panel_Interrupt
            // 
            this.Panel_Interrupt.Controls.Add(this.PictureBox_Erase);
            this.Panel_Interrupt.Controls.Add(this.PictureBox_Undo);
            this.Panel_Interrupt.Controls.Add(this.PictureBox_Redo);
            this.Panel_Interrupt.Controls.Add(this.PictureBox_Interrupt);
            this.Panel_Interrupt.Controls.Add(this.PictureBox_Restart);
            this.Panel_Interrupt.Controls.Add(this.PictureBox_ExitGame);
            this.Panel_Interrupt.Location = new System.Drawing.Point(285, 0);
            this.Panel_Interrupt.Name = "Panel_Interrupt";
            this.Panel_Interrupt.Size = new System.Drawing.Size(300, 50);
            this.Panel_Interrupt.TabIndex = 0;
            // 
            // PictureBox_Erase
            // 
            this.PictureBox_Erase.BackColor = System.Drawing.Color.Transparent;
            this.PictureBox_Erase.ErrorImage = null;
            this.PictureBox_Erase.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox_Erase.Image")));
            this.PictureBox_Erase.InitialImage = null;
            this.PictureBox_Erase.Location = new System.Drawing.Point(0, 0);
            this.PictureBox_Erase.Name = "PictureBox_Erase";
            this.PictureBox_Erase.Size = new System.Drawing.Size(50, 50);
            this.PictureBox_Erase.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox_Erase.TabIndex = 0;
            this.PictureBox_Erase.TabStop = false;
            this.PictureBox_Erase.Paint += new System.Windows.Forms.PaintEventHandler(this.PictureBox_Erase_Paint);
            // 
            // PictureBox_Undo
            // 
            this.PictureBox_Undo.BackColor = System.Drawing.Color.Transparent;
            this.PictureBox_Undo.ErrorImage = null;
            this.PictureBox_Undo.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox_Undo.Image")));
            this.PictureBox_Undo.InitialImage = null;
            this.PictureBox_Undo.Location = new System.Drawing.Point(50, 0);
            this.PictureBox_Undo.Name = "PictureBox_Undo";
            this.PictureBox_Undo.Size = new System.Drawing.Size(50, 50);
            this.PictureBox_Undo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox_Undo.TabIndex = 0;
            this.PictureBox_Undo.TabStop = false;
            // 
            // PictureBox_Redo
            // 
            this.PictureBox_Redo.BackColor = System.Drawing.Color.Transparent;
            this.PictureBox_Redo.ErrorImage = null;
            this.PictureBox_Redo.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox_Redo.Image")));
            this.PictureBox_Redo.InitialImage = null;
            this.PictureBox_Redo.Location = new System.Drawing.Point(100, 0);
            this.PictureBox_Redo.Name = "PictureBox_Redo";
            this.PictureBox_Redo.Size = new System.Drawing.Size(50, 50);
            this.PictureBox_Redo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox_Redo.TabIndex = 0;
            this.PictureBox_Redo.TabStop = false;
            // 
            // PictureBox_Interrupt
            // 
            this.PictureBox_Interrupt.BackColor = System.Drawing.Color.Transparent;
            this.PictureBox_Interrupt.ErrorImage = null;
            this.PictureBox_Interrupt.Image = global::WinFormApp.Properties.Resources.Pause;
            this.PictureBox_Interrupt.InitialImage = null;
            this.PictureBox_Interrupt.Location = new System.Drawing.Point(150, 0);
            this.PictureBox_Interrupt.Name = "PictureBox_Interrupt";
            this.PictureBox_Interrupt.Size = new System.Drawing.Size(50, 50);
            this.PictureBox_Interrupt.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox_Interrupt.TabIndex = 0;
            this.PictureBox_Interrupt.TabStop = false;
            // 
            // PictureBox_Restart
            // 
            this.PictureBox_Restart.BackColor = System.Drawing.Color.Transparent;
            this.PictureBox_Restart.ErrorImage = null;
            this.PictureBox_Restart.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox_Restart.Image")));
            this.PictureBox_Restart.InitialImage = null;
            this.PictureBox_Restart.Location = new System.Drawing.Point(200, 0);
            this.PictureBox_Restart.Name = "PictureBox_Restart";
            this.PictureBox_Restart.Size = new System.Drawing.Size(50, 50);
            this.PictureBox_Restart.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox_Restart.TabIndex = 0;
            this.PictureBox_Restart.TabStop = false;
            // 
            // PictureBox_ExitGame
            // 
            this.PictureBox_ExitGame.BackColor = System.Drawing.Color.Transparent;
            this.PictureBox_ExitGame.ErrorImage = null;
            this.PictureBox_ExitGame.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox_ExitGame.Image")));
            this.PictureBox_ExitGame.InitialImage = null;
            this.PictureBox_ExitGame.Location = new System.Drawing.Point(250, 0);
            this.PictureBox_ExitGame.Name = "PictureBox_ExitGame";
            this.PictureBox_ExitGame.Size = new System.Drawing.Size(50, 50);
            this.PictureBox_ExitGame.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox_ExitGame.TabIndex = 0;
            this.PictureBox_ExitGame.TabStop = false;
            // 
            // Panel_Environment
            // 
            this.Panel_Environment.BackColor = System.Drawing.Color.Transparent;
            this.Panel_Environment.Location = new System.Drawing.Point(0, 50);
            this.Panel_Environment.Name = "Panel_Environment";
            this.Panel_Environment.Size = new System.Drawing.Size(585, 370);
            this.Panel_Environment.TabIndex = 0;
            this.Panel_Environment.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Panel_Environment_KeyDown);
            this.Panel_Environment.Paint += new System.Windows.Forms.PaintEventHandler(this.Panel_Environment_Paint);
            this.Panel_Environment.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Panel_Environment_MouseDown);
            this.Panel_Environment.MouseLeave += new System.EventHandler(this.Panel_Environment_MouseLeave);
            this.Panel_Environment.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Panel_Environment_MouseMove);
            this.Panel_Environment.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Panel_Environment_MouseUp);
            this.Panel_Environment.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.Panel_Environment_MouseWheel);
            // 
            // Timer_EnterPrompt
            // 
            this.Timer_EnterPrompt.Interval = 10;
            this.Timer_EnterPrompt.Tick += new System.EventHandler(this.Timer_EnterPrompt_Tick);
            // 
            // Timer_Timer
            // 
            this.Timer_Timer.Interval = 10;
            this.Timer_Timer.Tick += new System.EventHandler(this.Timer_Timer_Tick);
            // 
            // ToolTip_InterruptPrompt
            // 
            this.ToolTip_InterruptPrompt.ShowAlways = true;
            // 
            // ColorDialog_ThemeColor
            // 
            this.ColorDialog_ThemeColor.Color = System.Drawing.Color.White;
            this.ColorDialog_ThemeColor.FullOpen = true;
            // 
            // BackgroundWorker_LoadGameStep
            // 
            this.BackgroundWorker_LoadGameStep.WorkerReportsProgress = true;
            this.BackgroundWorker_LoadGameStep.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BackgroundWorker_LoadGameStep_DoWork);
            this.BackgroundWorker_LoadGameStep.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.BackgroundWorker_LoadGameStep_ProgressChanged);
            this.BackgroundWorker_LoadGameStep.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.BackgroundWorker_LoadGameStep_RunWorkerCompleted);
            // 
            // BackgroundWorker_SaveGameStep
            // 
            this.BackgroundWorker_SaveGameStep.WorkerReportsProgress = true;
            this.BackgroundWorker_SaveGameStep.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BackgroundWorker_SaveGameStep_DoWork);
            this.BackgroundWorker_SaveGameStep.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.BackgroundWorker_SaveGameStep_ProgressChanged);
            this.BackgroundWorker_SaveGameStep.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.BackgroundWorker_SaveGameStep_RunWorkerCompleted);
            // 
            // Form_Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(585, 420);
            this.Controls.Add(this.Panel_Main);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form_Main";
            this.Opacity = 0D;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.Form_Main_MouseWheel);
            this.Panel_Main.ResumeLayout(false);
            this.Panel_Client.ResumeLayout(false);
            this.Panel_FunctionArea.ResumeLayout(false);
            this.Panel_FunctionAreaOptionsBar.ResumeLayout(false);
            this.Panel_FunctionAreaTab.ResumeLayout(false);
            this.Panel_Tab_Start.ResumeLayout(false);
            this.Panel_EnterGameSelection.ResumeLayout(false);
            this.Panel_Tab_Record.ResumeLayout(false);
            this.Panel_Score.ResumeLayout(false);
            this.Panel_Score.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox_Score)).EndInit();
            this.Panel_GameTime.ResumeLayout(false);
            this.Panel_GameTime.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox_GameTime)).EndInit();
            this.Panel_Tab_Options.ResumeLayout(false);
            this.Panel_Order.ResumeLayout(false);
            this.Panel_Order.PerformLayout();
            this.Panel_DifficultyLevel.ResumeLayout(false);
            this.Panel_DifficultyLevel.PerformLayout();
            this.Panel_ShowNotes.ResumeLayout(false);
            this.Panel_ShowNotes.PerformLayout();
            this.Panel_OperationMode.ResumeLayout(false);
            this.Panel_OperationMode.PerformLayout();
            this.Panel_Save.ResumeLayout(false);
            this.Panel_Save.PerformLayout();
            this.Panel_ThemeColor.ResumeLayout(false);
            this.Panel_ThemeColor.PerformLayout();
            this.Panel_AntiAlias.ResumeLayout(false);
            this.Panel_AntiAlias.PerformLayout();
            this.Panel_Tab_About.ResumeLayout(false);
            this.Panel_Tab_About.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox_ApplicationLogo)).EndInit();
            this.Panel_GitHub.ResumeLayout(false);
            this.Panel_GitHub.PerformLayout();
            this.Panel_GameUI.ResumeLayout(false);
            this.Panel_Current.ResumeLayout(false);
            this.Panel_Interrupt.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox_Erase)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox_Undo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox_Redo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox_Interrupt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox_Restart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox_ExitGame)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel Panel_Main;
        private System.Windows.Forms.Panel Panel_Environment;
        private System.Windows.Forms.Panel Panel_Client;
        private System.Windows.Forms.Panel Panel_Current;
        private System.Windows.Forms.Panel Panel_GameUI;
        private System.Windows.Forms.Panel Panel_FunctionArea;
        private System.Windows.Forms.Panel Panel_FunctionAreaOptionsBar;
        private System.Windows.Forms.Panel Panel_Tab_Record;
        private System.Windows.Forms.Label Label_TotalTime;
        private System.Windows.Forms.Label Label_ThisTime;
        private System.Windows.Forms.Label Label_TotalTimeVal;
        private System.Windows.Forms.Label Label_ThisTimeVal;
        private System.Windows.Forms.Panel Panel_Tab_About;
        private System.Windows.Forms.Label Label_ApplicationEdition;
        private System.Windows.Forms.Label Label_Copyright;
        private System.Windows.Forms.Label Label_Version;
        private System.Windows.Forms.Label Label_ApplicationName;
        private System.Windows.Forms.PictureBox PictureBox_ApplicationLogo;
        private System.Windows.Forms.Panel Panel_Tab_Options;
        private System.Windows.Forms.Panel Panel_Tab_Start;
        private System.Windows.Forms.Panel Panel_EnterGameSelection;
        private System.Windows.Forms.Timer Timer_EnterPrompt;
        private System.Windows.Forms.Panel Panel_GameTime;
        private System.Windows.Forms.Timer Timer_Timer;
        private System.Windows.Forms.ToolTip ToolTip_InterruptPrompt;
        private System.Windows.Forms.Panel Panel_FunctionAreaTab;
        private System.Windows.Forms.Panel Panel_Interrupt;
        private System.Windows.Forms.PictureBox PictureBox_GameTime;
        private System.Windows.Forms.Panel Panel_AntiAlias;
        private System.Windows.Forms.CheckBox CheckBox_AntiAlias;
        private System.Windows.Forms.Label Label_AntiAlias;
        private System.Windows.Forms.Panel Panel_Score;
        private System.Windows.Forms.PictureBox PictureBox_Score;
        private System.Windows.Forms.Label Label_ThisRecord;
        private System.Windows.Forms.Label Label_ThisRecordVal_GameTime;
        private System.Windows.Forms.Label Label_ThisRecordVal_StepCount;
        private System.Windows.Forms.Label Label_BestRecord;
        private System.Windows.Forms.Label Label_BestRecordVal_GameTime;
        private System.Windows.Forms.Label Label_BestRecordVal_StepCount;
        private System.Windows.Forms.Panel Panel_DifficultyLevel;
        private System.Windows.Forms.Label Label_DifficultyLevel;
        private System.Windows.Forms.Panel Panel_DifficultyLevelAdjustment;
        private System.Windows.Forms.Label Label_DifficultyLevel_Val;
        private System.Windows.Forms.Label Label_Easy;
        private System.Windows.Forms.Label Label_Difficult;
        private System.Windows.Forms.Panel Panel_ShowNotes;
        private System.Windows.Forms.CheckBox CheckBox_ShowNotes;
        private System.Windows.Forms.Label Label_ShowNotes;
        private System.Windows.Forms.Label Label_ShowNotes_Info;
        private System.Windows.Forms.Panel Panel_OperationMode;
        private System.Windows.Forms.Label Label_OperationMode;
        private System.Windows.Forms.RadioButton RadioButton_Keyboard;
        private System.Windows.Forms.CheckBox CheckBox_AlwaysEnableKeyboard;
        private System.Windows.Forms.RadioButton RadioButton_Mouse;
        private System.Windows.Forms.RadioButton RadioButton_Touch;
        private System.Windows.Forms.Panel Panel_Order;
        private System.Windows.Forms.Label Label_Order;
        private System.Windows.Forms.RadioButton RadioButton_Order3;
        private System.Windows.Forms.RadioButton RadioButton_Order4;
        private System.Windows.Forms.RadioButton RadioButton_Order5;
        private System.Windows.Forms.ColorDialog ColorDialog_ThemeColor;
        private System.Windows.Forms.Panel Panel_ThemeColor;
        private System.Windows.Forms.Label Label_ThemeColor;
        private System.Windows.Forms.RadioButton RadioButton_UseRandomThemeColor;
        private System.Windows.Forms.RadioButton RadioButton_UseCustomColor;
        private System.Windows.Forms.Label Label_ThemeColorName;
        private System.ComponentModel.BackgroundWorker BackgroundWorker_LoadGameStep;
        private System.ComponentModel.BackgroundWorker BackgroundWorker_SaveGameStep;
        private System.Windows.Forms.Panel Panel_Save;
        private System.Windows.Forms.Label Label_Save;
        private System.Windows.Forms.RadioButton RadioButton_SaveEveryStep;
        private System.Windows.Forms.Label Label_TooSlow;
        private System.Windows.Forms.Label Label_CleanGameStep;
        private System.Windows.Forms.Label Label_CleanGameStepDone;
        private System.Windows.Forms.RadioButton RadioButton_SaveLastStep;
        private System.Windows.Forms.Label Label_Tab_Start;
        private System.Windows.Forms.Label Label_Tab_Record;
        private System.Windows.Forms.Label Label_Tab_Options;
        private System.Windows.Forms.Label Label_Tab_About;
        private System.Windows.Forms.PictureBox PictureBox_Erase;
        private System.Windows.Forms.PictureBox PictureBox_Interrupt;
        private System.Windows.Forms.PictureBox PictureBox_Restart;
        private System.Windows.Forms.PictureBox PictureBox_ExitGame;
        private System.Windows.Forms.PictureBox PictureBox_Undo;
        private System.Windows.Forms.PictureBox PictureBox_Redo;
        private System.Windows.Forms.Label Label_StartNewGame;
        private System.Windows.Forms.Label Label_ContinueLastGame;
        private System.Windows.Forms.Panel Panel_GitHub;
        private System.Windows.Forms.Label Label_GitHub_Part1;
        private System.Windows.Forms.Label Label_GitHub_Base;
        private System.Windows.Forms.Label Label_GitHub_Part2;
        private System.Windows.Forms.Label Label_GitHub_Release;
    }
}