﻿/* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
Copyright © 2013-2018 chibayuki@foxmail.com

数独
Version 7.1.17000.4433.R12.180606-0000

This file is part of 数独

数独 is released under the GPLv3 license
* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Diagnostics;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.IO;
using System.Text.RegularExpressions;

namespace WinFormApp
{
    public partial class Form_Main : Form
    {
        #region 版本信息

        private static readonly string ApplicationName = Application.ProductName; // 程序名。
        private static readonly string ApplicationEdition = "7.1.12"; // 程序版本。

        private static readonly Int32 MajorVersion = new Version(Application.ProductVersion).Major; // 主版本。
        private static readonly Int32 MinorVersion = new Version(Application.ProductVersion).Minor; // 副版本。
        private static readonly Int32 BuildNumber = new Version(Application.ProductVersion).Build; // 版本号。
        private static readonly Int32 BuildRevision = new Version(Application.ProductVersion).Revision; // 修订版本。
        private static readonly string LabString = "R12"; // 分支名。
        private static readonly string BuildTime = "180606-0000"; // 编译时间。

        //

        private static readonly string RootDir_Product = Environment.SystemDirectory.Substring(0, 1) + @":\ProgramData\AppConfig\sudoku"; // 根目录：此产品。
        private static readonly string RootDir_CurrentVersion = RootDir_Product + "\\" + BuildNumber + "." + BuildRevision; // 根目录：当前版本。

        private static readonly string ConfigFileDir = RootDir_CurrentVersion + @"\Config"; // 配置文件所在目录。
        private static readonly string ConfigFilePath = ConfigFileDir + @"\settings.cfg"; // 配置文件路径。

        private static readonly string LogFileDir = RootDir_CurrentVersion + @"\Log"; // 存档文件所在目录。
        private static readonly string DataFilePath = LogFileDir + @"\userdata.cfg"; // 用户数据文件路径（包含最佳成绩与游戏时长）。
        private static readonly string RecordFilePath = LogFileDir + @"\lastgame.cfg"; // 上次游戏文件路径（包含最后一次游戏记录）。

        //

        private static readonly List<Version> OldVersionList = new List<Version> // 兼容的版本列表，用于从最新的兼容版本迁移配置设置。
        {
            new Version(7, 1, 17000, 0),
            new Version(7, 1, 17000, 119),
            new Version(7, 1, 17000, 178),
            new Version(7, 1, 17000, 544),
            new Version(7, 1, 17000, 3030),
            new Version(7, 1, 17000, 3736),
            new Version(7, 1, 17000, 3982),
            new Version(7, 1, 17000, 4189),
            new Version(7, 1, 17000, 4234),
            new Version(7, 1, 17000, 4279),/*
            new Version(7, 1, 17000, 4433)*/
        };

        //

        private static readonly string URL_GitHub_Base = @"https://github.com/chibayuki/Sudoku"; // 此项目在 GitHub 的 URL。
        private static readonly string URL_GitHub_Release = URL_GitHub_Base + @"/releases"; // 此项目的发布版本在 GitHub 的 URL。

        #endregion

        #region 配置设置变量

        private enum Orders { Order3 = 3, Order4 = 4, Order5 = 5, MIN = 3, MAX = 5 } // 数独的阶数枚举。【注意】为避免枚举与整数或字符串之间的转换发生意外，应始终将表示范围的枚举 MIN 与 MAX 置于最后。
        private Orders Order = Orders.Order3; // 当前数独的阶数。

        //

        private const Int32 DifficultyLevel_MAX = 9; // 最大难度等级。
        private const Int32 DifficultyLevel_MIN = 1; // 最小难度等级。
        private Int32 DifficultyLevel = 3; // 当前难度等级。

        //

        private bool ShowNotes = false; // 是否显示提示。

        //

        private enum OperationModes { NULL = -1, Mouse, Touch, Keyboard, COUNT } // 操作方式枚举。
        private OperationModes OperationMode = OperationModes.Mouse; // 当前操作方式。

        private bool AlwaysEnableKeyboard = true; // 是否即使选择其他操作方式，键盘仍然可用

        //

        private bool SaveEveryStep = true; // 退出游戏时是否自动保存所有步骤而不是仅最后一步。

        //

        private const Com.WinForm.Theme Theme_DEFAULT = Com.WinForm.Theme.Colorful; // 主题的默认值。

        private bool UseRandomThemeColor = true; // 是否使用随机的主题颜色。

        private static readonly Color ThemeColor_DEFAULT = Color.Gray; // 主题颜色的默认值。

        private const bool ShowFormTitleColor_DEFAULT = true; // 是否显示窗体标题栏的颜色的默认值。

        private const double Opacity_MIN = 0.05; // 总体不透明度的最小值。
        private const double Opacity_MAX = 1.0; // 总体不透明度的最大值。

        //

        private bool AntiAlias = true; // 是否使用抗锯齿模式绘图。

        #endregion

        #region 元素矩阵变量

        private const Int32 CAPACITY = 25; // 元素矩阵容量的平方根。

        private Size Range => new Size(SudokuSize, SudokuSize); // 当前界面布局（以元素数为单位）。

        private Int32[,] ElementArray = new Int32[CAPACITY, CAPACITY]; // 元素矩阵。

        private List<Point> ElementIndexList = new List<Point>(CAPACITY * CAPACITY); // 元素索引列表。

        //

        private Int32 ElementSize = 60; // 元素边长。

        #endregion

        #region 游戏变量

        private static readonly Size FormClientInitialSize = new Size(585, 420); // 窗体工作区初始大小。

        //

        private Color GameUIBackColor_DEC => Me.RecommendColors.Background_DEC.ToColor(); // 游戏 UI 背景颜色（浅色）。
        private Color GameUIBackColor_INC => Me.RecommendColors.Background_INC.ToColor(); // 游戏 UI 背景颜色（深色）。

        //

        private bool GameIsWin = false; // 游戏是否已经完成。

        //

        private TimeSpan ThisGameTime = TimeSpan.Zero; // 本次游戏时长。
        private TimeSpan TotalGameTime = TimeSpan.Zero; // 累计游戏时长。

        //

        private struct Record // 记录。
        {
            public Int32 OrderValue; // 数独阶数的数值。
            public Int32 DifficultyLevel; // 难度等级。
            public bool ShowNotes; // 是否显示提示。

            public TimeSpan GameTime; // 游戏用时。
            public Int32 StepCount; // 操作次数。
        }

        private Record[,] BestRecordArray = new Record[(Int32)Orders.MAX - (Int32)Orders.MIN + 1, DifficultyLevel_MAX - DifficultyLevel_MIN + 1]; // 最高分记录数组。

        private Record BestRecord // 获取或设置当前数独阶数与难度等级下的最高分记录。
        {
            get
            {
                return BestRecordArray[(Int32)Order - (Int32)Orders.MIN, DifficultyLevel - DifficultyLevel_MIN];
            }

            set
            {
                BestRecordArray[(Int32)Order - (Int32)Orders.MIN, DifficultyLevel - DifficultyLevel_MIN] = value;
            }
        }

        private Record ThisRecord = new Record(); // 本次记录。

        //

        private Record Record_Last = new Record(); // 上次游戏的记录。

        private Int32[,] ElementArray_Last = new Int32[CAPACITY, CAPACITY]; // 上次游戏的元素矩阵。

        private List<Point> ElementIndexList_Last = new List<Point>(CAPACITY * CAPACITY); // 上次游戏的元素索引列表。

        private bool[,] SolidFlagTable_Last = new bool[CAPACITY, CAPACITY]; // 上次游戏的固态标志表。

        private string StepListString = string.Empty; // 上次游戏中表示操作步骤列表的字符串。

        #endregion

        #region 计时器数据

        private struct CycData // 计时周期数据。
        {
            private Int32 _Tick0;
            private Int32 _Tick1;

            public double DeltaMS // 当前周期的毫秒数。
            {
                get
                {
                    return Math.Abs(_Tick0 - _Tick1);
                }
            }

            private Int32 _Cnt; // 周期计数。
            public Int32 Cnt
            {
                get
                {
                    return _Cnt;
                }
            }

            private double _Avg_Am; // 周期毫秒数的算数平均值。
            public double Avg_Am
            {
                get
                {
                    return _Avg_Am;
                }
            }

            private double _Avg_St; // 周期毫秒数的统计平均值。
            public double Avg_St
            {
                get
                {
                    return _Avg_St;
                }
            }

            public void Reset() // 重置此结构。
            {
                _Tick0 = _Tick1 = Environment.TickCount;
                _Cnt = 0;
                _Avg_Am = _Avg_St = 0;
            }

            public void Update() // 更新此结构。
            {
                if (_Tick0 <= _Tick1)
                {
                    _Tick0 = Environment.TickCount;
                }
                else
                {
                    _Tick1 = Environment.TickCount;
                }

                _Cnt = Math.Min(1048576, _Cnt + 1);

                _Avg_Am = (_Avg_Am + DeltaMS) / 2;
                _Avg_St = _Avg_St * (_Cnt - 1) / _Cnt + DeltaMS / _Cnt;
            }
        }

        #endregion

        #region 窗体构造

        private Com.WinForm.FormManager Me;

        public Com.WinForm.FormManager FormManager
        {
            get
            {
                return Me;
            }
        }

        protected override CreateParams CreateParams
        {
            get
            {
                const int WS_MINIMIZEBOX = 0x00020000;

                CreateParams CP = base.CreateParams;

                if (Me != null && Me.FormStyle != Com.WinForm.FormStyle.Dialog)
                {
                    CP.Style = CP.Style | WS_MINIMIZEBOX;
                }

                return CP;
            }
        }

        private void _Ctor(Com.WinForm.FormManager owner)
        {
            InitializeComponent();

            //

            if (owner != null)
            {
                Me = new Com.WinForm.FormManager(this, owner);
            }
            else
            {
                Me = new Com.WinForm.FormManager(this);
            }

            //

            FormDefine();
        }

        public Form_Main()
        {
            _Ctor(null);
        }

        public Form_Main(Com.WinForm.FormManager owner)
        {
            _Ctor(owner);
        }

        private void FormDefine()
        {
            Me.Caption = ApplicationName;
            Me.FormStyle = Com.WinForm.FormStyle.Sizable;
            Me.EnableFullScreen = true;
            Me.ClientSize = FormClientInitialSize;
            Me.Theme = Theme_DEFAULT;
            Me.ThemeColor = new Com.ColorX(ThemeColor_DEFAULT);
            Me.ShowCaptionBarColor = ShowFormTitleColor_DEFAULT;

            Me.Loading += LoadingEvents;
            Me.Loaded += LoadedEvents;
            Me.Closed += ClosedEvents;
            Me.Resize += ResizeEvents;
            Me.SizeChanged += SizeChangedEvents;
            Me.FormStateChanged += FormStateChangedEvents;
            Me.ThemeChanged += ThemeColorChangedEvents;
            Me.ThemeColorChanged += ThemeColorChangedEvents;
        }

        #endregion

        #region 窗体事件

        private void LoadingEvents(object sender, EventArgs e)
        {
            //
            // 在窗体加载时发生。
            //

            TransConfig();

            DelOldConfig();

            LoadConfig();

            LoadUserData();

            LoadLastGame();

            //

            if (UseRandomThemeColor)
            {
                Me.ThemeColor = Com.ColorManipulation.GetRandomColorX();
            }
        }

        private void LoadedEvents(object sender, EventArgs e)
        {
            //
            // 在窗体加载后发生。
            //

            Me.OnSizeChanged();
            Me.OnThemeChanged();

            //

            RadioButton_Order3.CheckedChanged -= RadioButton_Order3_CheckedChanged;
            RadioButton_Order4.CheckedChanged -= RadioButton_Order4_CheckedChanged;
            RadioButton_Order5.CheckedChanged -= RadioButton_Order5_CheckedChanged;

            switch (Order)
            {
                case Orders.Order3: RadioButton_Order3.Checked = true; break;
                case Orders.Order4: RadioButton_Order4.Checked = true; break;
                case Orders.Order5: RadioButton_Order5.Checked = true; break;
            }

            RadioButton_Order3.CheckedChanged += RadioButton_Order3_CheckedChanged;
            RadioButton_Order4.CheckedChanged += RadioButton_Order4_CheckedChanged;
            RadioButton_Order5.CheckedChanged += RadioButton_Order5_CheckedChanged;

            //

            CheckBox_ShowNotes.CheckedChanged -= CheckBox_ShowNotes_CheckedChanged;

            CheckBox_ShowNotes.Checked = ShowNotes;

            CheckBox_ShowNotes.CheckedChanged += CheckBox_ShowNotes_CheckedChanged;

            Label_ShowNotes_Info.Enabled = ShowNotes;

            //

            RadioButton_Mouse.CheckedChanged -= RadioButton_Mouse_CheckedChanged;
            RadioButton_Touch.CheckedChanged -= RadioButton_Touch_CheckedChanged;
            RadioButton_Keyboard.CheckedChanged -= RadioButton_Keyboard_CheckedChanged;

            switch (OperationMode)
            {
                case OperationModes.Mouse: RadioButton_Mouse.Checked = true; break;
                case OperationModes.Touch: RadioButton_Touch.Checked = true; break;
                case OperationModes.Keyboard: RadioButton_Keyboard.Checked = true; break;
            }

            RadioButton_Mouse.CheckedChanged += RadioButton_Mouse_CheckedChanged;
            RadioButton_Touch.CheckedChanged += RadioButton_Touch_CheckedChanged;
            RadioButton_Keyboard.CheckedChanged += RadioButton_Keyboard_CheckedChanged;

            CheckBox_AlwaysEnableKeyboard.CheckedChanged -= CheckBox_AlwaysEnableKeyboard_CheckedChanged;

            CheckBox_AlwaysEnableKeyboard.Checked = AlwaysEnableKeyboard;

            CheckBox_AlwaysEnableKeyboard.CheckedChanged += CheckBox_AlwaysEnableKeyboard_CheckedChanged;

            //

            RadioButton_SaveEveryStep.CheckedChanged -= RadioButton_SaveEveryStep_CheckedChanged;
            RadioButton_SaveLastStep.CheckedChanged -= RadioButton_SaveLastStep_CheckedChanged;

            if (SaveEveryStep)
            {
                RadioButton_SaveEveryStep.Checked = true;
            }
            else
            {
                RadioButton_SaveLastStep.Checked = true;
            }

            RadioButton_SaveEveryStep.CheckedChanged += RadioButton_SaveEveryStep_CheckedChanged;
            RadioButton_SaveLastStep.CheckedChanged += RadioButton_SaveLastStep_CheckedChanged;

            //

            RadioButton_UseRandomThemeColor.CheckedChanged -= RadioButton_UseRandomThemeColor_CheckedChanged;
            RadioButton_UseCustomColor.CheckedChanged -= RadioButton_UseCustomColor_CheckedChanged;

            if (UseRandomThemeColor)
            {
                RadioButton_UseRandomThemeColor.Checked = true;
            }
            else
            {
                RadioButton_UseCustomColor.Checked = true;
            }

            RadioButton_UseRandomThemeColor.CheckedChanged += RadioButton_UseRandomThemeColor_CheckedChanged;
            RadioButton_UseCustomColor.CheckedChanged += RadioButton_UseCustomColor_CheckedChanged;

            Label_ThemeColorName.Enabled = !UseRandomThemeColor;

            //

            CheckBox_AntiAlias.CheckedChanged -= CheckBox_AntiAlias_CheckedChanged;

            CheckBox_AntiAlias.Checked = AntiAlias;

            CheckBox_AntiAlias.CheckedChanged += CheckBox_AntiAlias_CheckedChanged;

            //

            Label_ApplicationName.Text = ApplicationName;
            Label_ApplicationEdition.Text = ApplicationEdition;
            Label_Version.Text = "版本: " + MajorVersion + "." + MinorVersion + "." + BuildNumber + "." + BuildRevision;

            //

            Com.WinForm.ControlSubstitution.LabelAsButton(Label_StartNewGame, Label_StartNewGame_Click);
            Com.WinForm.ControlSubstitution.LabelAsButton(Label_ContinueLastGame, Label_ContinueLastGame_Click);

            //

            FunctionAreaTab = FunctionAreaTabs.Start;
        }

        private void ClosedEvents(object sender, EventArgs e)
        {
            //
            // 在窗体关闭后发生。
            //

            SaveConfig();

            if (GameUINow)
            {
                Interrupt(InterruptActions.CloseApp);
            }
        }

        private void ResizeEvents(object sender, EventArgs e)
        {
            //
            // 在窗体的大小调整时发生。
            //

            Panel_FunctionArea.Size = Panel_GameUI.Size = Panel_Client.Size = Panel_Main.Size;

            Panel_FunctionAreaOptionsBar.Size = new Size(Panel_FunctionArea.Width / 3, Panel_FunctionArea.Height);
            Label_Tab_Start.Size = Label_Tab_Record.Size = Label_Tab_Options.Size = Label_Tab_About.Size = new Size(Panel_FunctionAreaOptionsBar.Width, Panel_FunctionAreaOptionsBar.Height / 4);
            Label_Tab_Record.Top = Label_Tab_Start.Bottom;
            Label_Tab_Options.Top = Label_Tab_Record.Bottom;
            Label_Tab_About.Top = Label_Tab_Options.Bottom;

            Panel_FunctionAreaTab.Left = Panel_FunctionAreaOptionsBar.Right;
            Panel_FunctionAreaTab.Size = new Size(Panel_FunctionArea.Width - Panel_FunctionAreaOptionsBar.Width, Panel_FunctionArea.Height);

            Func<Control, Control, Size> GetTabSize = (Tab, Container) => new Size(Container.Width - (Container.Height < Tab.MinimumSize.Height ? 25 : 0), Container.Height - (Container.Width < Tab.MinimumSize.Width ? 25 : 0));

            Panel_Tab_Start.Size = GetTabSize(Panel_Tab_Start, Panel_FunctionAreaTab);
            Panel_Tab_Record.Size = GetTabSize(Panel_Tab_Record, Panel_FunctionAreaTab);
            Panel_Tab_Options.Size = GetTabSize(Panel_Tab_Options, Panel_FunctionAreaTab);
            Panel_Tab_About.Size = GetTabSize(Panel_Tab_About, Panel_FunctionAreaTab);

            //

            Panel_EnterGameSelection.Location = new Point((Panel_Tab_Start.Width - Panel_EnterGameSelection.Width) / 2, (Panel_Tab_Start.Height - Panel_EnterGameSelection.Height) / 2);

            Panel_Score.Width = Panel_Tab_Record.Width - Panel_Score.Left * 2;
            Panel_Score.Height = Panel_Tab_Record.Height - Panel_Score.Top * 2 - Panel_GameTime.Height;
            Panel_GameTime.Width = Panel_Tab_Record.Width - Panel_GameTime.Left * 2;
            Panel_GameTime.Top = Panel_Score.Bottom;
            Label_ThisRecord.Location = new Point(Math.Max(0, Math.Min(Panel_Score.Width - Label_ThisRecord.Width, (Panel_Score.Width / 2 - Label_ThisRecord.Width) / 2)), Panel_Score.Height - 25 - Label_ThisRecord.Height);
            Label_BestRecord.Location = new Point(Math.Max(0, Math.Min(Panel_Score.Width - Label_BestRecord.Width, Panel_Score.Width / 2 + (Panel_Score.Width / 2 - Label_BestRecord.Width) / 2)), Panel_Score.Height - 25 - Label_BestRecord.Height);

            Panel_Order.Width = Panel_Tab_Options.Width - Panel_Order.Left * 2;
            Panel_DifficultyLevel.Width = Panel_Tab_Options.Width - Panel_DifficultyLevel.Left * 2;
            Panel_ShowNotes.Width = Panel_Tab_Options.Width - Panel_ShowNotes.Left * 2;
            Panel_OperationMode.Width = Panel_Tab_Options.Width - Panel_OperationMode.Left * 2;
            Panel_Save.Width = Panel_Tab_Options.Width - Panel_Save.Left * 2;
            Panel_ThemeColor.Width = Panel_Tab_Options.Width - Panel_ThemeColor.Left * 2;
            Panel_AntiAlias.Width = Panel_Tab_Options.Width - Panel_AntiAlias.Left * 2;

            //

            Panel_Current.Width = Panel_GameUI.Width;

            Panel_Interrupt.Left = Panel_Current.Width - Panel_Interrupt.Width;

            Panel_Environment.Size = new Size(Panel_GameUI.Width, Panel_GameUI.Height - Panel_Environment.Top);
        }

        private void SizeChangedEvents(object sender, EventArgs e)
        {
            //
            // 在窗体的大小更改时发生。
            //

            if (Panel_GameUI.Visible)
            {
                double RW = Range.Width, RH = Range.Height;

                if (Panel_Environment.Height * Range.Width >= Panel_Environment.Width * (Range.Height + CandidateNumberAreaDist + CandidateNumberAreaSize))
                {
                    RH = Range.Height + CandidateNumberAreaDist + CandidateNumberAreaSize;
                }
                else
                {
                    RW = Range.Width + CandidateNumberAreaDist + CandidateNumberAreaSize;
                }

                ElementSize = (Int32)Math.Max(1, Math.Min(Panel_Environment.Width / RW, Panel_Environment.Height / RH));

                EAryBmpRect.Size = new Size((Int32)Math.Max(1, ElementSize * RW), (Int32)Math.Max(1, ElementSize * RH));
                EAryBmpRect.Location = new Point((Panel_Environment.Width - EAryBmpRect.Width) / 2, (Panel_Environment.Height - EAryBmpRect.Height) / 2);

                if (RW > Range.Width)
                {
                    CandidateNumberArea = new Rectangle(new Point((Int32)(EAryBmpRect.X + ElementSize * (Range.Width + CandidateNumberAreaDist)), EAryBmpRect.Y), new Size((Int32)Math.Max(1, ElementSize * CandidateNumberAreaSize), Math.Max(1, ElementSize * Range.Height)));
                }
                else
                {
                    CandidateNumberArea = new Rectangle(new Point(EAryBmpRect.X, (Int32)(EAryBmpRect.Y + ElementSize * (Range.Height + CandidateNumberAreaDist))), new Size(Math.Max(1, ElementSize * Range.Width), (Int32)Math.Max(1, ElementSize * CandidateNumberAreaSize)));
                }

                RepaintCurBmp();

                ElementArray_RepresentAll();
            }

            if (Panel_FunctionArea.Visible && FunctionAreaTab == FunctionAreaTabs.Record)
            {
                Panel_Tab_Record.Refresh();
            }
        }

        private void FormStateChangedEvents(object sender, EventArgs e)
        {
            //
            // 在窗体的状态更改时发生。
            //

            if (Me.FormState == Com.WinForm.FormState.Minimized && Timer_Timer.Enabled)
            {
                Interrupt(InterruptActions.Pause);
            }
        }

        private void ThemeColorChangedEvents(object sender, EventArgs e)
        {
            //
            // 在窗体的主题色更改时发生。
            //

            // 功能区选项卡

            Panel_FunctionArea.BackColor = Me.RecommendColors.Background_DEC.ToColor();
            Panel_FunctionAreaOptionsBar.BackColor = Me.RecommendColors.Main.ToColor();

            FunctionAreaTab = _FunctionAreaTab;

            // "记录"区域

            Label_ThisRecord.ForeColor = Label_BestRecord.ForeColor = Me.RecommendColors.Text.ToColor();
            Label_ThisRecordVal_GameTime.ForeColor = Label_BestRecordVal_GameTime.ForeColor = Me.RecommendColors.Text_INC.ToColor();
            Label_ThisRecordVal_StepCount.ForeColor = Label_BestRecordVal_StepCount.ForeColor = Me.RecommendColors.Text.ToColor();

            Label_ThisTime.ForeColor = Label_TotalTime.ForeColor = Me.RecommendColors.Text.ToColor();
            Label_ThisTimeVal.ForeColor = Label_TotalTimeVal.ForeColor = Me.RecommendColors.Text_INC.ToColor();

            // "选项"区域

            Label_Order.ForeColor = Label_DifficultyLevel.ForeColor = Label_ShowNotes.ForeColor = Label_OperationMode.ForeColor = Label_Save.ForeColor = Label_ThemeColor.ForeColor = Label_AntiAlias.ForeColor = Me.RecommendColors.Text_INC.ToColor();

            RadioButton_Order3.ForeColor = RadioButton_Order4.ForeColor = RadioButton_Order5.ForeColor = Me.RecommendColors.Text.ToColor();

            Label_DifficultyLevel_Val.ForeColor = Label_Easy.ForeColor = Label_Difficult.ForeColor = Me.RecommendColors.Text.ToColor();

            Panel_DifficultyLevelAdjustment.BackColor = Panel_FunctionArea.BackColor;

            CheckBox_ShowNotes.ForeColor = Me.RecommendColors.Text.ToColor();

            Label_ShowNotes_Info.ForeColor = Me.RecommendColors.Text.ToColor();

            RadioButton_Mouse.ForeColor = RadioButton_Touch.ForeColor = RadioButton_Keyboard.ForeColor = Me.RecommendColors.Text.ToColor();

            CheckBox_AlwaysEnableKeyboard.ForeColor = Me.RecommendColors.Text.ToColor();

            RadioButton_SaveEveryStep.ForeColor = RadioButton_SaveLastStep.ForeColor = Me.RecommendColors.Text.ToColor();

            Label_TooSlow.ForeColor = Me.RecommendColors.Text.ToColor();
            Label_CleanGameStep.ForeColor = Me.RecommendColors.Text.ToColor();
            Label_CleanGameStepDone.ForeColor = Me.RecommendColors.Text.ToColor();

            RadioButton_UseRandomThemeColor.ForeColor = RadioButton_UseCustomColor.ForeColor = Me.RecommendColors.Text.ToColor();

            Label_ThemeColorName.Text = Com.ColorManipulation.GetColorName(Me.ThemeColor.ToColor());
            Label_ThemeColorName.ForeColor = Me.RecommendColors.Text.ToColor();

            CheckBox_AntiAlias.ForeColor = Me.RecommendColors.Text.ToColor();

            // "关于"区域

            Label_ApplicationName.ForeColor = Me.RecommendColors.Text_INC.ToColor();
            Label_ApplicationEdition.ForeColor = Label_Version.ForeColor = Label_Copyright.ForeColor = Me.RecommendColors.Text.ToColor();
            Label_GitHub_Part1.ForeColor = Label_GitHub_Base.ForeColor = Label_GitHub_Part2.ForeColor = Label_GitHub_Release.ForeColor = Me.RecommendColors.Text.ToColor();

            // 控件替代

            Com.WinForm.ControlSubstitution.PictureBoxAsButton(PictureBox_Erase, PictureBox_Erase_Click, null, PictureBox_Erase_MouseEnter, null, Color.Transparent, Me.RecommendColors.Button_INC.AtOpacity(50).ToColor(), Me.RecommendColors.Button_INC.AtOpacity(70).ToColor());
            Com.WinForm.ControlSubstitution.PictureBoxAsButton(PictureBox_Undo, PictureBox_Undo_Click, null, PictureBox_Undo_MouseEnter, null, Color.Transparent, Me.RecommendColors.Button_INC.AtOpacity(50).ToColor(), Me.RecommendColors.Button_INC.AtOpacity(70).ToColor());
            Com.WinForm.ControlSubstitution.PictureBoxAsButton(PictureBox_Redo, PictureBox_Redo_Click, null, PictureBox_Redo_MouseEnter, null, Color.Transparent, Me.RecommendColors.Button_INC.AtOpacity(50).ToColor(), Me.RecommendColors.Button_INC.AtOpacity(70).ToColor());
            Com.WinForm.ControlSubstitution.PictureBoxAsButton(PictureBox_Interrupt, PictureBox_Interrupt_Click, null, PictureBox_Interrupt_MouseEnter, null, Color.Transparent, Me.RecommendColors.Button_INC.AtOpacity(50).ToColor(), Me.RecommendColors.Button_INC.AtOpacity(70).ToColor());
            Com.WinForm.ControlSubstitution.PictureBoxAsButton(PictureBox_Restart, PictureBox_Restart_Click, null, PictureBox_Restart_MouseEnter, null, Color.Transparent, Me.RecommendColors.Button_INC.AtOpacity(50).ToColor(), Me.RecommendColors.Button_INC.AtOpacity(70).ToColor());
            Com.WinForm.ControlSubstitution.PictureBoxAsButton(PictureBox_ExitGame, PictureBox_ExitGame_Click, null, PictureBox_ExitGame_MouseEnter, null, Color.Transparent, Me.RecommendColors.Button_INC.AtOpacity(50).ToColor(), Me.RecommendColors.Button_INC.AtOpacity(70).ToColor());

            Com.WinForm.ControlSubstitution.LabelAsButton(Label_CleanGameStep, Label_CleanGameStep_Click, Color.Transparent, Me.RecommendColors.Button_DEC.ToColor(), Me.RecommendColors.Button_INC.ToColor(), new Font("微软雅黑", 9.75F, FontStyle.Underline, GraphicsUnit.Point, 134), new Font("微软雅黑", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 134), new Font("微软雅黑", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 134));
            Com.WinForm.ControlSubstitution.LabelAsButton(Label_ThemeColorName, Label_ThemeColorName_Click, Color.Transparent, Me.RecommendColors.Button_DEC.ToColor(), Me.RecommendColors.Button_INC.ToColor(), new Font("微软雅黑", 9.75F, FontStyle.Underline, GraphicsUnit.Point, 134), new Font("微软雅黑", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 134), new Font("微软雅黑", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 134));

            Com.WinForm.ControlSubstitution.LabelAsButton(Label_GitHub_Base, Label_GitHub_Base_Click, Color.Transparent, Me.RecommendColors.Button_DEC.ToColor(), Me.RecommendColors.Button_INC.ToColor(), new Font("微软雅黑", 9.75F, FontStyle.Underline, GraphicsUnit.Point, 134), new Font("微软雅黑", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 134), new Font("微软雅黑", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 134));
            Com.WinForm.ControlSubstitution.LabelAsButton(Label_GitHub_Release, Label_GitHub_Release_Click, Color.Transparent, Me.RecommendColors.Button_DEC.ToColor(), Me.RecommendColors.Button_INC.ToColor(), new Font("微软雅黑", 9.75F, FontStyle.Underline, GraphicsUnit.Point, 134), new Font("微软雅黑", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 134), new Font("微软雅黑", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 134));
        }

        #endregion

        #region 背景绘图

        private void Panel_FunctionAreaOptionsBar_Paint(object sender, PaintEventArgs e)
        {
            //
            // Panel_FunctionAreaOptionsBar 绘图。
            //

            Graphics Grap = e.Graphics;
            Grap.SmoothingMode = SmoothingMode.AntiAlias;

            //

            Control[] TabCtrl = new Control[(Int32)FunctionAreaTabs.COUNT] { Label_Tab_Start, Label_Tab_Record, Label_Tab_Options, Label_Tab_About };

            List<bool> TabBtnPointed = new List<bool>(TabCtrl.Length);
            List<bool> TabBtnSeld = new List<bool>(TabCtrl.Length);

            for (int i = 0; i < TabCtrl.Length; i++)
            {
                TabBtnPointed.Add(Com.Geometry.CursorIsInControl(TabCtrl[i]));
                TabBtnSeld.Add(FunctionAreaTab == (FunctionAreaTabs)i);
            }

            Color TabBtnCr_Bk_Pointed = Color.FromArgb(128, Color.White), TabBtnCr_Bk_Seld = Color.FromArgb(192, Color.White), TabBtnCr_Bk_Uns = Color.FromArgb(64, Color.White);

            for (int i = 0; i < TabCtrl.Length; i++)
            {
                Color TabBtnCr_Bk = (TabBtnSeld[i] ? TabBtnCr_Bk_Seld : (TabBtnPointed[i] ? TabBtnCr_Bk_Pointed : TabBtnCr_Bk_Uns));

                GraphicsPath Path_TabBtn = new GraphicsPath();
                Path_TabBtn.AddRectangle(TabCtrl[i].Bounds);
                PathGradientBrush PGB_TabBtn = new PathGradientBrush(Path_TabBtn)
                {
                    CenterColor = Color.FromArgb(TabBtnCr_Bk.A / 2, TabBtnCr_Bk),
                    SurroundColors = new Color[] { TabBtnCr_Bk },
                    FocusScales = new PointF(1F, 0F)
                };
                Grap.FillPath(PGB_TabBtn, Path_TabBtn);
                Path_TabBtn.Dispose();
                PGB_TabBtn.Dispose();

                if (TabBtnSeld[i])
                {
                    PointF[] Polygon = new PointF[] { new PointF(TabCtrl[i].Right, TabCtrl[i].Top + TabCtrl[i].Height / 4), new PointF(TabCtrl[i].Right - TabCtrl[i].Height / 4, TabCtrl[i].Top + TabCtrl[i].Height / 2), new PointF(TabCtrl[i].Right, TabCtrl[i].Bottom - TabCtrl[i].Height / 4) };

                    Grap.FillPolygon(new SolidBrush(Panel_FunctionArea.BackColor), Polygon);
                }
            }
        }

        private void Panel_Score_Paint(object sender, PaintEventArgs e)
        {
            //
            // Panel_Score 绘图。
            //

            Control Cntr = sender as Control;

            if (Cntr != null)
            {
                Pen P = new Pen(Me.RecommendColors.Border_DEC.ToColor(), 1);
                Control Ctrl = PictureBox_Score;
                e.Graphics.DrawLine(P, new Point(Ctrl.Right, Ctrl.Top + Ctrl.Height / 2), new Point(Cntr.Width, Ctrl.Top + Ctrl.Height / 2));
                P.Dispose();
            }

            //

            PaintScore(e);
        }

        private void Panel_GameTime_Paint(object sender, PaintEventArgs e)
        {
            //
            // Panel_GameTime 绘图。
            //

            Control Cntr = sender as Control;

            if (Cntr != null)
            {
                Pen P = new Pen(Me.RecommendColors.Border_DEC.ToColor(), 1);
                Control Ctrl = PictureBox_GameTime;
                e.Graphics.DrawLine(P, new Point(Ctrl.Right, Ctrl.Top + Ctrl.Height / 2), new Point(Cntr.Width, Ctrl.Top + Ctrl.Height / 2));
                P.Dispose();
            }
        }

        private void Panel_Order_Paint(object sender, PaintEventArgs e)
        {
            //
            // Panel_Order 绘图。
            //

            Control Cntr = sender as Control;

            if (Cntr != null)
            {
                Pen P = new Pen(Me.RecommendColors.Border_DEC.ToColor(), 1);
                Control Ctrl = Label_Order;
                e.Graphics.DrawLine(P, new Point(Ctrl.Right, Ctrl.Top + Ctrl.Height / 2), new Point(Cntr.Width, Ctrl.Top + Ctrl.Height / 2));
                P.Dispose();
            }
        }

        private void Panel_DifficultyLevel_Paint(object sender, PaintEventArgs e)
        {
            //
            // Panel_DifficultyLevel 绘图。
            //

            Control Cntr = sender as Control;

            if (Cntr != null)
            {
                Pen P = new Pen(Me.RecommendColors.Border_DEC.ToColor(), 1);
                Control Ctrl = Label_DifficultyLevel;
                e.Graphics.DrawLine(P, new Point(Ctrl.Right, Ctrl.Top + Ctrl.Height / 2), new Point(Cntr.Width, Ctrl.Top + Ctrl.Height / 2));
                P.Dispose();
            }
        }

        private void Panel_ShowNotes_Paint(object sender, PaintEventArgs e)
        {
            //
            // Panel_ShowNotes 绘图。
            //

            Control Cntr = sender as Control;

            if (Cntr != null)
            {
                Pen P = new Pen(Me.RecommendColors.Border_DEC.ToColor(), 1);
                Control Ctrl = Label_ShowNotes;
                e.Graphics.DrawLine(P, new Point(Ctrl.Right, Ctrl.Top + Ctrl.Height / 2), new Point(Cntr.Width, Ctrl.Top + Ctrl.Height / 2));
                P.Dispose();
            }
        }

        private void Panel_OperationMode_Paint(object sender, PaintEventArgs e)
        {
            //
            // Panel_OperationMode 绘图。
            //

            Control Cntr = sender as Control;

            if (Cntr != null)
            {
                Pen P = new Pen(Me.RecommendColors.Border_DEC.ToColor(), 1);
                Control Ctrl = Label_OperationMode;
                e.Graphics.DrawLine(P, new Point(Ctrl.Right, Ctrl.Top + Ctrl.Height / 2), new Point(Cntr.Width, Ctrl.Top + Ctrl.Height / 2));
                P.Dispose();
            }
        }

        private void Panel_Save_Paint(object sender, PaintEventArgs e)
        {
            //
            // Panel_Save 绘图。
            //

            Control Cntr = sender as Control;

            if (Cntr != null)
            {
                Pen P = new Pen(Me.RecommendColors.Border_DEC.ToColor(), 1);
                Control Ctrl = Label_Save;
                e.Graphics.DrawLine(P, new Point(Ctrl.Right, Ctrl.Top + Ctrl.Height / 2), new Point(Cntr.Width, Ctrl.Top + Ctrl.Height / 2));
                P.Dispose();
            }
        }

        private void Panel_ThemeColor_Paint(object sender, PaintEventArgs e)
        {
            //
            // Panel_ThemeColor 绘图。
            //

            Control Cntr = sender as Control;

            if (Cntr != null)
            {
                Pen P = new Pen(Me.RecommendColors.Border_DEC.ToColor(), 1);
                Control Ctrl = Label_ThemeColor;
                e.Graphics.DrawLine(P, new Point(Ctrl.Right, Ctrl.Top + Ctrl.Height / 2), new Point(Cntr.Width, Ctrl.Top + Ctrl.Height / 2));
                P.Dispose();
            }
        }

        private void Panel_AntiAlias_Paint(object sender, PaintEventArgs e)
        {
            //
            // Panel_AntiAlias 绘图。
            //

            Control Cntr = sender as Control;

            if (Cntr != null)
            {
                Pen P = new Pen(Me.RecommendColors.Border_DEC.ToColor(), 1);
                Control Ctrl = Label_AntiAlias;
                e.Graphics.DrawLine(P, new Point(Ctrl.Right, Ctrl.Top + Ctrl.Height / 2), new Point(Cntr.Width, Ctrl.Top + Ctrl.Height / 2));
                P.Dispose();
            }
        }

        #endregion

        #region 配置设置

        private void TransConfig()
        {
            //
            // 从当前内部版本号下最近的旧版本迁移配置文件。
            //

            try
            {
                if (!Directory.Exists(RootDir_CurrentVersion))
                {
                    if (OldVersionList.Count > 0)
                    {
                        List<Version> OldVersionList_Copy = new List<Version>(OldVersionList);
                        List<Version> OldVersionList_Sorted = new List<Version>(OldVersionList_Copy.Count);

                        while (OldVersionList_Copy.Count > 0)
                        {
                            Version NewestVersion = OldVersionList_Copy[0];

                            foreach (var V in OldVersionList_Copy)
                            {
                                if (NewestVersion <= V)
                                {
                                    NewestVersion = V;
                                }
                            }

                            OldVersionList_Sorted.Add(NewestVersion);
                            OldVersionList_Copy.Remove(NewestVersion);
                        }

                        for (int i = 0; i < OldVersionList_Sorted.Count; i++)
                        {
                            string Dir = RootDir_Product + "\\" + OldVersionList_Sorted[i].Build + "." + OldVersionList_Sorted[i].Revision;

                            if (Directory.Exists(Dir))
                            {
                                try
                                {
                                    Com.IO.CopyFolder(Dir, RootDir_CurrentVersion);

                                    break;
                                }
                                catch
                                {
                                    continue;
                                }
                            }
                        }
                    }
                }
            }
            catch { }
        }

        private void DelOldConfig()
        {
            //
            // 删除当前内部版本号下所有旧版本的配置文件。
            //

            try
            {
                if (OldVersionList.Count > 0)
                {
                    foreach (var V in OldVersionList)
                    {
                        Com.IO.DeleteFolder(RootDir_Product + "\\" + V.Build + "." + V.Revision);
                    }
                }
            }
            catch { }
        }

        private void LoadConfig()
        {
            //
            // 加载配置文件。
            //

            if (File.Exists(ConfigFilePath))
            {
                if (new FileInfo(ConfigFilePath).Length > 0)
                {
                    StreamReader Read = new StreamReader(ConfigFilePath, false);
                    string Cfg = Read.ReadLine();
                    Read.Close();

                    Regex RegexUint = new Regex(@"[^0-9]");
                    Regex RegexFloat = new Regex(@"[^0-9\-\.]");

                    //

                    try
                    {
                        string SubStr = RegexUint.Replace(Com.Text.GetIntervalString(Cfg, "<ElementSize>", "</ElementSize>", false, false), string.Empty);

                        ElementSize = Convert.ToInt32(SubStr);
                    }
                    catch { }

                    //

                    try
                    {
                        string SubStr = Com.Text.GetIntervalString(Cfg, "<Order>", "</Order>", false, false);

                        foreach (var V in Enum.GetValues(typeof(Orders)))
                        {
                            if (SubStr.Trim().ToUpper() == V.ToString().ToUpper())
                            {
                                Order = (Orders)V;

                                break;
                            }
                        }
                    }
                    catch { }

                    //

                    try
                    {
                        string SubStr = RegexUint.Replace(Com.Text.GetIntervalString(Cfg, "<DifficultyLevel>", "</DifficultyLevel>", false, false), string.Empty);

                        Int32 DL = Convert.ToInt32(SubStr);

                        if (DL >= DifficultyLevel_MIN && DL <= DifficultyLevel_MAX)
                        {
                            DifficultyLevel = DL;
                        }
                    }
                    catch { }

                    //

                    if (Com.Text.GetIntervalString(Cfg, "<ShowNotes>", "</ShowNotes>", false, false).Contains((!ShowNotes).ToString()))
                    {
                        ShowNotes = !ShowNotes;
                    }

                    //

                    try
                    {
                        string SubStr = Com.Text.GetIntervalString(Cfg, "<OperationMode>", "</OperationMode>", false, false);

                        foreach (var V in Enum.GetValues(typeof(OperationModes)))
                        {
                            if (SubStr.Trim().ToUpper() == V.ToString().ToUpper())
                            {
                                OperationMode = (OperationModes)V;

                                break;
                            }
                        }
                    }
                    catch { }

                    if (Com.Text.GetIntervalString(Cfg, "<AlwaysEnableKeyboard>", "</AlwaysEnableKeyboard>", false, false).Contains((!AlwaysEnableKeyboard).ToString()))
                    {
                        AlwaysEnableKeyboard = !AlwaysEnableKeyboard;
                    }

                    //

                    if (Com.Text.GetIntervalString(Cfg, "<SaveEveryStep>", "</SaveEveryStep>", false, false).Contains((!SaveEveryStep).ToString()))
                    {
                        SaveEveryStep = !SaveEveryStep;
                    }

                    //

                    try
                    {
                        string SubStr = Com.Text.GetIntervalString(Cfg, "<Theme>", "</Theme>", false, false);

                        foreach (var V in Enum.GetValues(typeof(Com.WinForm.Theme)))
                        {
                            if (SubStr.Trim().ToUpper() == V.ToString().ToUpper())
                            {
                                Me.Theme = (Com.WinForm.Theme)V;

                                break;
                            }
                        }
                    }
                    catch { }

                    //

                    if (Com.Text.GetIntervalString(Cfg, "<UseRandomThemeColor>", "</UseRandomThemeColor>", false, false).Contains((!UseRandomThemeColor).ToString()))
                    {
                        UseRandomThemeColor = !UseRandomThemeColor;
                    }

                    if (!UseRandomThemeColor)
                    {
                        try
                        {
                            string SubStr = Com.Text.GetIntervalString(Cfg, "<ThemeColor>", "</ThemeColor>", false, false);

                            string[] Fields = SubStr.Split(',');

                            if (Fields.Length == 3)
                            {
                                int i = 0;

                                string StrR = RegexUint.Replace(Fields[i++], string.Empty);
                                Int32 TC_R = Convert.ToInt32(StrR);

                                string StrG = RegexUint.Replace(Fields[i++], string.Empty);
                                Int32 TC_G = Convert.ToInt32(StrG);

                                string StrB = RegexUint.Replace(Fields[i++], string.Empty);
                                Int32 TC_B = Convert.ToInt32(StrB);

                                Me.ThemeColor = Com.ColorX.FromRGB(TC_R, TC_G, TC_B);
                            }
                        }
                        catch { }
                    }

                    //

                    if (Com.Text.GetIntervalString(Cfg, "<ShowFormTitleColor>", "</ShowFormTitleColor>", false, false).Contains((!Me.ShowCaptionBarColor).ToString()))
                    {
                        Me.ShowCaptionBarColor = !Me.ShowCaptionBarColor;
                    }

                    //

                    try
                    {
                        string SubStr = RegexFloat.Replace(Com.Text.GetIntervalString(Cfg, "<Opacity>", "</Opacity>", false, false), string.Empty);

                        double Op = Convert.ToDouble(SubStr);

                        if (Op >= Opacity_MIN && Op <= Opacity_MAX)
                        {
                            Me.Opacity = Op;
                        }
                    }
                    catch { }

                    //

                    if (Com.Text.GetIntervalString(Cfg, "<AntiAlias>", "</AntiAlias>", false, false).Contains((!AntiAlias).ToString()))
                    {
                        AntiAlias = !AntiAlias;
                    }
                }
            }
        }

        private void SaveConfig()
        {
            //
            // 保存配置文件。
            //

            string Cfg = string.Empty;

            Cfg += "<Config>";

            Cfg += "<ElementSize>" + ElementSize + "</ElementSize>";
            Cfg += "<Order>" + Order + "</Order>";
            Cfg += "<DifficultyLevel>" + DifficultyLevel + "</DifficultyLevel>";
            Cfg += "<ShowNotes>" + ShowNotes + "</ShowNotes>";
            Cfg += "<OperationMode>" + OperationMode + "</OperationMode>";
            Cfg += "<AlwaysEnableKeyboard>" + AlwaysEnableKeyboard + "</AlwaysEnableKeyboard>";
            Cfg += "<SaveEveryStep>" + SaveEveryStep + "</SaveEveryStep>";

            Cfg += "<Theme>" + Me.Theme.ToString() + "</Theme>";
            Cfg += "<UseRandomThemeColor>" + UseRandomThemeColor + "</UseRandomThemeColor>";
            Cfg += "<ThemeColor>(" + Me.ThemeColor.ToColor().R + ", " + Me.ThemeColor.ToColor().G + ", " + Me.ThemeColor.ToColor().B + ")</ThemeColor>";
            Cfg += "<ShowFormTitleColor>" + Me.ShowCaptionBarColor + "</ShowFormTitleColor>";
            Cfg += "<Opacity>" + Me.Opacity + "</Opacity>";

            Cfg += "<AntiAlias>" + AntiAlias + "</AntiAlias>";

            Cfg += "</Config>";

            //

            try
            {
                if (!Directory.Exists(ConfigFileDir))
                {
                    Directory.CreateDirectory(ConfigFileDir);
                }

                StreamWriter Write = new StreamWriter(ConfigFilePath, false);
                Write.WriteLine(Cfg);
                Write.Close();
            }
            catch { }
        }

        #endregion

        #region 存档管理

        // 用户数据。

        private void LoadUserData()
        {
            //
            // 加载用户数据。
            //

            if (File.Exists(DataFilePath))
            {
                FileInfo FInfo = new FileInfo(DataFilePath);

                if (FInfo.Length > 0)
                {
                    StreamReader SR = new StreamReader(DataFilePath, false);
                    string Str = SR.ReadLine();
                    SR.Close();

                    Regex RegexUint = new Regex(@"[^0-9]");

                    //

                    try
                    {
                        string SubStr = RegexUint.Replace(Com.Text.GetIntervalString(Str, "<TotalGameTime>", "</TotalGameTime>", false, false), string.Empty);

                        TotalGameTime = TimeSpan.FromMilliseconds(Convert.ToInt64(SubStr));
                    }
                    catch { }

                    //

                    try
                    {
                        string SubStr = Com.Text.GetIntervalString(Str, "<BestRecord>", "</BestRecord>", false, false);

                        while (SubStr.IndexOf("(") != -1 && SubStr.IndexOf(")") != -1)
                        {
                            try
                            {
                                string StrRec = Com.Text.GetIntervalString(SubStr, "(", ")", false, false);

                                string[] Fields = StrRec.Split(',');

                                if (Fields.Length == 4 || Fields.Length == 3)
                                {
                                    int i = 0;

                                    Record Rec = new Record();

                                    if (Fields.Length == 4)
                                    {
                                        string StrOV = RegexUint.Replace(Fields[i++], string.Empty);
                                        Rec.OrderValue = Convert.ToInt32(StrOV);

                                        string StrDL = RegexUint.Replace(Fields[i++], string.Empty);
                                        Rec.DifficultyLevel = Convert.ToInt32(StrDL);

                                        string StrGT = RegexUint.Replace(Fields[i++], string.Empty);
                                        Rec.GameTime = TimeSpan.FromMilliseconds(Convert.ToInt64(StrGT));

                                        string StrSC = RegexUint.Replace(Fields[i++], string.Empty);
                                        Rec.StepCount = Convert.ToInt32(StrSC);
                                    }
                                    else if (Fields.Length == 3)
                                    {
                                        Rec.OrderValue = (Int32)Orders.MIN;

                                        string StrDL = RegexUint.Replace(Fields[i++], string.Empty);
                                        Rec.DifficultyLevel = Convert.ToInt32(StrDL);

                                        string StrGT = RegexUint.Replace(Fields[i++], string.Empty);
                                        Rec.GameTime = TimeSpan.FromMilliseconds(Convert.ToInt64(StrGT));

                                        string StrSC = RegexUint.Replace(Fields[i++], string.Empty);
                                        Rec.StepCount = Convert.ToInt32(StrSC);
                                    }

                                    if ((Rec.OrderValue >= (Int32)Orders.MIN && Rec.OrderValue <= (Int32)Orders.MAX) && (Rec.DifficultyLevel >= DifficultyLevel_MIN && Rec.DifficultyLevel <= DifficultyLevel_MAX))
                                    {
                                        BestRecordArray[Rec.OrderValue - (Int32)Orders.MIN, Rec.DifficultyLevel - DifficultyLevel_MIN] = Rec;
                                    }
                                }
                            }
                            catch { }

                            SubStr = SubStr.Substring(SubStr.IndexOf(")") + (")").Length);
                        }
                    }
                    catch { }
                }
            }
        }

        private void SaveUserData()
        {
            //
            // 保存用户数据。
            //

            if (!ShowNotes && GameIsWin && ((BestRecord.GameTime.TotalMilliseconds == 0 || (ThisRecord.GameTime.TotalMilliseconds > 0 && BestRecord.GameTime > ThisRecord.GameTime)) && ThisRecord.StepCount > 0))
            {
                BestRecord = ThisRecord;
            }

            //

            string Str = string.Empty;

            Str += "<Log>";

            Str += "<TotalGameTime>" + (Int64)TotalGameTime.TotalMilliseconds + "</TotalGameTime>";

            Str += "<BestRecord>[";
            for (int i = (Int32)Orders.MIN; i <= (Int32)Orders.MAX; i++)
            {
                for (int j = DifficultyLevel_MIN; j <= DifficultyLevel_MAX; j++)
                {
                    Record Rec = BestRecordArray[i - (Int32)Orders.MIN, j - DifficultyLevel_MIN];

                    if (Rec.GameTime.TotalMilliseconds > 0 && Rec.StepCount > 0)
                    {
                        Str += "(" + Rec.OrderValue + "," + Rec.DifficultyLevel + "," + Rec.GameTime.TotalMilliseconds + "," + Rec.StepCount + ")";
                    }
                }
            }
            Str += "]</BestRecord>";

            Str += "</Log>";

            //

            try
            {
                if (!Directory.Exists(LogFileDir))
                {
                    Directory.CreateDirectory(LogFileDir);
                }

                StreamWriter SW = new StreamWriter(DataFilePath, false);
                SW.WriteLine(Str);
                SW.Close();
            }
            catch { }
        }

        // 上次游戏。

        private void LoadLastGame()
        {
            //
            // 加载上次游戏。
            //

            if (File.Exists(RecordFilePath))
            {
                FileInfo FInfo = new FileInfo(RecordFilePath);

                if (FInfo.Length > 0)
                {
                    StreamReader SR = new StreamReader(RecordFilePath, false);
                    string Str = SR.ReadLine();
                    SR.Close();

                    Regex RegexUint = new Regex(@"[^0-9]");

                    //

                    try
                    {
                        string SubStr = RegexUint.Replace(Com.Text.GetIntervalString(Str, "<OrderValue>", "</OrderValue>", false, false), string.Empty);

                        Record_Last.OrderValue = Convert.ToInt32(SubStr);
                    }
                    catch { }

                    //

                    try
                    {
                        string SubStr = RegexUint.Replace(Com.Text.GetIntervalString(Str, "<DifficultyLevel>", "</DifficultyLevel>", false, false), string.Empty);

                        Record_Last.DifficultyLevel = Convert.ToInt32(SubStr);
                    }
                    catch { }

                    //

                    if (Com.Text.GetIntervalString(Str, "<ShowNotes>", "</ShowNotes>", false, false).Contains((!Record_Last.ShowNotes).ToString()))
                    {
                        Record_Last.ShowNotes = !Record_Last.ShowNotes;
                    }

                    //

                    try
                    {
                        string SubStr = Com.Text.GetIntervalString(Str, "<Element>", "</Element>", false, false);

                        while (SubStr.IndexOf("(") != -1 && SubStr.IndexOf(")") != -1)
                        {
                            try
                            {
                                string StrE = Com.Text.GetIntervalString(SubStr, "(", ")", false, false);

                                string[] Fields = StrE.Split(',');

                                if (Fields.Length == 4)
                                {
                                    int i = 0;

                                    Point Index = new Point();
                                    Int32 E = 0;
                                    bool SF = false;

                                    string StrIDX = RegexUint.Replace(Fields[i++], string.Empty);
                                    Index.X = Convert.ToInt32(StrIDX);

                                    string StrIDY = RegexUint.Replace(Fields[i++], string.Empty);
                                    Index.Y = Convert.ToInt32(StrIDY);

                                    string StrVal = RegexUint.Replace(Fields[i++], string.Empty);
                                    E = Convert.ToInt32(StrVal);

                                    string StrSF = RegexUint.Replace(Fields[i++], string.Empty);
                                    SF = (Convert.ToInt32(StrSF) == 0 ? false : true);

                                    if ((Index.X >= 0 && Index.X < Range.Width && Index.Y >= 0 && Index.Y < Range.Height) && (E > 0 && E <= SudokuSize))
                                    {
                                        ElementArray_Last[Index.X, Index.Y] = E;
                                        ElementIndexList_Last.Add(Index);

                                        SolidFlagTable_Last[Index.X, Index.Y] = SF;
                                    }
                                }
                            }
                            catch { }

                            SubStr = SubStr.Substring(SubStr.IndexOf(")") + (")").Length);
                        }
                    }
                    catch { }

                    //

                    try
                    {
                        string SubStr = RegexUint.Replace(Com.Text.GetIntervalString(Str, "<GameTime>", "</GameTime>", false, false), string.Empty);

                        Record_Last.GameTime = TimeSpan.FromMilliseconds(Convert.ToInt64(SubStr));
                    }
                    catch { }

                    //

                    try
                    {
                        string SubStr = RegexUint.Replace(Com.Text.GetIntervalString(Str, "<StepCount>", "</StepCount>", false, false), string.Empty);

                        Record_Last.StepCount = Convert.ToInt32(SubStr);
                    }
                    catch { }

                    //

                    StepListString = string.Empty;

                    StepListString += Com.Text.GetIntervalString(Str, "<Previous>", "</Previous>", true, true);
                    StepListString += Com.Text.GetIntervalString(Str, "<Next>", "</Next>", true, true);
                }
            }
        }

        private void EraseLastGame()
        {
            //
            // 擦除上次游戏。
            //

            foreach (var V in ElementIndexList_Last)
            {
                ElementArray_Last[V.X, V.Y] = 0;
            }

            ElementIndexList_Last.Clear();

            Record_Last = new Record();

            StepListString = string.Empty;

            //

            try
            {
                if (!Directory.Exists(LogFileDir))
                {
                    Directory.CreateDirectory(LogFileDir);
                }

                StreamWriter SW = new StreamWriter(RecordFilePath, false);
                SW.WriteLine(string.Empty);
                SW.Close();
            }
            catch { }
        }

        // 上次游戏：游戏步骤。

        private void CleanGameStep()
        {
            //
            // 清理游戏步骤。
            //

            if (StepListString.Length > 0)
            {
                StepListString = string.Empty;

                //

                string Str = string.Empty;

                Str += "<Log>";

                if (File.Exists(RecordFilePath))
                {
                    FileInfo FInfo = new FileInfo(RecordFilePath);

                    if (FInfo.Length > 0)
                    {
                        StreamReader SR = new StreamReader(RecordFilePath, false);
                        string S = SR.ReadLine();
                        SR.Close();

                        //

                        Str += Com.Text.GetIntervalString(S, "<OrderValue>", "</OrderValue>", true, true);
                        Str += Com.Text.GetIntervalString(S, "<DifficultyLevel>", "</DifficultyLevel>", true, true);
                        Str += Com.Text.GetIntervalString(S, "<ShowNotes>", "</ShowNotes>", true, true);
                        Str += Com.Text.GetIntervalString(S, "<GameTime>", "</GameTime>", true, true);
                        Str += Com.Text.GetIntervalString(S, "<StepCount>", "</StepCount>", true, true);
                    }
                }

                Str += "</Log>";

                //

                try
                {
                    if (!Directory.Exists(LogFileDir))
                    {
                        Directory.CreateDirectory(LogFileDir);
                    }

                    StreamWriter SW = new StreamWriter(RecordFilePath, false);
                    SW.WriteLine(Str);
                    SW.Close();
                }
                catch { }
            }
        }

        private DateTime BkgWkrStartingTime = DateTime.Now; // 处理游戏步骤的后台工作的开始时刻。
        private double BkgWkrComPct = 0; // 处理游戏步骤的后台工作的已完成百分比。

        private string GetRemainingTimeString(Int64 Seconds)
        {
            //
            // 获取剩余时间字符串。Seconds：剩余时间的总秒数。
            //

            try
            {
                TimeSpan TS = TimeSpan.FromSeconds(Seconds);

                return ((Int32)TS.TotalDays >= 1 ? TS.Days + " 天 " + TS.Hours + " 小时" : ((Int32)TS.TotalHours >= 1 ? TS.Hours + " 小时 " + TS.Minutes / 5 * 5 + " 分" : ((Int32)TS.TotalMinutes >= 1 ? TS.Minutes + " 分 " + TS.Seconds / 15 * 15 + " 秒" : ((Int32)TS.TotalSeconds >= 0 ? Math.Max(1, TS.Seconds / 5) * 5 + " 秒" : string.Empty))));
            }
            catch
            {
                return string.Empty;
            }
        }

        private void _LoadGameStep()
        {
            //
            // 为后台或前台操作提供加载游戏步骤功能。
            //

            if (SaveEveryStep)
            {
                Regex RegexUint = new Regex(@"[^0-9]");

                //

                Int32 PreviousStrLen = 0, NextStrLen = 0;
                double PreviousPct = 0, NextPct = 0;

                //

                try
                {
                    string SubStr = Com.Text.GetIntervalString(StepListString, "<Previous>", "</Previous>", false, false);

                    PreviousStrLen = SubStr.Length;
                    PreviousPct = (double)PreviousStrLen / StepListString.Length;

                    while (SubStr.IndexOf("[") != -1 && SubStr.IndexOf("]") != -1)
                    {
                        try
                        {
                            if (BackgroundWorker_LoadGameStep.IsBusy)
                            {
                                BkgWkrComPct = (1 - Math.Pow((double)SubStr.Length / PreviousStrLen, 2)) * PreviousPct * 100;

                                BackgroundWorker_LoadGameStep.ReportProgress((Int32)BkgWkrComPct);
                            }

                            string StepString = Com.Text.GetIntervalString(SubStr, "[", "]", false, false);

                            Step S = new Step();
                            S.ElementArray = new Int32[CAPACITY, CAPACITY];

                            //

                            while (StepString.IndexOf("(") != -1 && StepString.IndexOf(")") != -1)
                            {
                                try
                                {
                                    string StrE = Com.Text.GetIntervalString(StepString, "(", ")", false, false);

                                    string[] Fields = StrE.Split(',');

                                    if (Fields.Length == 3)
                                    {
                                        int i = 0;

                                        Point Index = new Point();
                                        Int32 E = 0;

                                        string StrIDX = RegexUint.Replace(Fields[i++], string.Empty);
                                        Index.X = Convert.ToInt32(StrIDX);

                                        string StrIDY = RegexUint.Replace(Fields[i++], string.Empty);
                                        Index.Y = Convert.ToInt32(StrIDY);

                                        string StrVal = RegexUint.Replace(Fields[i++], string.Empty);
                                        E = Convert.ToInt32(StrVal);

                                        if ((Index.X >= 0 && Index.X < Range.Width && Index.Y >= 0 && Index.Y < Range.Height) && (E > 0 && E <= SudokuSize))
                                        {
                                            S.ElementArray[Index.X, Index.Y] = E;
                                        }
                                    }
                                }
                                catch { }

                                StepString = StepString.Substring(StepString.IndexOf(")") + (")").Length);
                            }

                            //

                            StepList_Previous.Add(S);
                        }
                        catch { }

                        SubStr = SubStr.Substring(SubStr.IndexOf("]") + ("]").Length);
                    }
                }
                catch { }

                //

                try
                {
                    string SubStr = Com.Text.GetIntervalString(StepListString, "<Next>", "</Next>", false, false);

                    NextStrLen = SubStr.Length;
                    NextPct = (double)PreviousStrLen / StepListString.Length;

                    while (SubStr.IndexOf("[") != -1 && SubStr.IndexOf("]") != -1)
                    {
                        try
                        {
                            if (BackgroundWorker_LoadGameStep.IsBusy)
                            {
                                BkgWkrComPct = (PreviousPct + (1 - Math.Pow((double)SubStr.Length / NextStrLen, 2)) * NextPct) * 100;

                                BackgroundWorker_LoadGameStep.ReportProgress((Int32)BkgWkrComPct);
                            }

                            string StepString = Com.Text.GetIntervalString(SubStr, "[", "]", false, false);

                            Step S = new Step();
                            S.ElementArray = new Int32[CAPACITY, CAPACITY];

                            //

                            while (StepString.IndexOf("(") != -1 && StepString.IndexOf(")") != -1)
                            {
                                try
                                {
                                    string StrE = Com.Text.GetIntervalString(StepString, "(", ")", false, false);

                                    string[] Fields = StrE.Split(',');

                                    if (Fields.Length == 3)
                                    {
                                        int i = 0;

                                        Point Index = new Point();
                                        Int32 E = 0;

                                        string StrIDX = RegexUint.Replace(Fields[i++], string.Empty);
                                        Index.X = Convert.ToInt32(StrIDX);

                                        string StrIDY = RegexUint.Replace(Fields[i++], string.Empty);
                                        Index.Y = Convert.ToInt32(StrIDY);

                                        string StrVal = RegexUint.Replace(Fields[i++], string.Empty);
                                        E = Convert.ToInt32(StrVal);

                                        if ((Index.X >= 0 && Index.X < Range.Width && Index.Y >= 0 && Index.Y < Range.Height) && (E > 0 && E <= SudokuSize))
                                        {
                                            S.ElementArray[Index.X, Index.Y] = E;
                                        }
                                    }
                                }
                                catch { }

                                StepString = StepString.Substring(StepString.IndexOf(")") + (")").Length);
                            }

                            //

                            StepList_Next.Add(S);
                        }
                        catch { }

                        SubStr = SubStr.Substring(SubStr.IndexOf("]") + ("]").Length);
                    }
                }
                catch { }
            }

            //

            ElementArray_Initialize();

            foreach (var V in ElementIndexList_Last)
            {
                ElementArray_Add(V, ElementArray_Last[V.X, V.Y]);
            }

            for (int X = 0; X < Range.Width; X++)
            {
                for (int Y = 0; Y < Range.Height; Y++)
                {
                    SolidFlagTable[X, Y] = SolidFlagTable_Last[X, Y];
                }
            }

            ElementArray_UpdateProbableValuesTable();
            ElementArray_UpdateCorrectionTable();

            ThisRecord.GameTime = Record_Last.GameTime;
            ThisRecord.StepCount = Record_Last.StepCount;
        }

        private void BackgroundWorker_LoadGameStep_DoWork(object sender, DoWorkEventArgs e)
        {
            //
            // 加载游戏步骤后台工作。
            //

            _LoadGameStep();
        }

        private void BackgroundWorker_LoadGameStep_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            //
            // 加载游戏步骤后台工作报告进度。
            //

            Int64 RemainingSeconds = ((e.ProgressPercentage > 0 && e.ProgressPercentage < 100) ? (Int64)((DateTime.Now - BkgWkrStartingTime).TotalSeconds / BkgWkrComPct * (100 - BkgWkrComPct)) : -1);

            Me.Caption = (RemainingSeconds >= 0 ? "已完成 " + e.ProgressPercentage + "% - 剩余 " + GetRemainingTimeString(RemainingSeconds) : ApplicationName) + " [正在打开]";
        }

        private void BackgroundWorker_LoadGameStep_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //
            // 加载游戏步骤后台工作完成。
            //

            OperatingIndex = new Point(-1, -1);
            OperatingNumber = -1;

            //

            TimerStart();

            ElementArray_RepresentAll();

            Judgement();

            //

            if (StepList_Previous.Count > 0)
            {
                PictureBox_Undo.Enabled = true;
            }

            if (StepList_Next.Count > 0)
            {
                PictureBox_Redo.Enabled = true;
            }

            //

            Me.Enabled = true;

            Me.Caption = ApplicationName;
        }

        private void LoadGameStepInBackground()
        {
            //
            // 后台加载游戏步骤。
            //

            Me.Enabled = false;

            Me.Caption = ApplicationName + " [正在打开]";

            //

            BkgWkrStartingTime = DateTime.Now;

            BackgroundWorker_LoadGameStep.RunWorkerAsync();
        }

        private void LoadGameStepInForeground()
        {
            //
            // 前台加载游戏步骤。
            //

            Me.Enabled = false;

            Me.Caption = ApplicationName + " [正在打开]";

            //

            _LoadGameStep();

            //

            OperatingIndex = new Point(-1, -1);
            OperatingNumber = -1;

            //

            TimerStart();

            ElementArray_RepresentAll();

            Judgement();

            //

            if (StepList_Previous.Count > 0)
            {
                PictureBox_Undo.Enabled = true;
            }

            if (StepList_Next.Count > 0)
            {
                PictureBox_Redo.Enabled = true;
            }

            //

            Me.Enabled = true;

            Me.Caption = ApplicationName;
        }

        private void _SaveGameStep()
        {
            //
            // 为后台或前台操作提供保存游戏步骤功能。
            //

            Record_Last = ThisRecord;

            foreach (var V in ElementIndexList_Last)
            {
                ElementArray_Last[V.X, V.Y] = 0;
            }

            ElementIndexList_Last.Clear();

            foreach (var V in ElementIndexList)
            {
                ElementArray_Last[V.X, V.Y] = ElementArray[V.X, V.Y];

                ElementIndexList_Last.Add(V);
            }

            for (int X = 0; X < Range.Width; X++)
            {
                for (int Y = 0; Y < Range.Height; Y++)
                {
                    SolidFlagTable_Last[X, Y] = SolidFlagTable[X, Y];
                }
            }

            StepListString = string.Empty;

            //

            string Str = string.Empty;

            Str += "<Log>";

            Str += "<OrderValue>" + (Int32)Order + "</OrderValue>";

            Str += "<DifficultyLevel>" + DifficultyLevel + "</DifficultyLevel>";

            Str += "<ShowNotes>" + ShowNotes + "</ShowNotes>";

            Str += "<Element>[";
            for (int i = 0; i < ElementIndexList.Count; i++)
            {
                Point A = ElementIndexList[i];

                Str += "(" + A.X + "," + A.Y + "," + ElementArray[A.X, A.Y] + "," + (SolidFlagTable[A.X, A.Y] ? "1" : "0") + ")";
            }
            Str += "]</Element>";

            Str += "<GameTime>" + ThisRecord.GameTime.TotalMilliseconds + "</GameTime>";
            Str += "<StepCount>" + ThisRecord.StepCount + "</StepCount>";

            if (SaveEveryStep)
            {
                double PreviousPct = 0, NextPct = 0;

                if (StepList_Previous.Count > 0)
                {
                    PreviousPct = (double)StepList_Previous.Count / (StepList_Previous.Count + StepList_Next.Count);

                    StepListString += "<Previous>";
                    for (int i = 0; i < StepList_Previous.Count; i++)
                    {
                        if (BackgroundWorker_SaveGameStep.IsBusy)
                        {
                            BkgWkrComPct = Math.Pow((double)(i + 1) / StepList_Previous.Count, 2) * PreviousPct * 100;

                            BackgroundWorker_SaveGameStep.ReportProgress((Int32)BkgWkrComPct);
                        }

                        Step S = StepList_Previous[i];

                        StepListString += "[";
                        for (int X = 0; X < Range.Width; X++)
                        {
                            for (int Y = 0; Y < Range.Height; Y++)
                            {
                                if (S.ElementArray[X, Y] != 0)
                                {
                                    StepListString += "(" + X + "," + Y + "," + S.ElementArray[X, Y] + ")";
                                }
                            }
                        }
                        StepListString += "]";
                    }
                    StepListString += "</Previous>";
                }

                if (StepList_Next.Count > 0)
                {
                    NextPct = (double)StepList_Next.Count / (StepList_Previous.Count + StepList_Next.Count);

                    StepListString += "<Next>";
                    for (int i = 0; i < StepList_Next.Count; i++)
                    {
                        if (BackgroundWorker_SaveGameStep.IsBusy)
                        {
                            BkgWkrComPct = (PreviousPct + Math.Pow((double)(i + 1) / StepList_Previous.Count, 2) * NextPct) * 100;

                            BackgroundWorker_SaveGameStep.ReportProgress((Int32)BkgWkrComPct);
                        }

                        Step S = StepList_Next[i];

                        StepListString += "[";
                        for (int X = 0; X < Range.Width; X++)
                        {
                            for (int Y = 0; Y < Range.Height; Y++)
                            {
                                if (S.ElementArray[X, Y] != 0)
                                {
                                    StepListString += "(" + X + "," + Y + "," + S.ElementArray[X, Y] + ")";
                                }
                            }
                        }
                        StepListString += "]";
                    }
                    StepListString += "</Next>";
                }
            }

            Str += "<Step>" + StepListString + "</Step>";

            Str += "</Log>";

            //

            try
            {
                if (!Directory.Exists(LogFileDir))
                {
                    Directory.CreateDirectory(LogFileDir);
                }

                StreamWriter SW = new StreamWriter(RecordFilePath, false);
                SW.WriteLine(Str);
                SW.Close();
            }
            catch { }
        }

        private void BackgroundWorker_SaveGameStep_DoWork(object sender, DoWorkEventArgs e)
        {
            //
            // 保存游戏步骤后台工作。
            //

            _SaveGameStep();
        }

        private void BackgroundWorker_SaveGameStep_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            //
            // 保存游戏步骤后台工作报告进度。
            //

            Int64 RemainingSeconds = ((e.ProgressPercentage > 0 && e.ProgressPercentage < 100) ? (Int64)((DateTime.Now - BkgWkrStartingTime).TotalSeconds / BkgWkrComPct * (100 - BkgWkrComPct)) : -1);

            Me.Caption = (RemainingSeconds >= 0 ? "已完成 " + e.ProgressPercentage + "% - 剩余 " + GetRemainingTimeString(RemainingSeconds) : ApplicationName) + " [正在保存]";
        }

        private void BackgroundWorker_SaveGameStep_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //
            // 保存游戏步骤后台工作完成。
            //

            ThisRecord.GameTime = TimeSpan.Zero;
            ThisRecord.StepCount = 0;

            //

            ExitGameUI();

            //

            Me.Enabled = true;

            Me.Caption = ApplicationName;
        }

        private void SaveGameStepInBackground()
        {
            //
            // 后台保存游戏步骤。
            //

            Me.Enabled = false;

            Me.Caption = ApplicationName + " [正在保存]";

            //

            BkgWkrStartingTime = DateTime.Now;

            BackgroundWorker_SaveGameStep.RunWorkerAsync();
        }

        private void SaveGameStepInForeground()
        {
            //
            // 前台保存游戏步骤。
            //

            Me.Caption = ApplicationName + " [正在保存]";

            //

            _SaveGameStep();
        }

        #endregion

        #region 数组功能

        private static Int32[,] GetCopyOfArray(Int32[,] Array)
        {
            //
            // 返回二维矩阵的浅表副本。Array：矩阵。
            //

            try
            {
                if (Array != null)
                {
                    return (Int32[,])Array.Clone();
                }

                return null;
            }
            catch
            {
                return null;
            }
        }

        private static Int32 GetZeroCountOfArray(Int32[,] Array, Size Cap)
        {
            //
            // 计算二维矩阵值为 0 的元素的数量。Array：矩阵，索引为 [x, y]；Cap：矩阵的大小，分量 (Width, Height) 分别表示沿 x 方向和沿 y 方向的元素数量。
            //

            try
            {
                if (Array != null)
                {
                    Int32 ZeroCount = 0;

                    for (int X = 0; X < Cap.Width; X++)
                    {
                        for (int Y = 0; Y < Cap.Height; Y++)
                        {
                            if (Array[X, Y] == 0)
                            {
                                ZeroCount++;
                            }
                        }
                    }

                    return ZeroCount;
                }

                return 0;
            }
            catch
            {
                return 0;
            }
        }

        private static List<Point> GetCertainIndexListOfArray(Int32[,] Array, Size Cap, Int32 Value)
        {
            //
            // 返回二维矩阵中所有值为指定值的元素的索引的列表。Array：矩阵，索引为 [x, y]；Cap：矩阵的大小，分量 (Width, Height) 分别表示沿 x 方向和沿 y 方向的元素数量；Value：指定值。
            //

            try
            {
                if (Array != null)
                {
                    List<Point> L = new List<Point>(Cap.Width * Cap.Height);

                    for (int X = 0; X < Cap.Width; X++)
                    {
                        for (int Y = 0; Y < Cap.Height; Y++)
                        {
                            if (Array[X, Y] == Value)
                            {
                                L.Add(new Point(X, Y));
                            }
                        }
                    }

                    return L;
                }

                return new List<Point>(0);
            }
            catch
            {
                return new List<Point>(0);
            }
        }

        #endregion

        #region 元素矩阵基本功能

        // 初始化。

        private void ElementArray_Initialize()
        {
            //
            // 初始化。
            //

            for (int i = 0; i < ElementIndexList.Count; i++)
            {
                ElementArray[ElementIndexList[i].X, ElementIndexList[i].Y] = 0;
            }

            ElementIndexList.Clear();
        }

        // 索引。

        private Point ElementArray_GetIndex(Point P)
        {
            //
            // 获取绘图容器中的指定坐标所在元素的索引。P：坐标。
            //

            try
            {
                Point dP = new Point(P.X - EAryBmpRect.X, P.Y - EAryBmpRect.Y);
                Point A = new Point((Int32)Math.Floor((double)dP.X / ElementSize), (Int32)Math.Floor((double)dP.Y / ElementSize));

                if (A.X >= 0 && A.X < Range.Width && A.Y >= 0 && A.Y < Range.Height)
                {
                    return A;
                }

                return new Point(-1, -1);
            }
            catch
            {
                return new Point(-1, -1);
            }
        }

        // 颜色。

        private Color ElementArray_GetColor(Int32 E)
        {
            //
            // 获取元素颜色。E：元素的值。
            //

            try
            {
                if (E == 0)
                {
                    return Me.RecommendColors.Background.ToColor();
                }
                else if (E > 0)
                {
                    return Me.RecommendColors.Main_DEC.ToColor();
                }

                return Color.Empty;
            }
            catch
            {
                return Color.Empty;
            }
        }

        // 添加与移除。

        private void ElementArray_Add(Point A, Int32 E)
        {
            //
            // 向元素矩阵添加一个元素。A：索引；E：元素的值。
            //

            if (E != 0 && A.X >= 0 && A.X < CAPACITY && A.Y >= 0 && A.Y < CAPACITY)
            {
                if (!ElementIndexList.Contains(A))
                {
                    ElementArray[A.X, A.Y] = E;

                    ElementIndexList.Add(A);
                }
            }
        }

        private void ElementArray_RemoveAt(Point A)
        {
            //
            // 从元素矩阵移除一个元素。A：索引。
            //

            if (A.X >= 0 && A.X < CAPACITY && A.Y >= 0 && A.Y < CAPACITY)
            {
                ElementArray[A.X, A.Y] = 0;

                if (ElementIndexList.Contains(A))
                {
                    ElementIndexList.Remove(A);
                }
            }
        }

        // 绘图与呈现。

        private Rectangle EAryBmpRect = new Rectangle(); // 元素矩阵位图区域（相对于绘图容器）。

        private Bitmap EAryBmp; // 元素矩阵位图。

        private Graphics EAryBmpGrap; // 元素矩阵位图绘图。

        private void ElementArray_DrawAtPoint(Point A, bool PresentNow)
        {
            //
            // 在元素矩阵位图的指定索引处绘制一个元素。A：索引；PresentNow：是否立即呈现此元素，如果为 true，那么将在位图中绘制此元素，并在不重绘整个位图的情况下在容器中绘制此元素，如果为 false，那么将仅在位图中绘制此元素。
            //

            if (A.X >= 0 && A.X < CAPACITY && A.Y >= 0 && A.Y < CAPACITY)
            {
                Rectangle BmpRect = new Rectangle(new Point(A.X * ElementSize, A.Y * ElementSize), new Size(ElementSize, ElementSize));

                Bitmap Bmp = new Bitmap(BmpRect.Width, BmpRect.Height);

                Graphics BmpGrap = Graphics.FromImage(Bmp);

                if (AntiAlias)
                {
                    BmpGrap.SmoothingMode = SmoothingMode.AntiAlias;
                    BmpGrap.TextRenderingHint = TextRenderingHint.AntiAlias;
                }

                //

                const double ElementClientDistPct = 1.0 / 12.0; // 相邻两元素有效区域的间距与元素边长之比。

                Int32 E = ElementArray[A.X, A.Y];

                bool EIsSolid = SolidFlagTable[A.X, A.Y];
                UInt32 EProbableValues = ProbableValuesTable[A.X, A.Y];
                bool EIsCorrect = CorrectionTable[A.X, A.Y];

                bool AIsPointed = (A == GameUIPointedIndex);
                bool AIsSelected = (A == OperatingIndex);
                bool OpIDIsCorrectAtA = (EProbableValues != 0 && (OperatingIndex.X >= 0 && OperatingIndex.X < Range.Width && OperatingIndex.Y >= 0 && OperatingIndex.Y < Range.Height) && Com.BitOperation.BinaryHasBit(EProbableValues, ElementArray[OperatingIndex.X, OperatingIndex.Y]));
                bool OpNumIsCorrectAtA = (EProbableValues != 0 && Com.BitOperation.BinaryHasBit(EProbableValues, OperatingNumber));
                bool EIsEqualToOpID = ((OperatingIndex.X >= 0 && OperatingIndex.X < Range.Width && OperatingIndex.Y >= 0 && OperatingIndex.Y < Range.Height) && E == ElementArray[OperatingIndex.X, OperatingIndex.Y]);
                bool EIsEqualToOpNum = (E == OperatingNumber);

                Action DrawSplitLine = () =>
                {
                    Color Cr_SL = Me.RecommendColors.Main_INC.ToColor();

                    if (!Timer_Timer.Enabled && !GameIsWin)
                    {
                        Cr_SL = Com.ColorManipulation.GetGrayscaleColor(Cr_SL);
                    }

                    List<Rectangle> Rect_SLs = new List<Rectangle>(2);

                    if (A.X > 0 && A.X < SudokuSize - 1)
                    {
                        if ((A.X + 1) % SudokuOrder == 0)
                        {
                            Rect_SLs.Add(new Rectangle(new Point((Int32)(ElementSize * (1 - ElementClientDistPct / 4)), 0), new Size((Int32)(ElementSize * ElementClientDistPct / 4 + 1), ElementSize)));
                        }
                        else if (A.X % SudokuOrder == 0)
                        {
                            Rect_SLs.Add(new Rectangle(new Point(-1, 0), new Size((Int32)(ElementSize * ElementClientDistPct / 4 + 1), ElementSize)));
                        }
                    }

                    if (A.Y > 0 && A.Y < SudokuSize - 1)
                    {
                        if ((A.Y + 1) % SudokuOrder == 0)
                        {
                            Rect_SLs.Add(new Rectangle(new Point(0, (Int32)(ElementSize * (1 - ElementClientDistPct / 4))), new Size(ElementSize, (Int32)(ElementSize * ElementClientDistPct / 4 + 1))));
                        }
                        else if (A.Y % SudokuOrder == 0)
                        {
                            Rect_SLs.Add(new Rectangle(new Point(0, -1), new Size(ElementSize, (Int32)(ElementSize * ElementClientDistPct / 4 + 1))));
                        }
                    }

                    if (Rect_SLs != null)
                    {
                        foreach (var V in Rect_SLs)
                        {
                            BmpGrap.FillRectangle(new SolidBrush(Cr_SL), V);
                        }
                    }
                };

                Action<bool, bool, bool> DrawNote = (IsHighlight, IsBordered, IsPointed) =>
                {
                    if (!ShowNotes)
                    {
                        IsHighlight = false;
                    }

                    Color Cr_Bk = (IsHighlight ? Com.ColorManipulation.ShiftLightnessByHSL(Me.RecommendColors.Main_DEC, 0.4).ToColor() : ElementArray_GetColor(E));
                    Color Cr_Bdr = Me.RecommendColors.Main.ToColor();
                    Color Cr_Ptd = Me.RecommendColors.Main.AtAlpha(96).ToColor();
                    Color Cr_Str = Com.ColorManipulation.ShiftLightnessByHSL(Cr_Bk, -0.5);

                    if (!Timer_Timer.Enabled && !GameIsWin)
                    {
                        Cr_Bdr = Com.ColorManipulation.GetGrayscaleColor(Cr_Bdr);
                        Cr_Bk = Com.ColorManipulation.GetGrayscaleColor(Cr_Bk);
                        Cr_Str = Com.ColorManipulation.GetGrayscaleColor(Cr_Str);
                    }

                    Rectangle Rect_Cen = new Rectangle(new Point((Int32)(ElementSize * ElementClientDistPct / 2), (Int32)(ElementSize * ElementClientDistPct / 2)), new Size((Int32)(Math.Max(1, ElementSize * (1 - ElementClientDistPct))), (Int32)(Math.Max(1, ElementSize * (1 - ElementClientDistPct)))));

                    GraphicsPath RndRect_Cen = Com.Geometry.CreateRoundedRectanglePath(Rect_Cen, (Int32)(ElementSize * ElementClientDistPct / 2));

                    BmpGrap.FillPath(new LinearGradientBrush(new Point(Rect_Cen.X - 1, Rect_Cen.Y - 1), new Point(Rect_Cen.Right, Rect_Cen.Bottom), Com.ColorManipulation.ShiftLightnessByHSL(Cr_Bk, 0.3), Cr_Bk), RndRect_Cen);

                    if (IsBordered)
                    {
                        PathGradientBrush PGB = new PathGradientBrush(RndRect_Cen)
                        {
                            CenterColor = Color.FromArgb(96, GameUIBackColor_DEC),
                            SurroundColors = new Color[] { Cr_Bdr },
                            FocusScales = new PointF(0.75F, 0.75F)
                        };
                        BmpGrap.FillPath(PGB, RndRect_Cen);
                        PGB.Dispose();
                    }

                    if (IsPointed)
                    {
                        BmpGrap.FillPath(new SolidBrush(Cr_Ptd), RndRect_Cen);
                    }

                    if (ShowNotes)
                    {
                        if (Timer_Timer.Enabled || GameIsWin)
                        {
                            List<Int32> Values = Com.BitOperation.GetBit1IndexOfBinary(EProbableValues);

                            if (EProbableValues != 0)
                            {
                                foreach (var V in Values)
                                {
                                    if (V >= 1 && V <= SudokuSize)
                                    {
                                        string StringText = V.ToString();

                                        Int32 X = (V - 1) % SudokuOrder, Y = (V - 1) / SudokuOrder;

                                        Color StringColor = Cr_Str;
                                        Font StringFont = Com.Text.GetSuitableFont(StringText, new Font("微软雅黑", 9F, FontStyle.Regular, GraphicsUnit.Point, 134), new SizeF(Rect_Cen.Width / 3F, Rect_Cen.Height / 3F));
                                        RectangleF StringRect = new RectangleF();
                                        StringRect.Size = BmpGrap.MeasureString(StringText, StringFont);
                                        StringRect.Location = new PointF(Rect_Cen.X + X * Rect_Cen.Width / (float)SudokuOrder + (Rect_Cen.Width / (float)SudokuOrder - StringRect.Width) / 2, Rect_Cen.Y + Y * Rect_Cen.Height / (float)SudokuOrder + (Rect_Cen.Height / (float)SudokuOrder - StringRect.Height) / 2);

                                        Com.Painting2D.PaintTextWithShadow(Bmp, StringText, StringFont, StringColor, StringColor, StringRect.Location, 0.02F, AntiAlias);
                                    }
                                }
                            }
                        }
                    }
                };

                Action<bool, bool, bool, bool> DrawNumber = (IsSolid, IsCorrect, IsBordered, IsPointed) =>
                {
                    if (!ShowNotes || IsSolid)
                    {
                        IsCorrect = true;
                    }

                    if (!IsCorrect)
                    {
                        IsBordered = true;
                    }

                    Color Cr_Bk = (IsSolid ? Com.ColorManipulation.GetGrayscaleColor(ElementArray_GetColor(E)) : (IsCorrect ? ElementArray_GetColor(E) : new Com.ColorX(Color.Crimson).AtLightness_HSL(60).ToColor()));
                    Color Cr_Bdr = (IsCorrect ? Me.RecommendColors.Main.ToColor() : new Com.ColorX(Color.Crimson).AtLightness_HSL(52).ToColor());
                    Color Cr_Ptd = Color.FromArgb(96, GameUIBackColor_DEC);
                    Color Cr_Str = Com.ColorManipulation.ShiftLightnessByHSL((IsCorrect ? Cr_Bk : new Com.ColorX(Color.Crimson).AtLightness_HSL(60).ToColor()), -0.5);

                    if (!Timer_Timer.Enabled && !GameIsWin)
                    {
                        Cr_Bdr = Com.ColorManipulation.GetGrayscaleColor(Cr_Bdr);
                        Cr_Bk = Com.ColorManipulation.GetGrayscaleColor(Cr_Bk);
                        Cr_Str = Com.ColorManipulation.GetGrayscaleColor(Cr_Str);
                    }

                    Rectangle Rect_Cen = new Rectangle(new Point((Int32)(ElementSize * ElementClientDistPct / 2), (Int32)(ElementSize * ElementClientDistPct / 2)), new Size((Int32)(Math.Max(1, ElementSize * (1 - ElementClientDistPct))), (Int32)(Math.Max(1, ElementSize * (1 - ElementClientDistPct)))));

                    GraphicsPath RndRect_Cen = Com.Geometry.CreateRoundedRectanglePath(Rect_Cen, (Int32)(ElementSize * ElementClientDistPct / 2));

                    BmpGrap.FillPath(new LinearGradientBrush(new Point(Rect_Cen.X - 1, Rect_Cen.Y - 1), new Point(Rect_Cen.Right, Rect_Cen.Bottom), Com.ColorManipulation.ShiftLightnessByHSL(Cr_Bk, 0.3), Cr_Bk), RndRect_Cen);

                    if (IsBordered)
                    {
                        PathGradientBrush PGB = new PathGradientBrush(RndRect_Cen)
                        {
                            CenterColor = Color.FromArgb(96, GameUIBackColor_DEC),
                            SurroundColors = new Color[] { Cr_Bdr },
                            FocusScales = new PointF(0.75F, 0.75F)
                        };
                        BmpGrap.FillPath(PGB, RndRect_Cen);
                        PGB.Dispose();
                    }

                    if (IsPointed)
                    {
                        BmpGrap.FillPath(new SolidBrush(Cr_Ptd), RndRect_Cen);
                    }

                    if (Timer_Timer.Enabled || GameIsWin)
                    {
                        string StringText = ((E > 0 && E <= SudokuSize) ? E.ToString() : string.Empty);

                        if (StringText.Length > 0)
                        {
                            Color StringColor = Cr_Str;
                            Font StringFont = Com.Text.GetSuitableFont(StringText, new Font("微软雅黑", 9F, FontStyle.Regular, GraphicsUnit.Point, 134), new SizeF(Rect_Cen.Width * 0.8F, Rect_Cen.Height * 0.8F));
                            RectangleF StringRect = new RectangleF();
                            StringRect.Size = BmpGrap.MeasureString(StringText, StringFont);
                            StringRect.Location = new PointF(Rect_Cen.X + (Rect_Cen.Width - StringRect.Width) / 2, Rect_Cen.Y + (Rect_Cen.Height - StringRect.Height) / 2);

                            Com.Painting2D.PaintTextWithShadow(Bmp, StringText, StringFont, StringColor, StringColor, StringRect.Location, 0.02F, AntiAlias);
                        }
                    }
                };

                DrawSplitLine();

                if (E == 0)
                {
                    DrawNote(OpIDIsCorrectAtA || OpNumIsCorrectAtA, AIsSelected, AIsPointed);
                }
                else
                {
                    DrawNumber(EIsSolid, EIsCorrect, (AIsSelected || EIsEqualToOpID || EIsEqualToOpNum), AIsPointed);
                }

                //

                if (Bmp != null)
                {
                    EAryBmpGrap.DrawImage(Bmp, BmpRect.Location);

                    if (PresentNow)
                    {
                        Panel_Environment.CreateGraphics().DrawImage(Bmp, new Point(EAryBmpRect.X + BmpRect.X, EAryBmpRect.Y + BmpRect.Y));
                    }
                }
            }
        }

        private void ElementArray_RepresentAll()
        {
            //
            // 更新并呈现元素矩阵包含的所有元素。
            //

            if (Panel_Environment.Visible && (Panel_Environment.Width > 0 && Panel_Environment.Height > 0))
            {
                if (EAryBmp != null)
                {
                    EAryBmp.Dispose();
                }

                EAryBmp = new Bitmap(Math.Max(1, EAryBmpRect.Width), Math.Max(1, EAryBmpRect.Height));

                EAryBmpGrap = Graphics.FromImage(EAryBmp);

                if (AntiAlias)
                {
                    EAryBmpGrap.SmoothingMode = SmoothingMode.AntiAlias;
                    EAryBmpGrap.TextRenderingHint = TextRenderingHint.AntiAlias;
                }

                EAryBmpGrap.Clear(GameUIBackColor_INC);

                //

                for (int X = 0; X < Range.Width; X++)
                {
                    for (int Y = 0; Y < Range.Height; Y++)
                    {
                        ElementArray_DrawAtPoint(new Point(X, Y), false);
                    }
                }

                //

                ElementArray_UpdateCandidateNumberArea(0, false);

                //

                if (!Timer_Timer.Enabled)
                {
                    EAryBmpGrap.FillRectangle(new SolidBrush(Color.FromArgb(128, Color.White)), new Rectangle(new Point(0, 0), EAryBmp.Size));

                    //

                    string StringText = string.Empty;
                    Color StringColor = Me.RecommendColors.Text.ToColor();

                    if (GameIsWin)
                    {
                        StringText = "成功";
                    }
                    else
                    {
                        StringText = "已暂停";
                    }

                    Font StringFont = Com.Text.GetSuitableFont(StringText, new Font("微软雅黑", 9F, FontStyle.Regular, GraphicsUnit.Point, 134), new SizeF(EAryBmp.Width * 0.8F, EAryBmp.Height * 0.2F));
                    RectangleF StringRect = new RectangleF();
                    StringRect.Size = EAryBmpGrap.MeasureString(StringText, StringFont);
                    StringRect.Location = new PointF((EAryBmp.Width - StringRect.Width) / 2, (EAryBmp.Height - StringRect.Height) / 2);

                    Color StringBkColor = Com.ColorManipulation.ShiftLightnessByHSL(StringColor, 0.5);
                    Rectangle StringBkRect = new Rectangle(new Point(0, (Int32)StringRect.Y), new Size(EAryBmp.Width, Math.Max(1, (Int32)StringRect.Height)));

                    GraphicsPath Path_StringBk = new GraphicsPath();
                    Path_StringBk.AddRectangle(StringBkRect);
                    PathGradientBrush PGB_StringBk = new PathGradientBrush(Path_StringBk)
                    {
                        CenterColor = Color.FromArgb(192, StringBkColor),
                        SurroundColors = new Color[] { Color.Transparent },
                        FocusScales = new PointF(0F, 1F)
                    };
                    EAryBmpGrap.FillPath(PGB_StringBk, Path_StringBk);
                    Path_StringBk.Dispose();
                    PGB_StringBk.Dispose();

                    Com.Painting2D.PaintTextWithShadow(EAryBmp, StringText, StringFont, StringColor, StringColor, StringRect.Location, 0.02F, AntiAlias);
                }

                //

                RepaintEAryBmp();
            }
        }

        private void ElementArray_PresentAt(Point A)
        {
            //
            // 呈现元素矩阵中指定的索引处的一个元素。A：索引。
            //

            if (Panel_Environment.Visible && (Panel_Environment.Width > 0 && Panel_Environment.Height > 0))
            {
                if (A.X >= 0 && A.X < Range.Width && A.Y >= 0 && A.Y < Range.Height)
                {
                    ElementArray_DrawAtPoint(A, true);
                }
            }
        }

        private void RepaintEAryBmp()
        {
            //
            // 重绘元素矩阵位图。
            //

            if (EAryBmp != null)
            {
                if (Panel_Environment.Width > EAryBmp.Width)
                {
                    Panel_Environment.CreateGraphics().FillRectangles(new SolidBrush(GameUIBackColor_DEC), new Rectangle[] { new Rectangle(new Point(0, 0), new Size((Panel_Environment.Width - EAryBmp.Width) / 2, Panel_Environment.Height)), new Rectangle(new Point(Panel_Environment.Width - (Panel_Environment.Width - EAryBmp.Width) / 2, 0), new Size((Panel_Environment.Width - EAryBmp.Width) / 2, Panel_Environment.Height)) });
                }

                if (Panel_Environment.Height > EAryBmp.Height)
                {
                    Panel_Environment.CreateGraphics().FillRectangles(new SolidBrush(GameUIBackColor_DEC), new Rectangle[] { new Rectangle(new Point(0, 0), new Size(Panel_Environment.Width, (Panel_Environment.Height - EAryBmp.Height) / 2)), new Rectangle(new Point(0, Panel_Environment.Height - (Panel_Environment.Height - EAryBmp.Height) / 2), new Size(Panel_Environment.Width, (Panel_Environment.Height - EAryBmp.Height) / 2)) });
                }

                Panel_Environment.CreateGraphics().DrawImage(EAryBmp, EAryBmpRect.Location);
            }
        }

        private void Panel_Environment_Paint(object sender, PaintEventArgs e)
        {
            //
            // Panel_Environment 绘图。
            //

            if (EAryBmp != null)
            {
                if (Panel_Environment.Width > EAryBmp.Width)
                {
                    e.Graphics.FillRectangles(new SolidBrush(GameUIBackColor_DEC), new Rectangle[] { new Rectangle(new Point(0, 0), new Size((Panel_Environment.Width - EAryBmp.Width) / 2, Panel_Environment.Height)), new Rectangle(new Point(Panel_Environment.Width - (Panel_Environment.Width - EAryBmp.Width) / 2, 0), new Size((Panel_Environment.Width - EAryBmp.Width) / 2, Panel_Environment.Height)) });
                }

                if (Panel_Environment.Height > EAryBmp.Height)
                {
                    e.Graphics.FillRectangles(new SolidBrush(GameUIBackColor_DEC), new Rectangle[] { new Rectangle(new Point(0, 0), new Size(Panel_Environment.Width, (Panel_Environment.Height - EAryBmp.Height) / 2)), new Rectangle(new Point(0, Panel_Environment.Height - (Panel_Environment.Height - EAryBmp.Height) / 2), new Size(Panel_Environment.Width, (Panel_Environment.Height - EAryBmp.Height) / 2)) });
                }

                e.Graphics.DrawImage(EAryBmp, EAryBmpRect.Location);
            }
        }

        #endregion

        #region 元素矩阵高级功能

        // 候选操作数区域。

        private const double CandidateNumberAreaDist = 0.5; // 候选操作数区域与元素矩阵位图间的距离相对于元素边长的倍数。
        private const double CandidateNumberAreaSize = 1.0; // 候选操作数区域的宽度与高度中的较小者相对于元素边长的倍数。
        private Rectangle CandidateNumberArea = new Rectangle(); // 候选操作数区域（相对于绘图容器）。

        private void ElementArray_UpdateCandidateNumberArea(Int32 UpdateNumber, bool PresentNow)
        {
            //
            // 更新候选操作数区域。UpdateNumber：需要更新的候选操作数，0 表示更新所有；PresentNow：是否立即呈现此区域，如果为 true，那么将在位图中绘制此区域，并在不重绘整个位图的情况下在容器中绘制此区域，如果为 false，那么将仅在位图中绘制此区域。
            //

            Action<Int32> DrawCandidateNumber = (Number) =>
            {
                Rectangle BmpRect = new Rectangle();
                BmpRect.Size = new Size(ElementSize, ElementSize);

                if (CandidateNumberArea.Width >= CandidateNumberArea.Height)
                {
                    BmpRect.Location = new Point((CandidateNumberArea.X - EAryBmpRect.X) + (Number - 1) * ElementSize, CandidateNumberArea.Y - EAryBmpRect.Y);
                }
                else
                {
                    BmpRect.Location = new Point(CandidateNumberArea.X - EAryBmpRect.X, (CandidateNumberArea.Y - EAryBmpRect.Y) + (Number - 1) * ElementSize);
                }

                Bitmap Bmp = new Bitmap(BmpRect.Width, BmpRect.Height);

                Graphics BmpGrap = Graphics.FromImage(Bmp);

                if (AntiAlias)
                {
                    BmpGrap.SmoothingMode = SmoothingMode.AntiAlias;
                    BmpGrap.TextRenderingHint = TextRenderingHint.AntiAlias;
                }

                //

                const double ElementClientDistPct = 1.0 / 12.0; // 相邻两元素有效区域的间距与元素边长之比。

                Int32 Surplus = ElementArray_GetSurplusOfCandidateNumber(Number);
                bool IsBordered = (Number == OperatingNumber);
                bool IsPointed = (Number == GameUIPointedNumber);

                Color Cr_Bk = (Surplus <= 0 ? Com.ColorManipulation.GetGrayscaleColor(ElementArray_GetColor(Number)) : ElementArray_GetColor(Number));
                Color Cr_Bdr = Me.RecommendColors.Main.ToColor();
                Color Cr_Ptd = Color.FromArgb(96, GameUIBackColor_DEC);
                Color Cr_Str = Com.ColorManipulation.ShiftLightnessByHSL(Cr_Bk, -0.5);

                if (!Timer_Timer.Enabled && !GameIsWin)
                {
                    Cr_Bdr = Com.ColorManipulation.GetGrayscaleColor(Cr_Bdr);
                    Cr_Bk = Com.ColorManipulation.GetGrayscaleColor(Cr_Bk);
                    Cr_Str = Com.ColorManipulation.GetGrayscaleColor(Cr_Str);
                }

                Rectangle Rect_Cen = new Rectangle(new Point((Int32)(ElementSize * ElementClientDistPct / 2), (Int32)(ElementSize * ElementClientDistPct / 2)), new Size((Int32)(Math.Max(1, ElementSize * (1 - ElementClientDistPct))), (Int32)(Math.Max(1, ElementSize * (1 - ElementClientDistPct)))));

                GraphicsPath RndRect_Cen = Com.Geometry.CreateRoundedRectanglePath(Rect_Cen, (Int32)(ElementSize * ElementClientDistPct / 2));

                BmpGrap.FillPath(new LinearGradientBrush(new Point(Rect_Cen.X - 1, Rect_Cen.Y - 1), new Point(Rect_Cen.Right, Rect_Cen.Bottom), Com.ColorManipulation.ShiftLightnessByHSL(Cr_Bk, 0.3), Cr_Bk), RndRect_Cen);

                if (IsBordered)
                {
                    PathGradientBrush PGB = new PathGradientBrush(RndRect_Cen)
                    {
                        CenterColor = Color.FromArgb(96, GameUIBackColor_DEC),
                        SurroundColors = new Color[] { Cr_Bdr },
                        FocusScales = new PointF(0.75F, 0.75F)
                    };
                    BmpGrap.FillPath(PGB, RndRect_Cen);
                    PGB.Dispose();
                }

                if (IsPointed)
                {
                    BmpGrap.FillPath(new SolidBrush(Cr_Ptd), RndRect_Cen);
                }

                string StringText = ((Number >= 1 && Number <= SudokuSize) ? Number.ToString() : string.Empty);

                if (StringText.Length > 0)
                {
                    Color StringColor = Cr_Str;
                    Font StringFont = Com.Text.GetSuitableFont(StringText, new Font("微软雅黑", 9F, FontStyle.Regular, GraphicsUnit.Point, 134), new SizeF(Rect_Cen.Width * 0.8F, Rect_Cen.Height * 0.8F));
                    RectangleF StringRect = new RectangleF();
                    StringRect.Size = BmpGrap.MeasureString(StringText, StringFont);
                    StringRect.Location = new PointF(Rect_Cen.X + (Rect_Cen.Width - StringRect.Width) / 2, Rect_Cen.Y + (Rect_Cen.Height - StringRect.Height) / 2);

                    Com.Painting2D.PaintTextWithShadow(Bmp, StringText, StringFont, StringColor, StringColor, StringRect.Location, 0.02F, AntiAlias);
                }

                if (Timer_Timer.Enabled || GameIsWin)
                {
                    string StrSurText = ((Surplus >= 0 && Surplus <= SudokuSize) ? Surplus.ToString() : string.Empty);

                    if (StrSurText.Length > 0)
                    {
                        Color StrSurColor = Cr_Str;
                        Font StrSurFont = Com.Text.GetSuitableFont(StrSurText, new Font("微软雅黑", 9F, FontStyle.Regular, GraphicsUnit.Point, 134), new SizeF(Rect_Cen.Width / 3F, Rect_Cen.Height / 3F));
                        RectangleF StrSurRect = new RectangleF();
                        StrSurRect.Size = BmpGrap.MeasureString(StrSurText, StrSurFont);
                        StrSurRect.Location = new PointF(Rect_Cen.X + Rect_Cen.Width * 2F / 3F + (Rect_Cen.Width / 3F - StrSurRect.Width) / 2, Rect_Cen.Y + (Rect_Cen.Height / 3F - StrSurRect.Height) / 2);

                        Com.Painting2D.PaintTextWithShadow(Bmp, StrSurText, StrSurFont, StrSurColor, StrSurColor, StrSurRect.Location, 0.02F, AntiAlias);
                    }
                }

                if (AlwaysEnableKeyboard || OperationMode == OperationModes.Keyboard)
                {
                    string StrDuoText = ((Number >= 10 && Number <= SudokuSize) ? ((char)('A' + (Number - 10))).ToString() : string.Empty);

                    if (StrDuoText.Length > 0)
                    {
                        Color StrDuoColor = Cr_Str;
                        Font StrDuoFont = Com.Text.GetSuitableFont(StrDuoText, new Font("微软雅黑", 9F, FontStyle.Regular, GraphicsUnit.Point, 134), new SizeF(Rect_Cen.Width / 3F, Rect_Cen.Height / 3F));
                        RectangleF StrDuoRect = new RectangleF();
                        StrDuoRect.Size = BmpGrap.MeasureString(StrDuoText, StrDuoFont);
                        StrDuoRect.Location = new PointF(Rect_Cen.X + (Rect_Cen.Width / 3F - StrDuoRect.Width) / 2, Rect_Cen.Y + Rect_Cen.Height * 2F / 3F + (Rect_Cen.Height / 3F - StrDuoRect.Height) / 2);

                        Com.Painting2D.PaintTextWithShadow(Bmp, StrDuoText, StrDuoFont, StrDuoColor, StrDuoColor, StrDuoRect.Location, 0.02F, AntiAlias);
                    }
                }

                //

                if (Bmp != null)
                {
                    EAryBmpGrap.DrawImage(Bmp, BmpRect.Location);

                    if (PresentNow)
                    {
                        Panel_Environment.CreateGraphics().DrawImage(Bmp, new Point(EAryBmpRect.X + BmpRect.X, EAryBmpRect.Y + BmpRect.Y));
                    }
                }
            };

            if (UpdateNumber == 0)
            {
                for (int i = 1; i <= SudokuSize; i++)
                {
                    DrawCandidateNumber(i);
                }
            }
            else
            {
                if (UpdateNumber >= 1 && UpdateNumber <= SudokuVolume)
                {
                    DrawCandidateNumber(UpdateNumber);
                }
            }
        }

        // 固态标志表，可填值表，正确性表。

        private bool[,] SolidFlagTable = new bool[CAPACITY, CAPACITY]; // 固态标志表。其任何一个元素表示数独中指定索引处的数值是否为命题时生成。
        private UInt32[,] ProbableValuesTable = new UInt32[CAPACITY, CAPACITY]; // 可填值表。
        private bool[,] CorrectionTable = new bool[CAPACITY, CAPACITY]; // 正确性表。

        private void ElementArray_UpdateProbableValuesTable()
        {
            //
            // 更新可填值表。
            //

            CalcPVTOfSudoku(ElementArray, ProbableValuesTable);
        }

        private void ElementArray_UpdateCorrectionTable()
        {
            //
            // 更新正确性表。
            //

            CalcCTOfSudoku(ElementArray, SolidFlagTable, CorrectionTable);
        }

        // 操作索引，操作数，操作方向。

        private Point _OperatingIndex = new Point(-1, -1); // 当前的操作索引。
        private Point OperatingIndex
        {
            get
            {
                return _OperatingIndex;
            }

            set
            {
                if (value.X >= 0 && value.X < Range.Width && value.Y >= 0 && value.Y < Range.Height)
                {
                    _OperatingIndex = value;
                }
                else
                {
                    _OperatingIndex = new Point(-1, -1);
                }
            }
        }

        private Int32 _OperatingNumber = -1; // 当前的操作数。
        private Int32 OperatingNumber
        {
            get
            {
                return _OperatingNumber;
            }

            set
            {
                if (value >= 0 && value <= SudokuSize)
                {
                    _OperatingNumber = value;
                }
                else
                {
                    _OperatingNumber = -1;
                }

                PictureBox_Erase.Refresh();
            }
        }

        private Int32 ElementArray_GetCandidateNumberAt(Point P)
        {
            //
            // 获取绘图容器中的指定坐标所在的候选操作数。P：坐标。
            //

            Int32 Number = -1;

            if (Com.Geometry.PointIsVisibleInRectangle(new Com.PointD(P), CandidateNumberArea))
            {
                if (CandidateNumberArea.Width >= CandidateNumberArea.Height)
                {
                    Number = (Int32)Math.Floor((double)(P.X - CandidateNumberArea.X) / ElementSize) + 1;
                }
                else
                {
                    Number = (Int32)Math.Floor((double)(P.Y - CandidateNumberArea.Y) / ElementSize) + 1;
                }

                if (!(Number >= 1 && Number <= SudokuSize))
                {
                    Number = -1;
                }
            }

            return Number;
        }

        private void ElementArray_ResetOperatingIndexAndNumber()
        {
            //
            // 重置当前的操作索引与操作数，重绘整个位图并进行判定。
            //

            if (!GameIsWin && Timer_Timer.Enabled)
            {
                if ((OperatingIndex.X >= 0 && OperatingIndex.X < Range.Width && OperatingIndex.Y >= 0 && OperatingIndex.Y < Range.Height) || (OperatingNumber >= 0 && OperatingNumber <= SudokuSize))
                {
                    OperatingIndex = new Point(-1, -1);
                    OperatingNumber = -1;

                    ElementArray_RepresentAll();

                    Judgement();
                }
            }
        }

        private void ElementArray_BasicLogicalOperation(Point A, bool IsFinal)
        {
            //
            // 对指定索引的基本逻辑操作。A：索引；IsFinal：是否是最终操作，如果是，将重绘整个位图并进行判定。
            //

            if (!GameIsWin && Timer_Timer.Enabled)
            {
                if (A.X >= 0 && A.X < Range.Width && A.Y >= 0 && A.Y < Range.Height)
                {
                    Step S = new Step();

                    Action CopyStep = () =>
                    {
                        S.ElementArray = GetCopyOfArray(ElementArray);
                    };

                    Action SaveStep = () =>
                    {
                        StepList_Append(S);

                        ThisRecord.StepCount += 1;

                        ElementArray_UpdateProbableValuesTable();
                        ElementArray_UpdateCorrectionTable();
                    };

                    if (OperatingIndex == A)
                    {
                        OperatingIndex = new Point(-1, -1);
                    }
                    else
                    {
                        OperatingIndex = A;

                        if (!SolidFlagTable[OperatingIndex.X, OperatingIndex.Y])
                        {
                            Int32 E = ElementArray[OperatingIndex.X, OperatingIndex.Y];

                            if (E == 0)
                            {
                                if (OperatingNumber == 0)
                                {
                                    OperatingNumber = -1;
                                }
                                else if (OperatingNumber >= 1 && OperatingNumber <= SudokuSize)
                                {
                                    if (ElementArray_GetSurplusOfCandidateNumber(OperatingNumber) > 0)
                                    {
                                        CopyStep();

                                        ElementArray_Add(OperatingIndex, OperatingNumber);

                                        SaveStep();

                                        OperatingIndex = new Point(-1, -1);
                                    }
                                    else
                                    {
                                        OperatingNumber = -1;
                                    }
                                }
                            }
                            else
                            {
                                if (E == OperatingNumber)
                                {
                                    CopyStep();

                                    ElementArray_RemoveAt(OperatingIndex);

                                    SaveStep();

                                    OperatingIndex = new Point(-1, -1);
                                }
                                else
                                {
                                    if (OperatingNumber == 0)
                                    {
                                        CopyStep();

                                        ElementArray_RemoveAt(OperatingIndex);

                                        SaveStep();

                                        OperatingIndex = new Point(-1, -1);
                                    }
                                    else if (OperatingNumber >= 1 && OperatingNumber <= SudokuSize)
                                    {
                                        if (ElementArray_GetSurplusOfCandidateNumber(OperatingNumber) > 0)
                                        {
                                            CopyStep();

                                            ElementArray_RemoveAt(OperatingIndex);
                                            ElementArray_Add(OperatingIndex, OperatingNumber);

                                            SaveStep();

                                            OperatingIndex = new Point(-1, -1);
                                        }
                                        else
                                        {
                                            OperatingNumber = -1;
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            OperatingNumber = -1;
                        }
                    }
                }
                else
                {
                    OperatingIndex = new Point(-1, -1);
                }

                if (IsFinal)
                {
                    ElementArray_RepresentAll();

                    Judgement();
                }
            }
        }

        private void ElementArray_BasicLogicalOperation(Int32 Number, bool IsFinal)
        {
            //
            // 对指定操作数的基本逻辑操作。Number：操作数；IsFinal：是否是最终操作，如果是，将重绘整个位图并进行判定。
            //

            if (!GameIsWin && Timer_Timer.Enabled)
            {
                if (Number >= 0 && Number <= SudokuSize)
                {
                    Step S = new Step();

                    Action CopyStep = () =>
                    {
                        S.ElementArray = GetCopyOfArray(ElementArray);
                    };

                    Action SaveStep = () =>
                    {
                        StepList_Append(S);

                        ThisRecord.StepCount += 1;

                        ElementArray_UpdateProbableValuesTable();
                        ElementArray_UpdateCorrectionTable();
                    };

                    if (OperatingNumber == Number)
                    {
                        OperatingNumber = -1;
                    }
                    else
                    {
                        OperatingNumber = Number;

                        if (OperatingIndex.X >= 0 && OperatingIndex.X < Range.Width && OperatingIndex.Y >= 0 && OperatingIndex.Y < Range.Height)
                        {
                            Int32 E = ElementArray[OperatingIndex.X, OperatingIndex.Y];

                            if (Number == 0)
                            {
                                if (!SolidFlagTable[OperatingIndex.X, OperatingIndex.Y])
                                {
                                    if (E > 0)
                                    {
                                        CopyStep();

                                        ElementArray_RemoveAt(OperatingIndex);

                                        SaveStep();

                                        OperatingNumber = -1;
                                    }
                                }

                                OperatingIndex = new Point(-1, -1);
                            }
                            else
                            {
                                if (!SolidFlagTable[OperatingIndex.X, OperatingIndex.Y])
                                {
                                    if (E == 0)
                                    {
                                        if (ElementArray_GetSurplusOfCandidateNumber(OperatingNumber) > 0)
                                        {
                                            CopyStep();

                                            ElementArray_Add(OperatingIndex, OperatingNumber);

                                            SaveStep();
                                        }

                                        OperatingIndex = new Point(-1, -1);
                                    }
                                    else
                                    {
                                        if (E == OperatingNumber)
                                        {
                                            CopyStep();

                                            ElementArray_RemoveAt(OperatingIndex);

                                            SaveStep();

                                            OperatingIndex = new Point(-1, -1);
                                        }
                                        else
                                        {
                                            if (ElementArray_GetSurplusOfCandidateNumber(OperatingNumber) > 0)
                                            {
                                                CopyStep();

                                                ElementArray_RemoveAt(OperatingIndex);
                                                ElementArray_Add(OperatingIndex, OperatingNumber);

                                                SaveStep();

                                                OperatingIndex = new Point(-1, -1);
                                            }
                                        }
                                    }

                                    OperatingNumber = -1;
                                }
                                else
                                {
                                    OperatingIndex = new Point(-1, -1);
                                }
                            }
                        }
                    }
                }
                else
                {
                    OperatingNumber = -1;
                }

                if (IsFinal)
                {
                    ElementArray_RepresentAll();

                    Judgement();
                }
            }
        }

        private void ElementArray_KeyOperation(Keys KeyCode)
        {
            //
            // 对指定键盘键的操作。KeyCode：键盘键代码。
            //

            if (!GameIsWin && Timer_Timer.Enabled)
            {
                Action<char> KeyDownDirection = (Direction) =>
                {
                    Point A = OperatingIndex;

                    if (A.X >= 0 && A.X < Range.Width && A.Y >= 0 && A.Y < Range.Height)
                    {
                        switch (Direction)
                        {
                            case 'L':
                            case 'l':
                                if (A.X >= 1)
                                {
                                    A.X--;
                                }
                                else
                                {
                                    A.X = Range.Width - 1;
                                }
                                break;

                            case 'R':
                            case 'r':
                                if (A.X <= Range.Width - 2)
                                {
                                    A.X++;
                                }
                                else
                                {
                                    A.X = 0;
                                }
                                break;

                            case 'U':
                            case 'u':
                                if (A.Y >= 1)
                                {
                                    A.Y--;
                                }
                                else
                                {
                                    A.Y = Range.Height - 1;
                                }
                                break;

                            case 'D':
                            case 'd':
                                if (A.Y <= Range.Height - 2)
                                {
                                    A.Y++;
                                }
                                else
                                {
                                    A.Y = 0;
                                }
                                break;
                        }
                    }
                    else
                    {
                        A = new Point(0, 0);
                    }

                    ElementArray_BasicLogicalOperation(A, true);
                };

                Action<Int32> KeyDownNumber = (Number) =>
                {
                    if (Number >= 0 && Number <= SudokuSize)
                    {
                        if ((OperatingIndex.X >= 0 && OperatingIndex.X < Range.Width && OperatingIndex.Y >= 0 && OperatingIndex.Y < Range.Height))
                        {
                            if (!SolidFlagTable[OperatingIndex.X, OperatingIndex.Y])
                            {
                                Int32 E = ElementArray[OperatingIndex.X, OperatingIndex.Y];

                                if ((E == 0 && Number != 0) || E != 0)
                                {
                                    Point A = OperatingIndex;

                                    ElementArray_BasicLogicalOperation(Number, true);

                                    if (!GameIsWin)
                                    {
                                        ElementArray_BasicLogicalOperation(A, true);
                                    }
                                }
                            }
                        }
                    }
                };

                switch (KeyCode)
                {
                    case Keys.Left: KeyDownDirection('L'); break;
                    case Keys.Right: KeyDownDirection('R'); break;
                    case Keys.Up: KeyDownDirection('U'); break;
                    case Keys.Down: KeyDownDirection('D'); break;

                    case Keys.Back:
                    case Keys.Delete:
                    case Keys.D0:
                    case Keys.NumPad0: KeyDownNumber(0); break;
                    case Keys.D1:
                    case Keys.NumPad1: KeyDownNumber(1); break;
                    case Keys.D2:
                    case Keys.NumPad2: KeyDownNumber(2); break;
                    case Keys.D3:
                    case Keys.NumPad3: KeyDownNumber(3); break;
                    case Keys.D4:
                    case Keys.NumPad4: KeyDownNumber(4); break;
                    case Keys.D5:
                    case Keys.NumPad5: KeyDownNumber(5); break;
                    case Keys.D6:
                    case Keys.NumPad6: KeyDownNumber(6); break;
                    case Keys.D7:
                    case Keys.NumPad7: KeyDownNumber(7); break;
                    case Keys.D8:
                    case Keys.NumPad8: KeyDownNumber(8); break;
                    case Keys.D9:
                    case Keys.NumPad9: KeyDownNumber(9); break;
                    case Keys.A: KeyDownNumber(10); break;
                    case Keys.B: KeyDownNumber(11); break;
                    case Keys.C: KeyDownNumber(12); break;
                    case Keys.D: KeyDownNumber(13); break;
                    case Keys.E: KeyDownNumber(14); break;
                    case Keys.F: KeyDownNumber(15); break;
                    case Keys.G: KeyDownNumber(16); break;
                    case Keys.H: KeyDownNumber(17); break;
                    case Keys.I: KeyDownNumber(18); break;
                    case Keys.J: KeyDownNumber(19); break;
                    case Keys.K: KeyDownNumber(20); break;
                    case Keys.L: KeyDownNumber(21); break;
                    case Keys.M: KeyDownNumber(22); break;
                    case Keys.N: KeyDownNumber(23); break;
                    case Keys.O: KeyDownNumber(24); break;
                    case Keys.P: KeyDownNumber(25); break;
                }
            }
        }

        private Point _MouseDownOpID = new Point(-1, -1); // 鼠标按下（或手指轻触）时鼠标指针所在的索引。
        private Int32 _MouseDownOpNum = -1; // 鼠标按下（或手指轻触）时鼠标指针所在的操作数。

        private void ElementArray_MouseDownOperation(Point MouseDownPt)
        {
            //
            // 对指定坐标的鼠标按下操作。MouseDownPt：鼠标按下（或手指轻触）时光标在绘图容器的坐标。
            //

            if (!GameIsWin && Timer_Timer.Enabled)
            {
                Point A = ElementArray_GetIndex(MouseDownPt);
                Int32 Number = ElementArray_GetCandidateNumberAt(MouseDownPt);

                if (A.X >= 0 && A.X < Range.Width && A.Y >= 0 && A.Y < Range.Height)
                {
                    if (OperationMode == OperationModes.Mouse)
                    {
                        ElementArray_BasicLogicalOperation(A, true);
                    }
                    else if (OperationMode == OperationModes.Touch)
                    {
                        _MouseDownOpID = A;
                    }
                }
                else if (Number >= 0 && Number <= SudokuSize)
                {
                    if (OperationMode == OperationModes.Mouse)
                    {
                        ElementArray_BasicLogicalOperation(Number, true);
                    }
                    else if (OperationMode == OperationModes.Touch)
                    {
                        _MouseDownOpNum = Number;
                    }
                }
                else
                {
                    if (OperationMode == OperationModes.Mouse)
                    {
                        ElementArray_ResetOperatingIndexAndNumber();
                    }
                }
            }
        }

        private void ElementArray_MouseUpOperation(Point MouseUpPt)
        {
            //
            // 对指定坐标的鼠标释放操作。MouseUpPt：鼠标释放（或手指离开）时光标在绘图容器的坐标。
            //

            if (OperationMode == OperationModes.Touch)
            {
                if (!GameIsWin && Timer_Timer.Enabled)
                {
                    Point A = ElementArray_GetIndex(MouseUpPt);
                    Int32 Number = ElementArray_GetCandidateNumberAt(MouseUpPt);

                    bool _MouseDownOpIDIsValid = (_MouseDownOpID.X >= 0 && _MouseDownOpID.X < Range.Width && _MouseDownOpID.Y >= 0 && _MouseDownOpID.Y < Range.Height);
                    bool _MouseDownOpNumIsValid = (_MouseDownOpNum >= 0 && _MouseDownOpNum <= SudokuSize);

                    if (_MouseDownOpIDIsValid || _MouseDownOpNumIsValid)
                    {
                        if (A.X >= 0 && A.X < Range.Width && A.Y >= 0 && A.Y < Range.Height)
                        {
                            if (A == _MouseDownOpID)
                            {
                                ElementArray_BasicLogicalOperation(A, true);
                            }
                            else
                            {
                                if (!SolidFlagTable[A.X, A.Y])
                                {
                                    Int32 E = ElementArray[A.X, A.Y];

                                    if (_MouseDownOpIDIsValid)
                                    {
                                        Int32 _Num = ElementArray[_MouseDownOpID.X, _MouseDownOpID.Y];

                                        if (!SolidFlagTable[_MouseDownOpID.X, _MouseDownOpID.Y] && _Num != 0)
                                        {
                                            if (E == 0)
                                            {
                                                if (OperatingIndex != _MouseDownOpID)
                                                {
                                                    ElementArray_BasicLogicalOperation(_MouseDownOpID, false);
                                                }

                                                ElementArray_BasicLogicalOperation(0, false);

                                                ElementArray_BasicLogicalOperation(A, false);
                                                ElementArray_BasicLogicalOperation(_Num, true);
                                            }
                                            else
                                            {
                                                if (OperatingIndex != _MouseDownOpID)
                                                {
                                                    ElementArray_BasicLogicalOperation(_MouseDownOpID, false);
                                                }

                                                ElementArray_BasicLogicalOperation(0, false);

                                                ElementArray_BasicLogicalOperation(A, false);
                                                ElementArray_BasicLogicalOperation(0, false);

                                                ElementArray_BasicLogicalOperation(_MouseDownOpID, false);
                                                ElementArray_BasicLogicalOperation(E, false);

                                                ElementArray_BasicLogicalOperation(A, false);
                                                ElementArray_BasicLogicalOperation(_Num, true);
                                            }
                                        }
                                    }
                                    else if (_MouseDownOpNumIsValid)
                                    {
                                        if (OperatingIndex != A)
                                        {
                                            ElementArray_BasicLogicalOperation(A, false);
                                        }

                                        ElementArray_BasicLogicalOperation(_MouseDownOpNum, true);
                                    }
                                }
                            }
                        }
                        else if (Number >= 0 && Number <= SudokuSize)
                        {
                            if (Number == _MouseDownOpNum)
                            {
                                ElementArray_BasicLogicalOperation(Number, true);
                            }
                            else
                            {
                                if (_MouseDownOpIDIsValid)
                                {
                                    Int32 _Num = ElementArray[_MouseDownOpID.X, _MouseDownOpID.Y];

                                    if (!SolidFlagTable[_MouseDownOpID.X, _MouseDownOpID.Y] && _Num != 0)
                                    {
                                        if (OperatingIndex != _MouseDownOpID)
                                        {
                                            ElementArray_BasicLogicalOperation(_MouseDownOpID, false);
                                        }

                                        ElementArray_BasicLogicalOperation(0, true);
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (_MouseDownOpIDIsValid)
                            {
                                Int32 _Num = ElementArray[_MouseDownOpID.X, _MouseDownOpID.Y];

                                if (!SolidFlagTable[_MouseDownOpID.X, _MouseDownOpID.Y] && _Num != 0)
                                {
                                    if (OperatingIndex != _MouseDownOpID)
                                    {
                                        ElementArray_BasicLogicalOperation(_MouseDownOpID, false);
                                    }

                                    ElementArray_BasicLogicalOperation(0, true);
                                }
                                else
                                {
                                    ElementArray_ResetOperatingIndexAndNumber();
                                }
                            }
                            else
                            {
                                ElementArray_ResetOperatingIndexAndNumber();
                            }
                        }
                    }
                    else
                    {
                        ElementArray_ResetOperatingIndexAndNumber();
                    }

                    _MouseDownOpID = new Point(-1, -1);
                    _MouseDownOpNum = -1;
                }
            }
        }

        // 计算与判断。

        private bool ElementArray_SudokuIsCompleted()
        {
            //
            // 依据已经更新的 CorrectionTable，判断数独是否已经填充完毕并且正确。
            //

            if (GetZeroCountOfArray(ElementArray, Range) == 0)
            {
                for (int X = 0; X < Range.Width; X++)
                {
                    for (int Y = 0; Y < Range.Height; Y++)
                    {
                        if (!CorrectionTable[X, Y])
                        {
                            return false;
                        }
                    }
                }

                return true;
            }

            return false;
        }

        private Int32 ElementArray_GetSurplusOfCandidateNumber(Int32 Number)
        {
            //
            // 获取指定候选操作数的剩余量。Number：候选操作数。
            //

            if (Number >= 1 && Number <= SudokuSize)
            {
                return (SudokuSize - GetCertainIndexListOfArray(ElementArray, Range, Number).Count);
            }

            return -1;
        }

        private Int32 SolidNumberCount // 依据已经更新的 SolidFlagTable，获取固态数值的数量。
        {
            get
            {
                Int32 Count = 0;

                for (int X = 0; X < Range.Width; X++)
                {
                    for (int Y = 0; Y < Range.Height; Y++)
                    {
                        if (SolidFlagTable[X, Y])
                        {
                            Count++;
                        }
                    }
                }

                return Count;
            }
        }

        private Int32 CorrectNumberCount // 依据已经更新的 CorrectionTable，获取填充正确的数值的数量（不含固态数值）。
        {
            get
            {
                Int32 Count = 0;

                foreach (var V in ElementIndexList)
                {
                    if (!SolidFlagTable[V.X, V.Y] && CorrectionTable[V.X, V.Y])
                    {
                        Count++;
                    }
                }

                return Count;
            }
        }

        #endregion

        #region 数独

        // 定义。

        private Int32 SudokuOrder // 数独的阶数（数独水平或垂直方向上的宫数，宫内水平或垂直方向上的元素数）。
        {
            get
            {
                switch (Order)
                {
                    case Orders.Order3: return 3;
                    case Orders.Order4: return 4;
                    case Orders.Order5: return 5;
                    default: return 3;
                }
            }
        }

        private Int32 SudokuSize // 数独水平或垂直方向上的元素数。
        {
            get
            {
                Int32 _SudokuOrder = SudokuOrder;

                return (_SudokuOrder * _SudokuOrder);
            }
        }

        private Int32 SudokuVolume // 数独的总元素数。
        {
            get
            {
                Int32 _SudokuSize = SudokuSize;

                return (_SudokuSize * _SudokuSize);
            }
        }

        // 可填值表与正确性表。

        private void CalcPVTOfSudoku(Int32[,] Sudoku, UInt32[,] ProbableValuesTable)
        {
            //
            // 计算一个数独的可填值表。可填值表为一个矩阵，其任何一个元素以二进制位的方式表示包含若干整数的集合，该集合表示数独中指定索引处所有可能填入的数值。Sudoku：数独；ProbableValuesTable：可填值表（作为输出）。
            //

            UInt32 All = 1;

            for (int i = 0; i < SudokuSize; i++)
            {
                All = (All << 1) + 1;
            }

            //

            UInt32[] PV_Row = new UInt32[SudokuSize];
            UInt32[] PV_Column = new UInt32[SudokuSize];
            UInt32[,] PV_Matrix = new UInt32[SudokuOrder, SudokuOrder];

            for (int i = 0; i < SudokuSize; i++)
            {
                PV_Row[i] = All;
                PV_Column[i] = All;
                PV_Matrix[i % SudokuOrder, i / SudokuOrder] = All;
            }

            //

            for (int Y = 0; Y < SudokuSize; Y++)
            {
                for (int X = 0; X < SudokuSize; X++)
                {
                    Com.BitOperation.RemoveBitFromBinary(ref PV_Row[Y], Sudoku[X, Y]);
                }
            }

            for (int X = 0; X < SudokuSize; X++)
            {
                for (int Y = 0; Y < SudokuSize; Y++)
                {
                    Com.BitOperation.RemoveBitFromBinary(ref PV_Column[X], Sudoku[X, Y]);
                }
            }

            for (int X = 0; X < SudokuOrder; X++)
            {
                for (int Y = 0; Y < SudokuOrder; Y++)
                {
                    Int32 _X = X * SudokuOrder, _Y = Y * SudokuOrder;

                    for (int y = _Y; y < _Y + SudokuOrder; y++)
                    {
                        for (int x = _X; x < _X + SudokuOrder; x++)
                        {
                            Com.BitOperation.RemoveBitFromBinary(ref PV_Matrix[X, Y], Sudoku[x, y]);
                        }
                    }
                }
            }

            //

            for (int X = 0; X < SudokuSize; X++)
            {
                for (int Y = 0; Y < SudokuSize; Y++)
                {
                    ProbableValuesTable[X, Y] = 0;

                    if (Sudoku[X, Y] == 0)
                    {
                        for (int i = 1; i <= SudokuSize; i++)
                        {
                            if (Com.BitOperation.BinaryHasBit(PV_Row[Y], i) && Com.BitOperation.BinaryHasBit(PV_Column[X], i) && Com.BitOperation.BinaryHasBit(PV_Matrix[X / SudokuOrder, Y / SudokuOrder], i))
                            {
                                Com.BitOperation.AddBitToBinary(ref ProbableValuesTable[X, Y], i);
                            }
                        }
                    }
                }
            }
        }

        private void CalcCTOfSudoku(Int32[,] Sudoku, bool[,] SolidFlagTable, bool[,] CorrectionTable)
        {
            //
            // 计算一个数独的正确性表。正确性表为一个矩阵，其任何一个元素表示数独中指定索引处填入的数值是否符合规则。Sudoku：数独；SolidFlagTable：固态标志表，其任何一个元素表示数独中指定索引处的数值是否为命题时生成；CorrectionTable：正确性表（作为输出）。
            //

            bool[,] Setted = new bool[SudokuSize, SudokuSize];

            for (int X = 0; X < SudokuSize; X++)
            {
                for (int Y = 0; Y < SudokuSize; Y++)
                {
                    Int32 E = Sudoku[X, Y];

                    if (E > 0 && E <= SudokuSize)
                    {
                        if (!SolidFlagTable[X, Y])
                        {
                            if (!Setted[X, Y])
                            {
                                for (int _X = 0; _X < SudokuSize; _X++)
                                {
                                    if (_X != X && E == Sudoku[_X, Y])
                                    {
                                        CorrectionTable[X, Y] = false;

                                        Setted[X, Y] = true;

                                        if (!SolidFlagTable[_X, Y])
                                        {
                                            CorrectionTable[_X, Y] = false;

                                            Setted[_X, Y] = true;
                                        }

                                        break;
                                    }
                                }
                            }

                            if (!Setted[X, Y])
                            {
                                for (int _Y = 0; _Y < SudokuSize; _Y++)
                                {
                                    if (_Y != Y && E == Sudoku[X, _Y])
                                    {
                                        CorrectionTable[X, Y] = false;

                                        Setted[X, Y] = true;

                                        if (!SolidFlagTable[X, _Y])
                                        {
                                            CorrectionTable[X, _Y] = false;

                                            Setted[X, _Y] = true;
                                        }

                                        break;
                                    }
                                }
                            }

                            if (!Setted[X, Y])
                            {
                                Int32 _X = X / SudokuOrder * SudokuOrder, _Y = Y / SudokuOrder * SudokuOrder;

                                for (int x = _X; x < Math.Min(_X + SudokuOrder, SudokuSize); x++)
                                {
                                    for (int y = _Y; y < Math.Min(_Y + SudokuOrder, SudokuSize); y++)
                                    {
                                        if (!(x == X && y == Y) && E == Sudoku[x, y])
                                        {
                                            CorrectionTable[X, Y] = false;

                                            Setted[X, Y] = true;

                                            if (!SolidFlagTable[x, y])
                                            {
                                                CorrectionTable[x, y] = false;

                                                Setted[x, y] = true;
                                            }

                                            break;
                                        }
                                    }
                                }
                            }

                            if (!Setted[X, Y])
                            {
                                CorrectionTable[X, Y] = true;
                            }
                        }
                        else
                        {
                            CorrectionTable[X, Y] = true;

                            Setted[X, Y] = true;
                        }
                    }
                    else
                    {
                        CorrectionTable[X, Y] = false;

                        Setted[X, Y] = true;
                    }
                }
            }
        }

        // 求解数独。

        private const Int32 _TSSRecFailureLimit = 10; // 递归失败阈值。递归求解数独的超时次数大于此值后将不再尝试求解，认为数独无解。
        private const double _TSSRecTimeout = 300; // 递归超时阈值。单次递归求解数独的耗时大于此毫秒数后将终止此次递归，并再次尝试求解。
        private DateTime _TSSRecStartDT = DateTime.Now; // 此次递归开始的时刻。

        private UInt32[,] _TSSPVT = new UInt32[CAPACITY, CAPACITY]; // 用于求解数独的可填值表。

        private void _InitTSSPVT(Int32[,] Sudoku)
        {
            //
            // 初始化用于求解数独的可填值表。Sudoku：数独。
            //

            CalcPVTOfSudoku(Sudoku, _TSSPVT);
        }

        private bool _TSSRecursion(Int32[,] Sudoku)
        {
            //
            // 使用可回溯的递归方法尝试求解一个数独，并返回是否成功求解。求解过程直接发生在输入的数独上，这意味着，不论最终能否成功求解，输入的数独都将被更改。Sudoku：数独（作为输入输出）。
            //

            if ((DateTime.Now - _TSSRecStartDT).TotalMilliseconds > _TSSRecTimeout)
            {
                return false;
            }

            //

            Int32 MinProbableCnt = SudokuSize + 1;
            Int32 MinProbableID_X = -1, MinProbableID_Y = -1;
            bool SudokuIsFull = true;

            for (int X = 0; X < SudokuSize; X++)
            {
                for (int Y = 0; Y < SudokuSize; Y++)
                {
                    if (Sudoku[X, Y] == 0)
                    {
                        Int32 Count = Com.BitOperation.GetBit1CountOfBinary(_TSSPVT[X, Y]);

                        if (Count == 0)
                        {
                            return false;
                        }
                        else
                        {
                            if (MinProbableCnt > Count)
                            {
                                MinProbableCnt = Count;
                                MinProbableID_X = X;
                                MinProbableID_Y = Y;
                                SudokuIsFull = false;
                            }

                            if (MinProbableCnt == 1)
                            {
                                break;
                            }
                        }
                    }
                }
            }

            //

            if (SudokuIsFull)
            {
                return true;
            }
            else
            {
                List<Int32> Values = Com.BitOperation.GetBit1IndexOfBinary(_TSSPVT[MinProbableID_X, MinProbableID_Y]);

                for (int i = 0; i < Values.Count; i++)
                {
                    Int32 Index = Com.Statistics.RandomInteger(Values.Count);

                    Int32 Temp = Values[i];
                    Values[i] = Values[Index];
                    Values[Index] = Temp;
                }

                for (int i = 0; i < Values.Count; i++)
                {
                    Int32 Num = Values[i];

                    Sudoku[MinProbableID_X, MinProbableID_Y] = Num;

                    UInt32[] PVT_Row = new UInt32[SudokuSize];
                    UInt32[] PVT_Column = new UInt32[SudokuSize];
                    UInt32[,] PVT_Matrix = new UInt32[SudokuOrder, SudokuOrder];

                    Int32 X = MinProbableID_X / SudokuOrder * SudokuOrder, Y = MinProbableID_Y / SudokuOrder * SudokuOrder;

                    for (int j = 0; j < SudokuSize; j++)
                    {
                        PVT_Row[j] = _TSSPVT[MinProbableID_X, j];
                        PVT_Column[j] = _TSSPVT[j, MinProbableID_Y];
                        PVT_Matrix[j % SudokuOrder, j / SudokuOrder] = _TSSPVT[X + j % SudokuOrder, Y + j / SudokuOrder];
                    }

                    for (int j = 0; j < SudokuSize; j++)
                    {
                        Com.BitOperation.RemoveBitFromBinary(ref _TSSPVT[MinProbableID_X, j], Num);
                        Com.BitOperation.RemoveBitFromBinary(ref _TSSPVT[j, MinProbableID_Y], Num);
                        Com.BitOperation.RemoveBitFromBinary(ref _TSSPVT[X + j % SudokuOrder, Y + j / SudokuOrder], Num);
                    }

                    if (_TSSRecursion(Sudoku))
                    {
                        return true;
                    }
                    else
                    {
                        Sudoku[MinProbableID_X, MinProbableID_Y] = 0;

                        for (int j = 0; j < SudokuSize; j++)
                        {
                            _TSSPVT[MinProbableID_X, j] = PVT_Row[j];
                            _TSSPVT[j, MinProbableID_Y] = PVT_Column[j];
                            _TSSPVT[X + j % SudokuOrder, Y + j / SudokuOrder] = PVT_Matrix[j % SudokuOrder, j / SudokuOrder];
                        }
                    }
                }

                return false;
            }
        }

        private bool TryToSolveASudoku(Int32[,] Sudoku)
        {
            //
            // 尝试求解一个数独，并返回是否成功求解。若成功求解，则将解覆盖输入的数独，否则将不对输入的数独做任何更改。Sudoku：数独（作为输入输出）。【注意】求解数独使用可回溯的递归方法，并特别针对 5 阶数独使用统计学方法进行优化，通过限制递归的单次耗时与超时次数，在保证成功率足够高的前提下使求解耗时的数学期望与标准差足够低，这些限制因素的取值依据 5 阶数独求解测试的大量统计数据确定。
            //

            for (int i = 0; i < _TSSRecFailureLimit; i++)
            {
                _TSSRecStartDT = DateTime.Now;

                Int32[,] SudokuCopy = GetCopyOfArray(Sudoku);

                _InitTSSPVT(SudokuCopy);

                if (_TSSRecursion(SudokuCopy))
                {
                    for (int X = 0; X < SudokuSize; X++)
                    {
                        for (int Y = 0; Y < SudokuSize; Y++)
                        {
                            Sudoku[X, Y] = SudokuCopy[X, Y];
                        }
                    }

                    return true;
                }
                else if ((DateTime.Now - _TSSRecStartDT).TotalMilliseconds <= _TSSRecTimeout)
                {
                    return false;
                }
            }

            return false;
        }

        // 命题。

        private Int32 EmptyCountForNewSudoku(Int32 SudokuOrder, Int32 DifficultyLevel)
        {
            //
            // 对于指定的阶数与难度等级，生成一个数独题目需要留空的数量。SudokuOrder：数独的阶数；DifficultyLevel：难度等级。
            //

            switch (SudokuOrder)
            {
                case 3: return (15 + 5 * DifficultyLevel);
                case 4: return (45 + 15 * DifficultyLevel);
                case 5: return (100 + 40 * DifficultyLevel);
                default: return (15 + 5 * DifficultyLevel);
            }
        }

        private void CreateSudoku()
        {
            //
            // 生成一个数独题目，将其复制到元素矩阵中，并更新 SolidFlagTable，ProbableValuesTable 与 CorrectionTable。
            //

            List<Int32> Values = new List<Int32>(SudokuSize);

            for (int i = 1; i <= SudokuSize; i++)
            {
                Values.Insert(Com.Statistics.RandomInteger(Values.Count + 1), i);
            }

            Int32[,] Sudoku = new Int32[SudokuSize, SudokuSize];

            for (int i = 0; i < SudokuSize; i++)
            {
                Int32 X = i % SudokuOrder, Y = i / SudokuOrder;

                Sudoku[X * SudokuOrder + Y, Y * SudokuOrder + X] = Values[i];
            }

            //

            if (!TryToSolveASudoku(Sudoku))
            {
                ExitGameUI();

                return;
            }

            //

            Int32 EmptyCount = EmptyCountForNewSudoku(SudokuOrder, DifficultyLevel);

            List<Int32> Zeros = new List<Int32>(EmptyCount);

            while (Zeros.Count < EmptyCount)
            {
                Int32 Z = Com.Statistics.RandomInteger(SudokuVolume);

                if (!Zeros.Contains(Z))
                {
                    Zeros.Add(Z);
                }
            }

            foreach (var V in Zeros)
            {
                Sudoku[V % SudokuSize, V / SudokuSize] = 0;
            }

            //

            for (int X = 0; X < SudokuSize; X++)
            {
                for (int Y = 0; Y < SudokuSize; Y++)
                {
                    SolidFlagTable[X, Y] = (Sudoku[X, Y] != 0);

                    ElementArray_Add(new Point(X, Y), Sudoku[X, Y]);
                }
            }

            ElementArray_UpdateProbableValuesTable();
            ElementArray_UpdateCorrectionTable();
        }

        #endregion

        #region 计时器

        private CycData CD_Timer = new CycData(); // Timer_Timer 计时周期数据。

        private void Timer_Timer_Tick(object sender, EventArgs e)
        {
            //
            // Timer_Timer。
            //

            TimerWorkOnce();
        }

        private void TimerStart()
        {
            //
            // 计时器开始。
            //

            CD_Timer.Reset();

            //

            TimerUpdateInterval();

            //

            Timer_Timer.Enabled = true;
        }

        private void TimerWorkOnce()
        {
            //
            // 计时器进行一次工作。
            //

            CD_Timer.Update();

            //

            ThisRecord.GameTime += TimeSpan.FromMilliseconds(CD_Timer.DeltaMS);

            ThisGameTime += TimeSpan.FromMilliseconds(CD_Timer.DeltaMS);
            TotalGameTime += TimeSpan.FromMilliseconds(CD_Timer.DeltaMS);

            RepaintCurBmp();

            //

            TimerUpdateInterval();
        }

        private void TimerStop()
        {
            //
            // 计时器停止。
            //

            Timer_Timer.Enabled = false;
        }

        private void TimerUpdateInterval()
        {
            //
            // 计时器更新工作周期。
            //

            if (ThisRecord.GameTime.TotalSeconds < 1)
            {
                Timer_Timer.Interval = 10;
            }
            else if (ThisRecord.GameTime.TotalSeconds < 10)
            {
                Timer_Timer.Interval = 50;
            }
            else if (ThisRecord.GameTime.TotalSeconds < 60)
            {
                Timer_Timer.Interval = 100;
            }
            else
            {
                Timer_Timer.Interval = 200;
            }
        }

        #endregion

        #region 步骤管理

        private struct Step // 操作步骤。
        {
            public Int32[,] ElementArray; // 元素矩阵。
        }

        private List<Step> StepList_Previous = new List<Step>(0); // 操作步骤列表（之前的）。
        private List<Step> StepList_Next = new List<Step>(0); // 操作步骤列表（接下来的）。

        private void StepList_Clear()
        {
            //
            // 清空操作步骤列表。
            //

            StepList_Previous.Clear();
            StepList_Next.Clear();

            PictureBox_Undo.Enabled = false;
            PictureBox_Redo.Enabled = false;
        }

        private void StepList_Append(Step S)
        {
            //
            // 追加一步操作。S：操作步骤。
            //

            StepList_Previous.Add(S);

            PictureBox_Undo.Enabled = true;

            //

            StepList_Next.Clear();

            PictureBox_Redo.Enabled = false;
        }

        private void StepList_Undo()
        {
            //
            // 撤销一步操作。
            //

            if (StepList_Previous.Count > 0)
            {
                Step Previous = StepList_Previous[StepList_Previous.Count - 1];
                Step S = new Step();

                S.ElementArray = GetCopyOfArray(ElementArray);

                StepList_Next.Add(S);

                PictureBox_Redo.Enabled = true;

                //

                List<Point> ElementIndexList_Copy = new List<Point>(ElementIndexList);

                ElementArray_Initialize();

                for (int X = 0; X < Range.Width; X++)
                {
                    for (int Y = 0; Y < Range.Height; Y++)
                    {
                        Int32 E = Previous.ElementArray[X, Y];

                        if (E != 0)
                        {
                            ElementArray_Add(new Point(X, Y), E);
                        }
                    }
                }

                //

                StepList_Previous.RemoveAt(StepList_Previous.Count - 1);

                //

                ThisRecord.StepCount += 1;

                OperatingIndex = new Point(-1, -1);
                OperatingNumber = -1;

                ElementArray_UpdateProbableValuesTable();
                ElementArray_UpdateCorrectionTable();

                ElementArray_RepresentAll();

                Judgement();
            }

            if (StepList_Previous.Count == 0)
            {
                PictureBox_Undo.Enabled = false;

                Panel_Environment.Focus();
            }
        }

        private void StepList_Redo()
        {
            //
            // 重做一步操作。
            //

            if (StepList_Next.Count > 0)
            {
                Step Next = StepList_Next[StepList_Next.Count - 1];
                Step S = new Step();

                S.ElementArray = GetCopyOfArray(ElementArray);

                StepList_Previous.Add(S);

                PictureBox_Undo.Enabled = true;

                //

                List<Point> ElementIndexList_Copy = new List<Point>(ElementIndexList);

                ElementArray_Initialize();

                for (int X = 0; X < Range.Width; X++)
                {
                    for (int Y = 0; Y < Range.Height; Y++)
                    {
                        Int32 E = Next.ElementArray[X, Y];

                        if (E != 0)
                        {
                            ElementArray_Add(new Point(X, Y), E);
                        }
                    }
                }

                //

                StepList_Next.RemoveAt(StepList_Next.Count - 1);

                //

                ThisRecord.StepCount += 1;

                OperatingIndex = new Point(-1, -1);
                OperatingNumber = -1;

                ElementArray_UpdateProbableValuesTable();
                ElementArray_UpdateCorrectionTable();

                ElementArray_RepresentAll();

                Judgement();
            }

            if (StepList_Next.Count == 0)
            {
                PictureBox_Redo.Enabled = false;

                Panel_Environment.Focus();
            }
        }

        #endregion

        #region 中断管理

        // 判定。

        private void Judgement()
        {
            //
            // 完成判定。
            //

            if (!GameIsWin)
            {
                if (ElementArray_SudokuIsCompleted())
                {
                    GameIsWin = true;

                    OperatingIndex = new Point(-1, -1);
                    OperatingNumber = -1;

                    TimerStop();

                    ElementArray_RepresentAll();

                    ThisRecord.OrderValue = (Int32)Order;
                    ThisRecord.DifficultyLevel = DifficultyLevel;
                    ThisRecord.ShowNotes = ShowNotes;

                    SaveUserData();

                    EraseLastGame();

                    StepList_Clear();

                    PictureBox_Erase.Enabled = false;

                    PictureBox_Interrupt.Enabled = false;
                }
            }

            //

            RepaintCurBmp();
        }

        // 中断。

        private enum InterruptActions { NULL = -1, StartNew, Continue, Erase, Previous, Next, Pause, Resume, Restart, Exit, CloseApp, COUNT } // 中断动作枚举。

        private void Interrupt(InterruptActions IA)
        {
            //
            // 中断。
            //

            switch (IA)
            {
                case InterruptActions.StartNew: // 开始新游戏。
                    {
                        EraseLastGame();

                        //

                        EnterGameUI();

                        //

                        CreateSudoku();

                        OperatingIndex = new Point(-1, -1);
                        OperatingNumber = -1;

                        TimerStart();

                        ElementArray_RepresentAll();

                        Judgement();
                    }
                    break;

                case InterruptActions.Continue: // 继续上次的游戏。
                    {
                        Order = (Orders)Record_Last.OrderValue;
                        DifficultyLevel = Record_Last.DifficultyLevel;
                        ShowNotes = Record_Last.ShowNotes;

                        //

                        EnterGameUI();

                        //

                        LoadGameStepInBackground();

                        //

                        RadioButton_Order3.CheckedChanged -= RadioButton_Order3_CheckedChanged;
                        RadioButton_Order4.CheckedChanged -= RadioButton_Order4_CheckedChanged;
                        RadioButton_Order5.CheckedChanged -= RadioButton_Order5_CheckedChanged;

                        switch (Order)
                        {
                            case Orders.Order3: RadioButton_Order3.Checked = true; break;
                            case Orders.Order4: RadioButton_Order4.Checked = true; break;
                            case Orders.Order5: RadioButton_Order5.Checked = true; break;
                        }

                        RadioButton_Order3.CheckedChanged += RadioButton_Order3_CheckedChanged;
                        RadioButton_Order4.CheckedChanged += RadioButton_Order4_CheckedChanged;
                        RadioButton_Order5.CheckedChanged += RadioButton_Order5_CheckedChanged;

                        //

                        CheckBox_ShowNotes.CheckedChanged -= CheckBox_ShowNotes_CheckedChanged;

                        CheckBox_ShowNotes.Checked = ShowNotes;

                        CheckBox_ShowNotes.CheckedChanged += CheckBox_ShowNotes_CheckedChanged;

                        Label_ShowNotes_Info.Enabled = ShowNotes;
                    }
                    break;

                case InterruptActions.Erase: // 擦除。
                    {
                        if (Timer_Timer.Enabled)
                        {
                            ElementArray_BasicLogicalOperation(0, true);
                        }
                    }
                    break;

                case InterruptActions.Previous: // 撤销一步。
                    {
                        StepList_Undo();
                    }
                    break;

                case InterruptActions.Next: // 重做一步。
                    {
                        StepList_Redo();
                    }
                    break;

                case InterruptActions.Pause: // 暂停。
                    {
                        OperatingIndex = new Point(-1, -1);
                        OperatingNumber = -1;

                        TimerStop();

                        ElementArray_RepresentAll();

                        RepaintCurBmp();

                        PictureBox_Interrupt.Image = Properties.Resources.Resume;

                        //

                        PictureBox_Erase.Enabled = false;

                        PictureBox_Undo.Enabled = false;
                        PictureBox_Redo.Enabled = false;
                    }
                    break;

                case InterruptActions.Resume: // 恢复。
                    {
                        TimerStart();

                        ElementArray_RepresentAll();

                        Judgement();

                        PictureBox_Interrupt.Image = Properties.Resources.Pause;

                        //

                        PictureBox_Erase.Enabled = true;

                        if (StepList_Previous.Count > 0)
                        {
                            PictureBox_Undo.Enabled = true;
                        }

                        if (StepList_Next.Count > 0)
                        {
                            PictureBox_Redo.Enabled = true;
                        }
                    }
                    break;

                case InterruptActions.Restart: // 重新开始。
                    {
                        EraseLastGame();

                        //

                        ThisRecord.OrderValue = (Int32)Order;
                        ThisRecord.DifficultyLevel = DifficultyLevel;
                        ThisRecord.ShowNotes = ShowNotes;

                        SaveUserData();

                        //

                        ThisGameTime = TimeSpan.Zero;

                        GameIsWin = false;

                        ThisRecord = new Record();

                        ElementArray_Initialize();

                        CreateSudoku();

                        OperatingIndex = new Point(-1, -1);
                        OperatingNumber = -1;

                        StepList_Clear();

                        RepaintCurBmp();

                        TimerStart();

                        ElementArray_RepresentAll();

                        PictureBox_Erase.Enabled = true;

                        PictureBox_Interrupt.Enabled = true;
                        PictureBox_Interrupt.Image = Properties.Resources.Pause;

                        Judgement();

                        //

                        Panel_Environment.Focus();
                    }
                    break;

                case InterruptActions.Exit: // 退出游戏。
                    {
                        ThisRecord.OrderValue = (Int32)Order;
                        ThisRecord.DifficultyLevel = DifficultyLevel;
                        ThisRecord.ShowNotes = ShowNotes;

                        SaveUserData();

                        //

                        Panel_Environment.Focus();

                        //

                        TimerStop();

                        if (!GameIsWin)
                        {
                            if (ThisRecord.GameTime.TotalMilliseconds > 0 && ThisRecord.StepCount > 0)
                            {
                                SaveGameStepInBackground();
                            }
                            else
                            {
                                ThisRecord.GameTime = TimeSpan.Zero;
                                ThisRecord.StepCount = 0;

                                //

                                ExitGameUI();
                            }
                        }
                        else
                        {
                            ExitGameUI();
                        }
                    }
                    break;

                case InterruptActions.CloseApp: // 关闭程序。
                    {
                        ThisRecord.OrderValue = (Int32)Order;
                        ThisRecord.DifficultyLevel = DifficultyLevel;
                        ThisRecord.ShowNotes = ShowNotes;

                        SaveUserData();

                        //

                        TimerStop();

                        if (!GameIsWin && (ThisRecord.GameTime.TotalMilliseconds > 0 && ThisRecord.StepCount > 0))
                        {
                            SaveGameStepInForeground();
                        }
                    }
                    break;
            }
        }

        // 中断按钮。

        private void Label_StartNewGame_Click(object sender, EventArgs e)
        {
            //
            // 单击 Label_StartNewGame。
            //

            Interrupt(InterruptActions.StartNew);
        }

        private void Label_ContinueLastGame_Click(object sender, EventArgs e)
        {
            //
            // 单击 Label_ContinueLastGame。
            //

            Interrupt(InterruptActions.Continue);
        }

        private void PictureBox_Erase_MouseEnter(object sender, EventArgs e)
        {
            //
            // 鼠标进入 PictureBox_Erase。
            //

            ToolTip_InterruptPrompt.RemoveAll();

            if (!GameIsWin)
            {
                ToolTip_InterruptPrompt.SetToolTip(PictureBox_Erase, "擦除");
            }
        }

        private void PictureBox_Erase_Click(object sender, EventArgs e)
        {
            //
            // 单击 PictureBox_Erase。
            //

            Interrupt(InterruptActions.Erase);
        }

        private void PictureBox_Erase_Paint(object sender, PaintEventArgs e)
        {
            //
            // PictureBox_Erase 绘图。
            //

            if (_OperatingNumber == 0)
            {
                e.Graphics.FillRectangle(new SolidBrush(Me.RecommendColors.Border_INC.ToColor()), new Rectangle(new Point(0, PictureBox_Erase.Height - 4), new Size(PictureBox_Erase.Width, 4)));
            }
        }

        private void PictureBox_Undo_MouseEnter(object sender, EventArgs e)
        {
            //
            // 鼠标进入 PictureBox_Undo。
            //

            ToolTip_InterruptPrompt.RemoveAll();

            if (StepList_Previous.Count > 0)
            {
                ToolTip_InterruptPrompt.SetToolTip(PictureBox_Undo, "撤销一步");
            }
        }

        private void PictureBox_Undo_Click(object sender, EventArgs e)
        {
            //
            // 单击 PictureBox_Undo。
            //

            Interrupt(InterruptActions.Previous);
        }

        private void PictureBox_Redo_MouseEnter(object sender, EventArgs e)
        {
            //
            // 鼠标进入 PictureBox_Redo。
            //

            ToolTip_InterruptPrompt.RemoveAll();

            if (StepList_Next.Count > 0)
            {
                ToolTip_InterruptPrompt.SetToolTip(PictureBox_Redo, "重做一步");
            }
        }

        private void PictureBox_Redo_Click(object sender, EventArgs e)
        {
            //
            // 单击 PictureBox_Redo。
            //

            Interrupt(InterruptActions.Next);
        }

        private void PictureBox_Interrupt_MouseEnter(object sender, EventArgs e)
        {
            //
            // 鼠标进入 PictureBox_Interrupt。
            //

            ToolTip_InterruptPrompt.RemoveAll();

            if (!GameIsWin)
            {
                if (Timer_Timer.Enabled)
                {
                    ToolTip_InterruptPrompt.SetToolTip(PictureBox_Interrupt, "暂停");
                }
                else
                {
                    ToolTip_InterruptPrompt.SetToolTip(PictureBox_Interrupt, "恢复");
                }
            }
        }

        private void PictureBox_Interrupt_Click(object sender, EventArgs e)
        {
            //
            // 单击 Button_Interrupt。
            //

            if (!GameIsWin)
            {
                if (Timer_Timer.Enabled)
                {
                    Interrupt(InterruptActions.Pause);
                }
                else
                {
                    Interrupt(InterruptActions.Resume);
                }
            }
        }

        private void PictureBox_Restart_MouseEnter(object sender, EventArgs e)
        {
            //
            // 鼠标进入 PictureBox_Restart。
            //

            ToolTip_InterruptPrompt.RemoveAll();

            ToolTip_InterruptPrompt.SetToolTip(PictureBox_Restart, "重新开始");
        }

        private void PictureBox_Restart_Click(object sender, EventArgs e)
        {
            //
            // 单击 PictureBox_Restart。
            //

            Interrupt(InterruptActions.Restart);
        }

        private void PictureBox_ExitGame_MouseEnter(object sender, EventArgs e)
        {
            //
            // 鼠标进入 PictureBox_ExitGame。
            //

            ToolTip_InterruptPrompt.RemoveAll();

            ToolTip_InterruptPrompt.SetToolTip(PictureBox_ExitGame, (!GameIsWin && ThisRecord.StepCount > 0 ? "保存并退出" : "退出"));
        }

        private void PictureBox_ExitGame_Click(object sender, EventArgs e)
        {
            //
            // 单击 PictureBox_ExitGame。
            //

            Interrupt(InterruptActions.Exit);
        }

        #endregion

        #region UI 切换

        private bool GameUINow = false; // 当前 UI 是否为游戏 UI。

        private void EnterGameUI()
        {
            //
            // 进入游戏 UI。
            //

            GameUINow = true;

            //

            ElementArray_Initialize();

            //

            GameIsWin = false;

            ThisGameTime = TimeSpan.Zero;

            ThisRecord = new Record();

            StepList_Clear();

            PictureBox_Erase.Enabled = true;

            PictureBox_Interrupt.Enabled = true;
            PictureBox_Interrupt.Image = Properties.Resources.Pause;

            //

            Panel_FunctionArea.Visible = false;
            Panel_GameUI.Visible = true;

            //

            Panel_Environment.Focus();

            //

            double RW = Range.Width, RH = Range.Height;

            if ((Screen.PrimaryScreen.WorkingArea.Width - (Me.CaptionBarHeight + Panel_Current.Height)) * Range.Width >= Screen.PrimaryScreen.WorkingArea.Width * (Range.Height + CandidateNumberAreaDist + CandidateNumberAreaSize))
            {
                RH = Range.Height + CandidateNumberAreaDist + CandidateNumberAreaSize;
            }
            else
            {
                RW = Range.Width + CandidateNumberAreaDist + CandidateNumberAreaSize;
            }

            while (ElementSize * RW > Screen.PrimaryScreen.WorkingArea.Width || Me.CaptionBarHeight + Panel_Current.Height + ElementSize * RH > Screen.PrimaryScreen.WorkingArea.Height)
            {
                ElementSize = ElementSize * 9 / 10;
            }

            Me.ClientSize = new Size((Int32)(ElementSize * RW), (Int32)(Panel_Current.Height + ElementSize * RH));
            Me.Location = new Point((Screen.PrimaryScreen.WorkingArea.Width - Me.Width) / 2, (Screen.PrimaryScreen.WorkingArea.Height - Me.Height) / 2);

            ElementSize = (Int32)Math.Max(1, Math.Min(Panel_Environment.Width / RW, Panel_Environment.Height / RH));

            EAryBmpRect.Size = new Size((Int32)Math.Max(1, ElementSize * RW), (Int32)Math.Max(1, ElementSize * RH));
            EAryBmpRect.Location = new Point((Panel_Environment.Width - EAryBmpRect.Width) / 2, (Panel_Environment.Height - EAryBmpRect.Height) / 2);

            if (RW > Range.Width)
            {
                CandidateNumberArea = new Rectangle(new Point((Int32)(EAryBmpRect.X + ElementSize * (Range.Width + CandidateNumberAreaDist)), EAryBmpRect.Y), new Size((Int32)Math.Max(1, ElementSize * CandidateNumberAreaSize), Math.Max(1, ElementSize * Range.Height)));
            }
            else
            {
                CandidateNumberArea = new Rectangle(new Point(EAryBmpRect.X, (Int32)(EAryBmpRect.Y + ElementSize * (Range.Height + CandidateNumberAreaDist))), new Size(Math.Max(1, ElementSize * Range.Width), (Int32)Math.Max(1, ElementSize * CandidateNumberAreaSize)));
            }

            //

            RepaintCurBmp();

            ElementArray_RepresentAll();
        }

        private void ExitGameUI()
        {
            //
            // 退出游戏 UI。
            //

            GameUINow = false;

            //

            Panel_FunctionArea.Visible = true;
            Panel_GameUI.Visible = false;

            //

            ElementArray_Initialize();

            //

            Me.ClientSize = FormClientInitialSize;
            Me.Location = new Point((Screen.PrimaryScreen.WorkingArea.Width - Me.Width) / 2, (Screen.PrimaryScreen.WorkingArea.Height - Me.Height) / 2);

            //

            FunctionAreaTab = FunctionAreaTabs.Start;
        }

        #endregion

        #region 游戏 UI 交互

        private Point GameUIPointedIndex = new Point(-1, -1); // 鼠标指向的索引。
        private Int32 GameUIPointedNumber = -1; // 鼠标指向的候选操作数。

        private void Panel_Environment_MouseMove(object sender, MouseEventArgs e)
        {
            //
            // 鼠标经过 Panel_Environment。
            //

            Panel_Environment.Focus();

            //

            if (Timer_Timer.Enabled)
            {
                if (OperationMode == OperationModes.Mouse || OperationMode == OperationModes.Touch)
                {
                    Point CurPtOfCtrl = Com.Geometry.GetCursorPositionOfControl(Panel_Environment);

                    //

                    Point A = ElementArray_GetIndex(CurPtOfCtrl);

                    if (GameUIPointedIndex != A)
                    {
                        Point LastPointedIndex = GameUIPointedIndex;

                        if (A != OperatingIndex)
                        {
                            GameUIPointedIndex = A;
                        }
                        else
                        {
                            GameUIPointedIndex = new Point(-1, -1);
                        }

                        ElementArray_PresentAt(LastPointedIndex);
                        ElementArray_PresentAt(GameUIPointedIndex);
                    }

                    //

                    Int32 Number = ElementArray_GetCandidateNumberAt(CurPtOfCtrl);

                    if (GameUIPointedNumber != Number)
                    {
                        Int32 LastPointedNumber = GameUIPointedNumber;

                        if (Number != OperatingNumber)
                        {
                            GameUIPointedNumber = Number;
                        }
                        else
                        {
                            GameUIPointedNumber = -1;
                        }

                        ElementArray_UpdateCandidateNumberArea(LastPointedNumber, true);
                        ElementArray_UpdateCandidateNumberArea(GameUIPointedNumber, true);
                    }
                }
            }
        }

        private void Panel_Environment_MouseLeave(object sender, EventArgs e)
        {
            //
            // 鼠标离开 Panel_Environment。
            //

            if (Timer_Timer.Enabled)
            {
                if (OperationMode == OperationModes.Mouse || OperationMode == OperationModes.Touch)
                {
                    Point LastPointedIndex = GameUIPointedIndex;
                    GameUIPointedIndex = new Point(-1, -1);

                    ElementArray_PresentAt(LastPointedIndex);
                    ElementArray_PresentAt(GameUIPointedIndex);

                    //

                    Int32 LastPointedNumber = GameUIPointedNumber;
                    GameUIPointedNumber = -1;

                    ElementArray_UpdateCandidateNumberArea(LastPointedNumber, true);
                    ElementArray_UpdateCandidateNumberArea(GameUIPointedNumber, true);
                }
            }
        }

        private void Panel_Environment_MouseDown(object sender, MouseEventArgs e)
        {
            //
            // 鼠标按下 Panel_Environment。
            //

            if (Timer_Timer.Enabled && e.Button == MouseButtons.Left)
            {
                GameUIPointedIndex = new Point(-1, -1);
                GameUIPointedNumber = -1;

                //

                Point CurPtOfCtrl = Com.Geometry.GetCursorPositionOfControl(Panel_Environment);

                ElementArray_MouseDownOperation(CurPtOfCtrl);
            }
        }

        private void Panel_Environment_MouseUp(object sender, MouseEventArgs e)
        {
            //
            // 鼠标释放 Panel_Environment。
            //

            if (Timer_Timer.Enabled && e.Button == MouseButtons.Left)
            {
                GameUIPointedIndex = new Point(-1, -1);
                GameUIPointedNumber = -1;

                //

                Point CurPtOfCtrl = Com.Geometry.GetCursorPositionOfControl(Panel_Environment);

                ElementArray_MouseUpOperation(CurPtOfCtrl);
            }
        }

        private void Panel_Environment_KeyDown(object sender, KeyEventArgs e)
        {
            //
            // 在 Panel_Environment 按下键。
            //

            if (AlwaysEnableKeyboard || OperationMode == OperationModes.Keyboard)
            {
                if (!GameIsWin)
                {
                    if (e.KeyCode == Keys.Space)
                    {
                        if (Timer_Timer.Enabled)
                        {
                            Interrupt(InterruptActions.Pause);
                        }
                        else
                        {
                            Interrupt(InterruptActions.Resume);
                        }
                    }
                    else
                    {
                        if (Timer_Timer.Enabled)
                        {
                            switch (e.KeyCode)
                            {
                                case Keys.PageUp: Interrupt(InterruptActions.Previous); break;
                                case Keys.PageDown: Interrupt(InterruptActions.Next); break;

                                default: ElementArray_KeyOperation(e.KeyCode); break;
                            }
                        }
                    }
                }

                switch (e.KeyCode)
                {
                    case Keys.Home: Interrupt(InterruptActions.Restart); break;
                    case Keys.End:
                    case Keys.Escape: Interrupt(InterruptActions.Exit); break;
                }
            }
        }

        #endregion

        #region 鼠标滚轮功能

        private void Form_Main_MouseWheel(object sender, MouseEventArgs e)
        {
            //
            // 鼠标滚轮在 this 滚动。
            //

            if (Panel_FunctionAreaOptionsBar.Visible && Panel_FunctionAreaOptionsBar.Enabled && Com.Geometry.CursorIsInControl(Panel_FunctionAreaOptionsBar))
            {
                if (e.Delta < 0 && (Int32)FunctionAreaTab < (Int32)FunctionAreaTabs.COUNT - 1)
                {
                    FunctionAreaTab++;
                }
                else if (e.Delta > 0 && (Int32)FunctionAreaTab > 0)
                {
                    FunctionAreaTab--;
                }
            }
        }

        private void Panel_Environment_MouseWheel(object sender, MouseEventArgs e)
        {
            //
            // 鼠标滚轮在 Panel_Environment 滚动。
            //

            double RW = Range.Width, RH = Range.Height;

            if (Panel_Environment.Height * Range.Width >= Panel_Environment.Width * (Range.Height + CandidateNumberAreaDist + CandidateNumberAreaSize))
            {
                RH = Range.Height + CandidateNumberAreaDist + CandidateNumberAreaSize;
            }
            else
            {
                RW = Range.Width + CandidateNumberAreaDist + CandidateNumberAreaSize;
            }

            if (RW <= RH)
            {
                if (e.Delta > 0)
                {
                    Me.Location = new Point(Me.X - Me.Width / 20, (Int32)(Me.Y - Me.Width / 20 * RH / RW));
                    Me.Size = new Size(Me.Width + Me.Width / 20 * 2, (Int32)(Me.Height + Me.Width / 20 * RH / RW * 2));
                }
                else if (e.Delta < 0)
                {
                    Me.Location = new Point(Me.X + Me.Width / 20, (Int32)(Me.Y + Me.Width / 20 * RH / RW));
                    Me.Size = new Size(Me.Width - Me.Width / 20 * 2, (Int32)(Me.Height - Me.Width / 20 * RH / RW * 2));
                }
            }
            else
            {
                if (e.Delta > 0)
                {
                    Me.Location = new Point((Int32)(Me.X - Me.Height / 20 * RW / RH), Me.Y - Me.Height / 20);
                    Me.Size = new Size((Int32)(Me.Width + Me.Height / 20 * RW / RH * 2), Me.Height + Me.Height / 20 * 2);
                }
                else if (e.Delta < 0)
                {
                    Me.Location = new Point((Int32)(Me.X + Me.Height / 20 * RW / RH), Me.Y + Me.Height / 20);
                    Me.Size = new Size((Int32)(Me.Width - Me.Height / 20 * RW / RH * 2), Me.Height - Me.Height / 20 * 2);
                }
            }

            Me.Location = new Point(Math.Max(0, Math.Min(Screen.PrimaryScreen.WorkingArea.Width - Me.Width, Me.X)), Math.Max(0, Math.Min(Screen.PrimaryScreen.WorkingArea.Height - Me.Height, Me.Y)));
        }

        #endregion

        #region 计分栏

        private Bitmap CurBmp; // 计分栏位图。

        private Graphics CurBmpGrap; // 计分栏位图绘图。

        private void UpdateCurBmp()
        {
            //
            // 更新计分栏位图。
            //

            if (CurBmp != null)
            {
                CurBmp.Dispose();
            }

            CurBmp = new Bitmap(Math.Max(1, Panel_Current.Width), Math.Max(1, Panel_Current.Height));

            CurBmpGrap = Graphics.FromImage(CurBmp);

            if (AntiAlias)
            {
                CurBmpGrap.SmoothingMode = SmoothingMode.AntiAlias;
                CurBmpGrap.TextRenderingHint = TextRenderingHint.AntiAlias;
            }

            CurBmpGrap.Clear(GameUIBackColor_DEC);

            //

            Rectangle Rect_Total = new Rectangle(new Point(0, 0), new Size(Math.Max(1, Panel_Current.Width), Math.Max(1, Panel_Current.Height)));
            Rectangle Rect_Current = new Rectangle(Rect_Total.Location, new Size((Int32)Math.Max(2, Math.Min(1, (double)(ShowNotes ? CorrectNumberCount : SudokuVolume - SolidNumberCount - GetZeroCountOfArray(ElementArray, Range)) / (SudokuVolume - SolidNumberCount)) * Rect_Total.Width), Rect_Total.Height));

            Color RectCr_Total = Me.RecommendColors.Background.ToColor(), RectCr_Current = Me.RecommendColors.Border.ToColor();

            GraphicsPath Path_Total = new GraphicsPath();
            Path_Total.AddRectangle(Rect_Total);
            PathGradientBrush PGB_Total = new PathGradientBrush(Path_Total)
            {
                CenterColor = RectCr_Total,
                SurroundColors = new Color[] { Com.ColorManipulation.ShiftLightnessByHSL(RectCr_Total, 0.3) },
                FocusScales = new PointF(1F, 0F)
            };
            CurBmpGrap.FillPath(PGB_Total, Path_Total);
            Path_Total.Dispose();
            PGB_Total.Dispose();

            GraphicsPath Path_Current = new GraphicsPath();
            Path_Current.AddRectangle(Rect_Current);
            PathGradientBrush PGB_Current = new PathGradientBrush(Path_Current)
            {
                CenterColor = RectCr_Current,
                SurroundColors = new Color[] { Com.ColorManipulation.ShiftLightnessByHSL(RectCr_Current, 0.3) },
                FocusScales = new PointF(1F, 0F)
            };
            CurBmpGrap.FillPath(PGB_Current, Path_Current);
            Path_Current.Dispose();
            PGB_Current.Dispose();

            //

            SizeF RegionSize_L = new SizeF(), RegionSize_R = new SizeF();
            RectangleF RegionRect = new RectangleF();

            string StringText_Score = Com.Text.GetTimeStringFromTimeSpan(ThisRecord.GameTime);
            Color StringColor_Score = Me.RecommendColors.Text_INC.ToColor();
            Font StringFont_Score = new Font("微软雅黑", 24F, FontStyle.Regular, GraphicsUnit.Point, 134);
            RectangleF StringRect_Score = new RectangleF();
            StringRect_Score.Size = CurBmpGrap.MeasureString(StringText_Score, StringFont_Score);

            string StringText_Complete = (ShowNotes ? "已完成: " : "已填入: "), StringText_Complete_Val = Math.Max(0, (ShowNotes ? CorrectNumberCount : SudokuVolume - SolidNumberCount - GetZeroCountOfArray(ElementArray, Range))) + " / " + Math.Max(0, SudokuVolume - SolidNumberCount);
            Color StringColor_Complete = Me.RecommendColors.Text.ToColor(), StringColor_Complete_Val = Me.RecommendColors.Text_INC.ToColor();
            Font StringFont_Complete = new Font("微软雅黑", 12F, FontStyle.Regular, GraphicsUnit.Point, 134), StringFont_Complete_Val = new Font("微软雅黑", 12F, FontStyle.Bold, GraphicsUnit.Point, 134);
            RectangleF StringRect_Complete = new RectangleF(), StringRect_Complete_Val = new RectangleF();
            StringRect_Complete.Size = CurBmpGrap.MeasureString(StringText_Complete, StringFont_Complete);
            StringRect_Complete_Val.Size = CurBmpGrap.MeasureString(StringText_Complete_Val, StringFont_Complete_Val);

            string StringText_StepCount = "操作次数: ", StringText_StepCount_Val = Math.Max(0, ThisRecord.StepCount).ToString();
            Color StringColor_StepCount = Me.RecommendColors.Text.ToColor(), StringColor_StepCount_Val = Me.RecommendColors.Text_INC.ToColor();
            Font StringFont_StepCount = new Font("微软雅黑", 12F, FontStyle.Regular, GraphicsUnit.Point, 134), StringFont_StepCount_Val = new Font("微软雅黑", 12F, FontStyle.Bold, GraphicsUnit.Point, 134);
            RectangleF StringRect_StepCount = new RectangleF(), StringRect_StepCount_Val = new RectangleF();
            StringRect_StepCount.Size = CurBmpGrap.MeasureString(StringText_StepCount, StringFont_StepCount);
            StringRect_StepCount_Val.Size = CurBmpGrap.MeasureString(StringText_StepCount_Val, StringFont_StepCount_Val);

            RegionSize_L = StringRect_Score.Size;
            RegionSize_R = new SizeF(Math.Max(StringRect_Complete.Width + StringRect_Complete_Val.Width, StringRect_StepCount.Width + StringRect_StepCount_Val.Width), 0);

            RegionRect.Size = new SizeF(Math.Max(RegionSize_L.Width + RegionSize_R.Width, Math.Min(EAryBmpRect.Width, Panel_Interrupt.Left - EAryBmpRect.X)), Panel_Current.Height);
            RegionRect.Location = new PointF(Math.Max(0, Math.Min(EAryBmpRect.X + (EAryBmpRect.Width - RegionRect.Width) / 2, Panel_Interrupt.Left - RegionRect.Width)), 0);

            StringRect_Score.Location = new PointF(RegionRect.X, (RegionRect.Height - StringRect_Score.Height) / 2);

            Com.Painting2D.PaintTextWithShadow(CurBmp, StringText_Score, StringFont_Score, StringColor_Score, StringColor_Score, StringRect_Score.Location, 0.05F, AntiAlias);

            StringRect_Complete_Val.Location = new PointF(RegionRect.Right - StringRect_Complete_Val.Width, (RegionRect.Height / 2 - StringRect_Complete_Val.Height) / 2);
            StringRect_Complete.Location = new PointF(StringRect_Complete_Val.X - StringRect_Complete.Width, (RegionRect.Height / 2 - StringRect_Complete.Height) / 2);

            Com.Painting2D.PaintTextWithShadow(CurBmp, StringText_Complete, StringFont_Complete, StringColor_Complete, StringColor_Complete, StringRect_Complete.Location, 0.1F, AntiAlias);
            Com.Painting2D.PaintTextWithShadow(CurBmp, StringText_Complete_Val, StringFont_Complete_Val, StringColor_Complete_Val, StringColor_Complete_Val, StringRect_Complete_Val.Location, 0.1F, AntiAlias);

            StringRect_StepCount_Val.Location = new PointF(RegionRect.Right - StringRect_StepCount_Val.Width, RegionRect.Height / 2 + (RegionRect.Height / 2 - StringRect_StepCount_Val.Height) / 2);
            StringRect_StepCount.Location = new PointF(StringRect_StepCount_Val.X - StringRect_StepCount.Width, RegionRect.Height / 2 + (RegionRect.Height / 2 - StringRect_StepCount.Height) / 2);

            Com.Painting2D.PaintTextWithShadow(CurBmp, StringText_StepCount, StringFont_StepCount, StringColor_StepCount, StringColor_StepCount, StringRect_StepCount.Location, 0.1F, AntiAlias);
            Com.Painting2D.PaintTextWithShadow(CurBmp, StringText_StepCount_Val, StringFont_StepCount_Val, StringColor_StepCount_Val, StringColor_StepCount_Val, StringRect_StepCount_Val.Location, 0.1F, AntiAlias);
        }

        private void RepaintCurBmp()
        {
            //
            // 更新并重绘计分栏位图。
            //

            UpdateCurBmp();

            if (CurBmp != null)
            {
                Panel_Current.CreateGraphics().DrawImage(CurBmp, new Point(0, 0));

                foreach (var V in Panel_Current.Controls)
                {
                    ((Control)V).Refresh();
                }
            }
        }

        private void Panel_Current_Paint(object sender, PaintEventArgs e)
        {
            //
            // Panel_Current 绘图。
            //

            UpdateCurBmp();

            if (CurBmp != null)
            {
                e.Graphics.DrawImage(CurBmp, new Point(0, 0));
            }
        }

        #endregion

        #region 功能区

        private enum FunctionAreaTabs { NULL = -1, Start, Record, Options, About, COUNT } // 功能区选项卡枚举。

        private FunctionAreaTabs _FunctionAreaTab = FunctionAreaTabs.NULL; // 当前打开的功能区选项卡。
        private FunctionAreaTabs FunctionAreaTab
        {
            get
            {
                return _FunctionAreaTab;
            }

            set
            {
                _FunctionAreaTab = value;

                Color TabBtnCr_Fr_Seld = Me.RecommendColors.Main_INC.ToColor(), TabBtnCr_Fr_Uns = Color.White;
                Color TabBtnCr_Bk_Seld = Color.Transparent, TabBtnCr_Bk_Uns = Color.Transparent;
                Font TabBtnFt_Seld = new Font("微软雅黑", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 134), TabBtnFt_Uns = new Font("微软雅黑", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 134);

                Label_Tab_Start.ForeColor = (_FunctionAreaTab == FunctionAreaTabs.Start ? TabBtnCr_Fr_Seld : TabBtnCr_Fr_Uns);
                Label_Tab_Start.BackColor = (_FunctionAreaTab == FunctionAreaTabs.Start ? TabBtnCr_Bk_Seld : TabBtnCr_Bk_Uns);
                Label_Tab_Start.Font = (_FunctionAreaTab == FunctionAreaTabs.Start ? TabBtnFt_Seld : TabBtnFt_Uns);

                Label_Tab_Record.ForeColor = (_FunctionAreaTab == FunctionAreaTabs.Record ? TabBtnCr_Fr_Seld : TabBtnCr_Fr_Uns);
                Label_Tab_Record.BackColor = (_FunctionAreaTab == FunctionAreaTabs.Record ? TabBtnCr_Bk_Seld : TabBtnCr_Bk_Uns);
                Label_Tab_Record.Font = (_FunctionAreaTab == FunctionAreaTabs.Record ? TabBtnFt_Seld : TabBtnFt_Uns);

                Label_Tab_Options.ForeColor = (_FunctionAreaTab == FunctionAreaTabs.Options ? TabBtnCr_Fr_Seld : TabBtnCr_Fr_Uns);
                Label_Tab_Options.BackColor = (_FunctionAreaTab == FunctionAreaTabs.Options ? TabBtnCr_Bk_Seld : TabBtnCr_Bk_Uns);
                Label_Tab_Options.Font = (_FunctionAreaTab == FunctionAreaTabs.Options ? TabBtnFt_Seld : TabBtnFt_Uns);

                Label_Tab_About.ForeColor = (_FunctionAreaTab == FunctionAreaTabs.About ? TabBtnCr_Fr_Seld : TabBtnCr_Fr_Uns);
                Label_Tab_About.BackColor = (_FunctionAreaTab == FunctionAreaTabs.About ? TabBtnCr_Bk_Seld : TabBtnCr_Bk_Uns);
                Label_Tab_About.Font = (_FunctionAreaTab == FunctionAreaTabs.About ? TabBtnFt_Seld : TabBtnFt_Uns);

                switch (_FunctionAreaTab)
                {
                    case FunctionAreaTabs.Start:
                        {
                            if (ElementIndexList_Last.Count > 0)
                            {
                                Label_ContinueLastGame.Visible = true;

                                Label_ContinueLastGame.Focus();
                            }
                            else
                            {
                                Label_ContinueLastGame.Visible = false;

                                Label_StartNewGame.Focus();
                            }
                        }
                        break;

                    case FunctionAreaTabs.Record:
                        {
                            if (BestRecord.GameTime.TotalMilliseconds == 0 || BestRecord.StepCount == 0)
                            {
                                Label_ThisRecordVal_GameTime.Text = "无记录";
                                Label_ThisRecordVal_StepCount.Text = "操作次数: 无";
                                Label_BestRecordVal_GameTime.Text = "无记录";
                                Label_BestRecordVal_StepCount.Text = "操作次数: 无";
                            }
                            else
                            {
                                Record ThRec = new Record();

                                if (ThisRecord.OrderValue == (Int32)Order && ThisRecord.DifficultyLevel == DifficultyLevel)
                                {
                                    ThRec = ThisRecord;
                                }

                                Label_ThisRecordVal_GameTime.Text = Com.Text.GetTimeStringFromTimeSpan(ThRec.GameTime);
                                Label_ThisRecordVal_StepCount.Text = "操作次数: " + ThRec.StepCount;
                                Label_BestRecordVal_GameTime.Text = Com.Text.GetTimeStringFromTimeSpan(BestRecord.GameTime);
                                Label_BestRecordVal_StepCount.Text = "操作次数: " + BestRecord.StepCount;
                            }

                            Label_ThisTimeVal.Text = Com.Text.GetTimeStringFromTimeSpan(ThisGameTime);
                            Label_TotalTimeVal.Text = Com.Text.GetTimeStringFromTimeSpan(TotalGameTime);
                        }
                        break;

                    case FunctionAreaTabs.Options:
                        {
                            ResetCleanStepsControl();
                        }
                        break;

                    case FunctionAreaTabs.About:
                        {

                        }
                        break;
                }

                Timer_EnterPrompt.Enabled = (_FunctionAreaTab == FunctionAreaTabs.Start);

                if (Panel_FunctionAreaTab.AutoScroll)
                {
                    // Panel 的 AutoScroll 功能似乎存在 bug，下面的代码可以规避某些显示问题

                    Panel_FunctionAreaTab.AutoScroll = false;

                    foreach (var V in Panel_FunctionAreaTab.Controls)
                    {
                        if (V is Panel)
                        {
                            Panel Pnl = V as Panel;

                            Pnl.Location = new Point(0, 0);
                        }
                    }

                    Panel_FunctionAreaTab.AutoScroll = true;
                }

                Panel_Tab_Start.Visible = (_FunctionAreaTab == FunctionAreaTabs.Start);
                Panel_Tab_Record.Visible = (_FunctionAreaTab == FunctionAreaTabs.Record);
                Panel_Tab_Options.Visible = (_FunctionAreaTab == FunctionAreaTabs.Options);
                Panel_Tab_About.Visible = (_FunctionAreaTab == FunctionAreaTabs.About);
            }
        }

        private void Label_Tab_MouseEnter(object sender, EventArgs e)
        {
            //
            // 鼠标进入 Label_Tab。
            //

            Panel_FunctionAreaOptionsBar.Refresh();
        }

        private void Label_Tab_MouseLeave(object sender, EventArgs e)
        {
            //
            // 鼠标离开 Label_Tab。
            //

            Panel_FunctionAreaOptionsBar.Refresh();
        }

        private void Label_Tab_Start_MouseDown(object sender, MouseEventArgs e)
        {
            //
            // 鼠标按下 Label_Tab_Start。
            //

            if (e.Button == MouseButtons.Left)
            {
                if (FunctionAreaTab != FunctionAreaTabs.Start)
                {
                    FunctionAreaTab = FunctionAreaTabs.Start;
                }
            }
        }

        private void Label_Tab_Record_MouseDown(object sender, MouseEventArgs e)
        {
            //
            // 鼠标按下 Label_Tab_Record。
            //

            if (e.Button == MouseButtons.Left)
            {
                if (FunctionAreaTab != FunctionAreaTabs.Record)
                {
                    FunctionAreaTab = FunctionAreaTabs.Record;
                }
            }
        }

        private void Label_Tab_Options_MouseDown(object sender, MouseEventArgs e)
        {
            //
            // 鼠标按下 Label_Tab_Options。
            //

            if (e.Button == MouseButtons.Left)
            {
                if (FunctionAreaTab != FunctionAreaTabs.Options)
                {
                    FunctionAreaTab = FunctionAreaTabs.Options;
                }
            }
        }

        private void Label_Tab_About_MouseDown(object sender, MouseEventArgs e)
        {
            //
            // 鼠标按下 Label_Tab_About。
            //

            if (e.Button == MouseButtons.Left)
            {
                if (FunctionAreaTab != FunctionAreaTabs.About)
                {
                    FunctionAreaTab = FunctionAreaTabs.About;
                }
            }
        }

        #endregion

        #region "开始"区域

        private const Int32 EnterGameButtonHeight_Min = 30, EnterGameButtonHeight_Max = 50; // 进入游戏按钮高度的取值范围。

        private Color EnterGameBackColor_INC = Color.Empty; // Panel_EnterGameSelection 绘图使用的颜色（深色）。
        private Color EnterGameBackColor_DEC => Panel_FunctionArea.BackColor; // Panel_EnterGameSelection 绘图使用的颜色（浅色）。

        private void Panel_EnterGameSelection_Paint(object sender, PaintEventArgs e)
        {
            //
            // Panel_EnterGameSelection 绘图。
            //

            Rectangle Rect_StartNew = new Rectangle(Label_StartNewGame.Location, Label_StartNewGame.Size);

            Color Cr_StartNew = Com.ColorManipulation.BlendByRGB(EnterGameBackColor_INC, EnterGameBackColor_DEC, Math.Sqrt((double)(Label_StartNewGame.Height - EnterGameButtonHeight_Min) / (EnterGameButtonHeight_Max - EnterGameButtonHeight_Min)));

            GraphicsPath Path_StartNew = new GraphicsPath();
            Path_StartNew.AddRectangle(Rect_StartNew);
            PathGradientBrush PGB_StartNew = new PathGradientBrush(Path_StartNew)
            {
                CenterColor = Cr_StartNew,
                SurroundColors = new Color[] { Com.ColorManipulation.BlendByRGB(Cr_StartNew, EnterGameBackColor_DEC, 0.7) },
                FocusScales = new PointF(1F, 0F)
            };
            e.Graphics.FillPath(PGB_StartNew, Path_StartNew);
            Path_StartNew.Dispose();
            PGB_StartNew.Dispose();

            //

            if (Label_ContinueLastGame.Visible)
            {
                Rectangle Rect_Continue = new Rectangle(Label_ContinueLastGame.Location, Label_ContinueLastGame.Size);

                Color Cr_Continue = Com.ColorManipulation.BlendByRGB(EnterGameBackColor_INC, EnterGameBackColor_DEC, Math.Sqrt((double)(Label_ContinueLastGame.Height - EnterGameButtonHeight_Min) / (EnterGameButtonHeight_Max - EnterGameButtonHeight_Min)));

                GraphicsPath Path_Continue = new GraphicsPath();
                Path_Continue.AddRectangle(Rect_Continue);
                PathGradientBrush PGB_Continue = new PathGradientBrush(Path_Continue)
                {
                    CenterColor = Cr_Continue,
                    SurroundColors = new Color[] { Com.ColorManipulation.BlendByRGB(Cr_Continue, EnterGameBackColor_DEC, 0.7) },
                    FocusScales = new PointF(1F, 0F)
                };
                e.Graphics.FillPath(PGB_Continue, Path_Continue);
                Path_Continue.Dispose();
                PGB_Continue.Dispose();
            }
        }

        private double EnterPrompt_Val = 0; // 闪烁相位。
        private double EnterPrompt_Step = 0.025; // 闪烁步长。

        private void Timer_EnterPrompt_Tick(object sender, EventArgs e)
        {
            //
            // Timer_EnterPrompt。
            //

            if (EnterPrompt_Val >= 0 && EnterPrompt_Val <= 1)
            {
                EnterPrompt_Val += EnterPrompt_Step;
            }

            if (EnterPrompt_Val < 0 || EnterPrompt_Val > 1)
            {
                EnterPrompt_Val = Math.Max(0, Math.Min(EnterPrompt_Val, 1));

                EnterPrompt_Step = -EnterPrompt_Step;
            }

            EnterGameBackColor_INC = Com.ColorManipulation.BlendByRGB(Me.RecommendColors.Border_INC, Me.RecommendColors.Border, EnterPrompt_Val).ToColor();

            //

            if (Label_ContinueLastGame.Visible)
            {
                Label_StartNewGame.Top = 0;

                if (Com.Geometry.CursorIsInControl(Label_StartNewGame))
                {
                    Label_StartNewGame.Height = Math.Max(EnterGameButtonHeight_Min, Math.Min(EnterGameButtonHeight_Max, Label_StartNewGame.Height + Math.Max(1, (EnterGameButtonHeight_Max - Label_StartNewGame.Height) / 4)));
                }
                else
                {
                    Label_StartNewGame.Height = Math.Max(EnterGameButtonHeight_Min, Math.Min(EnterGameButtonHeight_Max, Label_StartNewGame.Height - Math.Max(1, (Label_StartNewGame.Height - EnterGameButtonHeight_Min) / 4)));
                }

                Label_ContinueLastGame.Top = Label_StartNewGame.Bottom;
                Label_ContinueLastGame.Height = Panel_EnterGameSelection.Height - Label_ContinueLastGame.Top;
            }
            else
            {
                Label_StartNewGame.Height = EnterGameButtonHeight_Max;

                Label_StartNewGame.Top = (Panel_EnterGameSelection.Height - Label_StartNewGame.Height) / 2;
            }

            Label_StartNewGame.Width = (Int32)(Math.Sqrt((double)Label_StartNewGame.Height / EnterGameButtonHeight_Max) * Panel_EnterGameSelection.Width);
            Label_StartNewGame.Left = (Panel_EnterGameSelection.Width - Label_StartNewGame.Width) / 2;

            Label_ContinueLastGame.Width = (Int32)(Math.Sqrt((double)Label_ContinueLastGame.Height / EnterGameButtonHeight_Max) * Panel_EnterGameSelection.Width);
            Label_ContinueLastGame.Left = (Panel_EnterGameSelection.Width - Label_ContinueLastGame.Width) / 2;

            Label_StartNewGame.Font = new Font("微软雅黑", Math.Max(1F, (Label_StartNewGame.Height - 4) / 3F), FontStyle.Regular, GraphicsUnit.Point, 134);
            Label_ContinueLastGame.Font = new Font("微软雅黑", Math.Max(1F, (Label_ContinueLastGame.Height - 4) / 3F), FontStyle.Regular, GraphicsUnit.Point, 134);

            Label_StartNewGame.ForeColor = Com.ColorManipulation.BlendByRGB(Me.RecommendColors.Text_INC, Me.RecommendColors.Text, Math.Sqrt((double)(Label_StartNewGame.Height - EnterGameButtonHeight_Min) / (EnterGameButtonHeight_Max - EnterGameButtonHeight_Min))).ToColor();
            Label_ContinueLastGame.ForeColor = Com.ColorManipulation.BlendByRGB(Me.RecommendColors.Text_INC, Me.RecommendColors.Text, Math.Sqrt((double)(Label_ContinueLastGame.Height - EnterGameButtonHeight_Min) / (EnterGameButtonHeight_Max - EnterGameButtonHeight_Min))).ToColor();

            //

            Panel_EnterGameSelection.Refresh();
        }

        #endregion

        #region "记录"区域

        private void PaintScore(PaintEventArgs e)
        {
            //
            // 绘制成绩。
            //

            Graphics Grap = e.Graphics;
            Grap.SmoothingMode = SmoothingMode.AntiAlias;

            //

            Int32 RectBottom = Panel_Score.Height - 50;

            Size RectSize_Max = new Size(Math.Max(2, Panel_Score.Width / 8), Math.Max(2, Panel_Score.Height - 120));
            Size RectSize_Min = new Size(Math.Max(2, Panel_Score.Width / 8), 2);

            Rectangle Rect_This = new Rectangle();
            Rectangle Rect_Best = new Rectangle();

            if (BestRecord.GameTime.TotalMilliseconds == 0 || BestRecord.StepCount == 0)
            {
                Rect_Best.Size = new Size(RectSize_Max.Width, RectSize_Min.Height);
                Rect_This.Size = new Size(Rect_Best.Width, RectSize_Min.Height);
            }
            else
            {
                Record ThRec = new Record();

                if (ThisRecord.OrderValue == (Int32)Order && ThisRecord.DifficultyLevel == DifficultyLevel)
                {
                    ThRec = ThisRecord;
                }

                if (BestRecord.GameTime.TotalMilliseconds >= ThRec.GameTime.TotalMilliseconds)
                {
                    Rect_Best.Size = RectSize_Max;
                    Rect_This.Size = new Size(Rect_Best.Width, (Int32)Math.Max(RectSize_Min.Height, Math.Sqrt(ThRec.GameTime.TotalMilliseconds / BestRecord.GameTime.TotalMilliseconds) * Rect_Best.Height));
                }
                else
                {
                    Rect_This.Size = RectSize_Max;
                    Rect_Best.Size = new Size(Rect_This.Width, (Int32)Math.Max(RectSize_Min.Height, Math.Sqrt(BestRecord.GameTime.TotalMilliseconds / ThRec.GameTime.TotalMilliseconds) * Rect_This.Height));
                }
            }

            Rect_This.Location = new Point((Panel_Score.Width / 2 - Rect_This.Width) / 2, RectBottom - Rect_This.Height);
            Rect_Best.Location = new Point(Panel_Score.Width / 2 + (Panel_Score.Width / 2 - Rect_Best.Width) / 2, RectBottom - Rect_Best.Height);

            Color RectCr = Me.RecommendColors.Border.ToColor();

            GraphicsPath Path_This = new GraphicsPath();
            Path_This.AddRectangle(Rect_This);
            PathGradientBrush PGB_This = new PathGradientBrush(Path_This)
            {
                CenterColor = RectCr,
                SurroundColors = new Color[] { Com.ColorManipulation.ShiftLightnessByHSL(RectCr, 0.3) },
                FocusScales = new PointF(0F, 1F)
            };
            Grap.FillPath(PGB_This, Path_This);
            Path_This.Dispose();
            PGB_This.Dispose();

            GraphicsPath Path_Best = new GraphicsPath();
            Path_Best.AddRectangle(Rect_Best);
            PathGradientBrush PGB_Best = new PathGradientBrush(Path_Best)
            {
                CenterColor = RectCr,
                SurroundColors = new Color[] { Com.ColorManipulation.ShiftLightnessByHSL(RectCr, 0.3) },
                FocusScales = new PointF(0F, 1F)
            };
            Grap.FillPath(PGB_Best, Path_Best);
            Path_Best.Dispose();
            PGB_Best.Dispose();

            //

            Label_ThisRecordVal_StepCount.Left = Math.Max(0, Math.Min(Panel_Score.Width - Label_ThisRecordVal_StepCount.Width, (Panel_Score.Width / 2 - Label_ThisRecordVal_StepCount.Width) / 2));
            Label_ThisRecordVal_StepCount.Top = Rect_This.Y - 5 - Label_ThisRecordVal_StepCount.Height;
            Label_ThisRecordVal_GameTime.Left = Math.Max(0, Math.Min(Panel_Score.Width - Label_ThisRecordVal_GameTime.Width, (Panel_Score.Width / 2 - Label_ThisRecordVal_GameTime.Width) / 2));
            Label_ThisRecordVal_GameTime.Top = Label_ThisRecordVal_StepCount.Top - Label_ThisRecordVal_GameTime.Height;

            Label_BestRecordVal_StepCount.Left = Math.Max(0, Math.Min(Panel_Score.Width - Label_BestRecordVal_StepCount.Width, Panel_Score.Width / 2 + (Panel_Score.Width / 2 - Label_BestRecordVal_StepCount.Width) / 2));
            Label_BestRecordVal_StepCount.Top = Rect_Best.Y - 5 - Label_BestRecordVal_StepCount.Height;
            Label_BestRecordVal_GameTime.Left = Math.Max(0, Math.Min(Panel_Score.Width - Label_BestRecordVal_GameTime.Width, Panel_Score.Width / 2 + (Panel_Score.Width / 2 - Label_BestRecordVal_GameTime.Width) / 2));
            Label_BestRecordVal_GameTime.Top = Label_BestRecordVal_StepCount.Top - Label_BestRecordVal_GameTime.Height;
        }

        #endregion

        #region "选项"区域

        // 数独大小。

        private void RadioButton_Order3_CheckedChanged(object sender, EventArgs e)
        {
            //
            // RadioButton_Order3 选中状态改变。
            //

            if (RadioButton_Order3.Checked)
            {
                Order = Orders.Order3;
            }
        }

        private void RadioButton_Order4_CheckedChanged(object sender, EventArgs e)
        {
            //
            // RadioButton_Order4 选中状态改变。
            //

            if (RadioButton_Order4.Checked)
            {
                Order = Orders.Order4;
            }
        }

        private void RadioButton_Order5_CheckedChanged(object sender, EventArgs e)
        {
            //
            // RadioButton_Order5 选中状态改变。
            //

            if (RadioButton_Order5.Checked)
            {
                Order = Orders.Order5;
            }
        }

        // 难度。

        private const Int32 DifficultyLevelMouseWheelStep = 1; // 难度等级的鼠标滚轮调节步长。

        private bool DifficultyLevelIsAdjusting = false; // 是否正在调整难度等级。

        private Bitmap DLTrbBmp; // 难度等级调节器位图。

        private Graphics DLTrbBmpGrap; // 难度等级调节器位图绘图。

        private Size DLTrbSliderSize => new Size(2, Panel_DifficultyLevelAdjustment.Height); // 难度等级调节器滑块大小。

        private void UpdateDLTrb()
        {
            //
            // 更新难度等级调节器位图。
            //

            if (DLTrbBmp != null)
            {
                DLTrbBmp.Dispose();
            }

            DLTrbBmp = new Bitmap(Math.Max(1, Panel_DifficultyLevelAdjustment.Width), Math.Max(1, Panel_DifficultyLevelAdjustment.Height));

            DLTrbBmpGrap = Graphics.FromImage(DLTrbBmp);

            DLTrbBmpGrap.Clear(Panel_DifficultyLevelAdjustment.BackColor);

            //

            Color Color_Slider, Color_ScrollBar_Current, Color_ScrollBar_Unavailable;

            if (Com.Geometry.CursorIsInControl(Panel_DifficultyLevelAdjustment) || DifficultyLevelIsAdjusting)
            {
                Color_Slider = Com.ColorManipulation.ShiftLightnessByHSL(Me.RecommendColors.Border_INC, 0.3).ToColor();
                Color_ScrollBar_Current = Com.ColorManipulation.ShiftLightnessByHSL(Me.RecommendColors.Border_INC, 0.3).ToColor();
                Color_ScrollBar_Unavailable = Com.ColorManipulation.ShiftLightnessByHSL(Me.RecommendColors.Border_DEC, 0.3).ToColor();
            }
            else
            {
                Color_Slider = Me.RecommendColors.Border_INC.ToColor();
                Color_ScrollBar_Current = Me.RecommendColors.Border_INC.ToColor();
                Color_ScrollBar_Unavailable = Me.RecommendColors.Border_DEC.ToColor();
            }

            Rectangle Rect_Slider = new Rectangle(new Point((Panel_DifficultyLevelAdjustment.Width - DLTrbSliderSize.Width) * (DifficultyLevel - DifficultyLevel_MIN) / (DifficultyLevel_MAX - DifficultyLevel_MIN), 0), DLTrbSliderSize);
            Rectangle Rect_ScrollBar_Current = new Rectangle(new Point(0, 0), new Size(Rect_Slider.X, Panel_DifficultyLevelAdjustment.Height));
            Rectangle Rect_ScrollBar_Unavailable = new Rectangle(new Point(Rect_Slider.Right, 0), new Size(Panel_DifficultyLevelAdjustment.Width - Rect_Slider.Right, Panel_DifficultyLevelAdjustment.Height));

            Rect_Slider.Width = Math.Max(1, Rect_Slider.Width);
            Rect_ScrollBar_Current.Width = Math.Max(1, Rect_ScrollBar_Current.Width);
            Rect_ScrollBar_Unavailable.Width = Math.Max(1, Rect_ScrollBar_Unavailable.Width);

            GraphicsPath Path_ScrollBar_Unavailable = new GraphicsPath();
            Path_ScrollBar_Unavailable.AddRectangle(Rect_ScrollBar_Unavailable);
            PathGradientBrush PGB_ScrollBar_Unavailable = new PathGradientBrush(Path_ScrollBar_Unavailable)
            {
                CenterColor = Color_ScrollBar_Unavailable,
                SurroundColors = new Color[] { Com.ColorManipulation.ShiftLightnessByHSL(Color_ScrollBar_Unavailable, 0.3) },
                FocusScales = new PointF(1F, 0F)
            };
            DLTrbBmpGrap.FillPath(PGB_ScrollBar_Unavailable, Path_ScrollBar_Unavailable);
            Path_ScrollBar_Unavailable.Dispose();
            PGB_ScrollBar_Unavailable.Dispose();

            GraphicsPath Path_ScrollBar_Current = new GraphicsPath();
            Path_ScrollBar_Current.AddRectangle(Rect_ScrollBar_Current);
            PathGradientBrush PGB_ScrollBar_Current = new PathGradientBrush(Path_ScrollBar_Current)
            {
                CenterColor = Color_ScrollBar_Current,
                SurroundColors = new Color[] { Com.ColorManipulation.ShiftLightnessByHSL(Color_ScrollBar_Current, 0.3) },
                FocusScales = new PointF(1F, 0F)
            };
            DLTrbBmpGrap.FillPath(PGB_ScrollBar_Current, Path_ScrollBar_Current);
            Path_ScrollBar_Current.Dispose();
            PGB_ScrollBar_Current.Dispose();

            GraphicsPath Path_Slider = new GraphicsPath();
            Path_Slider.AddRectangle(Rect_Slider);
            PathGradientBrush PGB_Slider = new PathGradientBrush(Path_Slider)
            {
                CenterColor = Color_Slider,
                SurroundColors = new Color[] { Com.ColorManipulation.ShiftLightnessByHSL(Color_Slider, 0.3) },
                FocusScales = new PointF(1F, 0F)
            };
            DLTrbBmpGrap.FillPath(PGB_Slider, Path_Slider);
            Path_Slider.Dispose();
            PGB_Slider.Dispose();

            //

            Label_DifficultyLevel_Val.Text = DifficultyLevel.ToString();
            Label_DifficultyLevel_Val.Left = Math.Max(Panel_DifficultyLevelAdjustment.Left, Math.Min(Panel_DifficultyLevelAdjustment.Left + Panel_DifficultyLevelAdjustment.Width - Label_DifficultyLevel_Val.Width, Panel_DifficultyLevelAdjustment.Left + Rect_Slider.Left + (Rect_Slider.Width - Label_DifficultyLevel_Val.Width) / 2));
        }

        private void RepaintDLTrb()
        {
            //
            // 更新并重绘难度等级调节器位图。
            //

            UpdateDLTrb();

            if (DLTrbBmp != null)
            {
                Panel_DifficultyLevelAdjustment.CreateGraphics().DrawImage(DLTrbBmp, new Point(0, 0));
            }
        }

        private void DifficultyLevelAdjustment()
        {
            //
            // 调整难度等级。
            //

            Int32 CurPosXOfCtrl = Math.Max(-DLTrbSliderSize.Width, Math.Min(Com.Geometry.GetCursorPositionOfControl(Panel_DifficultyLevelAdjustment).X, Panel_DifficultyLevelAdjustment.Width + DLTrbSliderSize.Width));

            double DivisionWidth = (double)(Panel_DifficultyLevelAdjustment.Width - DLTrbSliderSize.Width) / (DifficultyLevel_MAX - DifficultyLevel_MIN);

            DifficultyLevel = (Int32)Math.Max(DifficultyLevel_MIN, Math.Min(DifficultyLevel_MIN + (CurPosXOfCtrl - (DLTrbSliderSize.Width - DivisionWidth) / 2) / DivisionWidth, DifficultyLevel_MAX));

            RepaintDLTrb();
        }

        private void Panel_DifficultyLevelAdjustment_Paint(object sender, PaintEventArgs e)
        {
            //
            // Panel_DifficultyLevelAdjustment 绘图。
            //

            UpdateDLTrb();

            if (DLTrbBmp != null)
            {
                e.Graphics.DrawImage(DLTrbBmp, new Point(0, 0));
            }
        }

        private void Panel_DifficultyLevelAdjustment_MouseEnter(object sender, EventArgs e)
        {
            //
            // 鼠标进入 Panel_DifficultyLevelAdjustment。
            //

            RepaintDLTrb();
        }

        private void Panel_DifficultyLevelAdjustment_MouseLeave(object sender, EventArgs e)
        {
            //
            // 鼠标离开 Panel_DifficultyLevelAdjustment。
            //

            RepaintDLTrb();
        }

        private void Panel_DifficultyLevelAdjustment_MouseDown(object sender, MouseEventArgs e)
        {
            //
            // 鼠标按下 Panel_DifficultyLevelAdjustment。
            //

            if (e.Button == MouseButtons.Left)
            {
                DifficultyLevelIsAdjusting = true;

                DifficultyLevelAdjustment();
            }
        }

        private void Panel_DifficultyLevelAdjustment_MouseUp(object sender, MouseEventArgs e)
        {
            //
            // 鼠标释放 Panel_DifficultyLevelAdjustment。
            //

            if (e.Button == MouseButtons.Left)
            {
                DifficultyLevelIsAdjusting = false;
            }
        }

        private void Panel_DifficultyLevelAdjustment_MouseMove(object sender, MouseEventArgs e)
        {
            //
            // 鼠标经过 Panel_DifficultyLevelAdjustment。
            //

            if (DifficultyLevelIsAdjusting)
            {
                DifficultyLevelAdjustment();
            }
        }

        private void Panel_DifficultyLevelAdjustment_MouseWheel(object sender, MouseEventArgs e)
        {
            //
            // 鼠标滚轮在 Panel_DifficultyLevelAdjustment 滚动。
            //

            if (e.Delta > 0)
            {
                if (DifficultyLevel % DifficultyLevelMouseWheelStep == 0)
                {
                    DifficultyLevel = Math.Min(DifficultyLevel_MAX, DifficultyLevel + DifficultyLevelMouseWheelStep);
                }
                else
                {
                    if (DifficultyLevel > 0)
                    {
                        DifficultyLevel = Math.Min(DifficultyLevel_MAX, DifficultyLevel - DifficultyLevel % DifficultyLevelMouseWheelStep + DifficultyLevelMouseWheelStep);
                    }
                    else
                    {
                        DifficultyLevel = Math.Min(DifficultyLevel_MAX, DifficultyLevel - DifficultyLevel % DifficultyLevelMouseWheelStep);
                    }
                }
            }
            else if (e.Delta < 0)
            {
                if (DifficultyLevel % DifficultyLevelMouseWheelStep == 0)
                {
                    DifficultyLevel = Math.Max(DifficultyLevel_MIN, DifficultyLevel - DifficultyLevelMouseWheelStep);
                }
                else
                {
                    if (DifficultyLevel > 0)
                    {
                        DifficultyLevel = Math.Max(DifficultyLevel_MIN, DifficultyLevel - DifficultyLevel % DifficultyLevelMouseWheelStep);
                    }
                    else
                    {
                        DifficultyLevel = Math.Max(DifficultyLevel_MIN, DifficultyLevel - DifficultyLevel % DifficultyLevelMouseWheelStep - DifficultyLevelMouseWheelStep);
                    }
                }
            }

            RepaintDLTrb();
        }

        // 提示。

        private void CheckBox_ShowNotes_CheckedChanged(object sender, EventArgs e)
        {
            //
            // CheckBox_ShowNotes 选中状态改变。
            //

            ShowNotes = CheckBox_ShowNotes.Checked;

            Label_ShowNotes_Info.Enabled = ShowNotes;
        }

        // 操作方式。

        private void RadioButton_Mouse_CheckedChanged(object sender, EventArgs e)
        {
            //
            // RadioButton_Mouse 选中状态改变。
            //

            if (RadioButton_Mouse.Checked)
            {
                OperationMode = OperationModes.Mouse;
            }
        }

        private void RadioButton_Touch_CheckedChanged(object sender, EventArgs e)
        {
            //
            // RadioButton_Touch 选中状态改变。
            //

            if (RadioButton_Touch.Checked)
            {
                OperationMode = OperationModes.Touch;
            }
        }

        private void RadioButton_Keyboard_CheckedChanged(object sender, EventArgs e)
        {
            //
            // RadioButton_Keyboard 选中状态改变。
            //

            if (RadioButton_Keyboard.Checked)
            {
                OperationMode = OperationModes.Keyboard;
            }
        }

        private void CheckBox_AlwaysEnableKeyboard_CheckedChanged(object sender, EventArgs e)
        {
            //
            // CheckBox_AlwaysEnableKeyboard 选中状态改变。
            //

            AlwaysEnableKeyboard = CheckBox_AlwaysEnableKeyboard.Checked;
        }

        // 自动保存。

        private void RadioButton_SaveEveryStep_CheckedChanged(object sender, EventArgs e)
        {
            //
            // RadioButton_SaveEveryStep 选中状态改变。
            //

            if (RadioButton_SaveEveryStep.Checked)
            {
                SaveEveryStep = true;
            }

            ResetCleanStepsControl();
        }

        private void ResetCleanStepsControl()
        {
            //
            // 重置清理步骤控件。
            //

            Label_CleanGameStepDone.Visible = false;

            if (SaveEveryStep && ElementIndexList_Last.Count > 0 && StepListString.Length > 0)
            {
                Label_TooSlow.Enabled = Label_CleanGameStep.Enabled = true;
            }
            else
            {
                Label_TooSlow.Enabled = Label_CleanGameStep.Enabled = false;
            }
        }

        private void Label_CleanGameStep_Click(object sender, EventArgs e)
        {
            //
            // 单击 Label_CleanGameStep。
            //

            CleanGameStep();

            //

            Label_CleanGameStepDone.Visible = true;
        }

        private void RadioButton_SaveLastStep_CheckedChanged(object sender, EventArgs e)
        {
            //
            // RadioButton_SaveLastStep 选中状态改变。
            //

            if (RadioButton_SaveLastStep.Checked)
            {
                SaveEveryStep = false;
            }

            ResetCleanStepsControl();
        }

        // 主题颜色。

        private void RadioButton_UseRandomThemeColor_CheckedChanged(object sender, EventArgs e)
        {
            //
            // RadioButton_UseRandomThemeColor 选中状态改变。
            //

            if (RadioButton_UseRandomThemeColor.Checked)
            {
                UseRandomThemeColor = true;
            }

            Label_ThemeColorName.Enabled = !UseRandomThemeColor;
        }

        private void RadioButton_UseCustomColor_CheckedChanged(object sender, EventArgs e)
        {
            //
            // RadioButton_UseCustomColor 选中状态改变。
            //

            if (RadioButton_UseCustomColor.Checked)
            {
                UseRandomThemeColor = false;
            }

            Label_ThemeColorName.Enabled = !UseRandomThemeColor;
        }

        private void Label_ThemeColorName_Click(object sender, EventArgs e)
        {
            //
            // 单击 Label_ThemeColorName。
            //

            ColorDialog_ThemeColor.Color = Me.ThemeColor.ToColor();

            Me.Enabled = false;

            DialogResult DR = ColorDialog_ThemeColor.ShowDialog();

            if (DR == DialogResult.OK)
            {
                Me.ThemeColor = new Com.ColorX(ColorDialog_ThemeColor.Color);
            }

            Me.Enabled = true;
        }

        // 抗锯齿。

        private void CheckBox_AntiAlias_CheckedChanged(object sender, EventArgs e)
        {
            //
            // CheckBox_AntiAlias 选中状态改变。
            //

            AntiAlias = CheckBox_AntiAlias.Checked;
        }

        #endregion

        #region "关于"区域

        private void Label_GitHub_Base_Click(object sender, EventArgs e)
        {
            //
            // 单击 Label_GitHub_Base。
            //

            Process.Start(URL_GitHub_Base);
        }

        private void Label_GitHub_Release_Click(object sender, EventArgs e)
        {
            //
            // 单击 Label_GitHub_Release。
            //

            Process.Start(URL_GitHub_Release);
        }

        #endregion

    }
}