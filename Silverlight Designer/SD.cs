using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.IO;
using Silverlight_Designer.Properties;
using System.Diagnostics;
using System.Xml;

namespace Silverlight_Designer
{

    public partial class SD : Form
    {
        public SD()
        {
            try
            {
                //InitializeComponent();
            }
            catch { }
            StoryBoardEditor.MAINPANEL = MAINPANEL;
            /*XmlTextReader bankReader = null;
            bankReader = new XmlTextReader("SBE.xml");

            while (bankReader.Read())
            {
                string Type1 = "";
                string TEXT = "";
                string XX = "";
                string YY = "";
            }*/
            this.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.SupportsTransparentBackColor, true);
            //Properties.Height = 22;
            //vScrollBar1.Maximum = Properties.Height/8;
            //vScrollBar1.SmallChange = splitContainer1.Panel2.Height - 20
        }

        #region Variables
        private LABEL1 yy, LABEL, LOBJECT;
        private RSE RSEOBJECT, kk;
        private string NL = @"
";
        private string originalSizeWidth = "300", originalSizeHeight = "300", originalBC = "white", portion;
        private string SAVEPATH = "C:/", FILENAME, DD;
        public static string mode = "none";
        public float[] positions1 = { 0.0f, 0.5f, 1.0f };
        public int zText = 0, zRect = 0, zCirc = 0, zEcli = 0, zImage = 0, OLDWidth, clipLeft, clipTop, clipWidth, clipHeight, mouseX, mouseY;
        private RESIZEPNL P5, P6, P7, P8;
        private bool MouseIsOverResizePanel = false, Dragging = false;
        public static bool UseFunctions = true;
        private SLDObjectRef SLDOBJECTREF = new SLDObjectRef();
        public static bool SBEMonitoring = false, SBEOpened = false, SBESave = false;
        StoryBoardEditor SBE = new StoryBoardEditor();
        public static StoryBoardEditor.StoryBoard[] SBEData;
        #endregion

        #region Graphics
        private void zz_PaintEcli(object sender, PaintEventArgs e)
        {
            RSE i = (RSE)sender;
            LinearGradientBrush linGrBrush = new LinearGradientBrush(
                new Point(0, i.Height),
                new Point(i.Width, i.Height),
                Color.LightYellow,
                Color.LightYellow);
            Pen X1 = new Pen(Color.Black);
            Pen X2 = new Pen(Color.LightGray);
            e.Graphics.FillEllipse(linGrBrush, 0, 0, i.Width, i.Height);
            e.Graphics.DrawEllipse(X1, 0, 0, i.Width, i.Height);
            e.Graphics.DrawRectangle(X2, 0, 0, i.Width, i.Height);
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

            LinearGradientBrush linGrBrush = new LinearGradientBrush(
                new Point(0, panel3.Height),
                new Point(panel3.Width, panel3.Height),
                Color.FromArgb(10, 100, 255),
                Color.FromArgb(200, 231, 255));
            e.Graphics.FillRectangle(linGrBrush, 0, 0, panel3.Width, panel3.Height);
        }
        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {
            Color[] Colors1 = new Color[]{Color.FromArgb(10, 100, 255),
                                Color.FromArgb(200, 231, 255),
                                Color.FromArgb(200, 231, 255)};

            ColorBlend mycolor = new ColorBlend();
            mycolor.Colors = Colors1;
            float[] Positions2 = { 0.0f, 0.4f, 1.0f };
            mycolor.Positions = Positions2;


            LinearGradientBrush linGrBrush = new LinearGradientBrush(
                new Point(0, panel4.Height),
                new Point(splitContainer1.Panel2.Width, panel4.Height),
                Color.FromArgb(10, 100, 255),
                Color.FromArgb(200, 231, 255));
            linGrBrush.InterpolationColors = mycolor;
            e.Graphics.FillRectangle(linGrBrush, 0, 0, splitContainer1.Panel2.Width, panel4.Height);
        }
        
        private void splitContainer1_Paint(object sender, PaintEventArgs e)
        {
            LinearGradientBrush linGrBrush = new LinearGradientBrush(
                new Point(splitContainer1.Panel1.Width, 0),
                new Point(splitContainer1.Panel1.Width, splitContainer1.Panel1.Height),
                Color.FromArgb(10, 100, 255),
                Color.FromArgb(200, 231, 255));
            e.Graphics.FillRectangle(linGrBrush, 0, 0, splitContainer1.Panel1.Width, splitContainer1.Panel1.Height);

        }
        #endregion

        #region save/load/New
        #region New
        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            New();
        }
        public void New()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SD));
            MAINPANEL.Controls.Clear();
            SAVEPATH = "C:/";
            originalSizeWidth = "300";
            originalSizeHeight = "300";
            originalBC = "white";
            mode = "none";
            LOBJECT = null;
            RSEOBJECT = null;
            EXAML.Text = "<Canvas\n   xmlns=\"http://schemas.microsoft.com/client/2007\"\n   xmlns:x=\"http://sc" +
                "hemas.microsoft.com/winfx/2006/xaml\">\n\n</Canvas>";
            EJS.Text = "";
            Silverlight.Text = "if(!window.Silverlight)window.Silverlight={};Silverlight._silverlightCount=0;Silverlight.ua=null;Silverlight.available=false;Silverlight.fwlinkRoot=\"http://go.microsoft.com/fwlink/?LinkID=\";Silverlight.detectUserAgent=function(){var a=window.navigator.userAgent;Silverlight.ua={OS:\"Unsupported\",Browser:\"Unsupported\"};if(a.indexOf(\"Windows NT\")>=0)Silverlight.ua.OS=\"Windows\";else if(a.indexOf(\"PPC Mac OS X\")>=0)Silverlight.ua.OS=\"MacPPC\";else if(a.indexOf(\"Intel Mac OS X\")>=0)Silverlight.ua.OS=\"MacIntel\";if(Silverlight.ua.OS!=\"Unsupported\")if(a.indexOf(\"MSIE\")>=0){if(navigator.userAgent.indexOf(\"Win64\")==-1)if(parseInt(a.split(\"MSIE\")[1])>=6)Silverlight.ua.Browser=\"MSIE\"}else if(a.indexOf(\"Firefox\")>=0){var b=a.split(\"Firefox/\")[1].split(\".\"),c=parseInt(b[0]);if(c>=2)Silverlight.ua.Browser=\"Firefox\";else{var d=parseInt(b[1]);if(c==1&&d>=5)Silverlight.ua.Browser=\"Firefox\"}}else if(a.indexOf(\"Safari\")>=0)Silverlight.ua.Browser=\"Safari\"};Silverlight.detectUserAgent();Silverlight.isInstalled=function(d){var c=false,a=null;try{var b=null;if(Silverlight.ua.Browser==\"MSIE\")b=new ActiveXObject(\"AgControl.AgControl\");else if(navigator.plugins[\"Silverlight Plug-In\"]){a=document.createElement(\"div\");document.body.appendChild(a);if(Silverlight.ua.Browser==\"Safari\")a.innerHTML='<embed type=\"application/x-silverlight\" />';else a.innerHTML='<object type=\"application/x-silverlight\"  data=\"data:,\" />';b=a.childNodes[0]}document.body.innerHTML;if(b.IsVersionSupported(d))c=true;b=null;Silverlight.available=true}catch(e){c=false}if(a)document.body.removeChild(a);return c};Silverlight.createObject=function(l,g,m,j,k,i,h){var b={},a=j,c=k;a.source=l;b.parentElement=g;b.id=Silverlight.HtmlAttributeEncode(m);b.width=Silverlight.HtmlAttributeEncode(a.width);b.height=Silverlight.HtmlAttributeEncode(a.height);b.ignoreBrowserVer=Boolean(a.ignoreBrowserVer);b.inplaceInstallPrompt=Boolean(a.inplaceInstallPrompt);b.onGetSilverlightClick=Silverlight.HtmlAttributeEncode(c.onGetSilverlightClick);var e=a.version.split(\".\");b.shortVer=e[0]+\".\"+e[1];b.version=a.version;a.initParams=i;a.windowless=a.isWindowless;a.maxFramerate=a.framerate;for(var d in c)if(c[d]&&d!=\"onLoad\"&&d!=\"onError\"){a[d]=c[d];c[d]=null}delete a.width;delete a.height;delete a.id;delete a.onLoad;delete a.onError;delete a.ignoreBrowserVer;delete a.inplaceInstallPrompt;delete a.version;delete a.isWindowless;delete a.framerate;delete a.data;delete a.src;delete c.onGetSilverlightClick;if(Silverlight.isInstalled(b.version)){if(Silverlight._silverlightCount==0)if(window.addEventListener)window.addEventListener(\"onunload\",Silverlight.__cleanup,false);else window.attachEvent(\"onunload\",Silverlight.__cleanup);var f=Silverlight._silverlightCount++;a.onLoad=\"__slLoad\"+f;a.onError=\"__slError\"+f;window[a.onLoad]=function(a){if(c.onLoad)c.onLoad(document.getElementById(b.id),h,a)};window[a.onError]=function(a,b){if(c.onError)c.onError(a,b);else Silverlight.default_error_handler(a,b)};slPluginHTML=Silverlight.buildHTML(b,a)}else slPluginHTML=Silverlight.buildPromptHTML(b);if(b.parentElement)b.parentElement.innerHTML=slPluginHTML;else return slPluginHTML};Silverlight.supportedUserAgent=function(){var a=Silverlight.ua,b=a.OS==\"Unsupported\"||a.Browser==\"Unsupported\"||a.OS==\"Windows\"&&a.Browser==\"Safari\"||a.OS.indexOf(\"Mac\")>=0&&a.Browser==\"IE\";return !b};Silverlight.buildHTML=function(c,d){var a=[],e,i,g,f,h;if(Silverlight.ua.Browser==\"Safari\"){a.push(\"<embed \");e=\"\";i=\" \";g='=\"';f='\"';h=' type=\"application/x-silverlight\"/>'+\"<iframe style='visibility:hidden;height:0;width:0'/>\"}else{a.push('<object type=\"application/x-silverlight\" data=\"data:,\"');e=\">\";i=' <param name=\"';g='\" value=\"';f='\" />';h=\"</object>\"}a.push(' id=\"'+c.id+'\" width=\"'+c.width+'\" height=\"'+c.height+'\" '+e);for(var b in d)if(d[b])a.push(i+Silverlight.HtmlAttributeEncode(b)+g+Silverlight.HtmlAttributeEncode(d[b])+f);a.push(h);return a.join(\"\")};Silverlight.default_error_handler=function(e,b){var d,c=b.ErrorType;d=b.ErrorCode;var a=\"\nSilverlight error message     \n\";a+=\"ErrorCode: \"+d+\"\n\";a+=\"ErrorType: \"+c+\"       \n\";a+=\"Message: \"+b.ErrorMessage+\"     \n\";if(c==\"ParserError\"){a+=\"XamlFile: \"+b.xamlFile+\"     \n\";a+=\"Line: \"+b.lineNumber+\"     \n\";a+=\"Position: \"+b.charPosition+\"     \n\"}else if(c==\"RuntimeError\"){if(b.lineNumber!=0){a+=\"Line: \"+b.lineNumber+\"     \n\";a+=\"Position: \"+b.charPosition+\"     \n\"}a+=\"MethodName: \"+b.methodName+\"     \n\"}alert(a)};Silverlight.createObjectEx=function(b){var a=b,c=Silverlight.createObject(a.source,a.parentElement,a.id,a.properties,a.events,a.initParams,a.context);if(a.parentElement==null)return c};Silverlight.buildPromptHTML=function(j){var a=null,h=Silverlight.fwlinkRoot,c=Silverlight.ua.OS,d=j.onGetSilverlightClick,b=\"92822\",e,f=\"Get Microsoft Silverlight\",m=\"0x409\";if(!d)d=\"\";else d+=\"();\";if(j.inplaceInstallPrompt){var n=\"104745\";if(Silverlight.available)e=\"104746\";else e=\"104747\";var i=\"93481\",g=\"93483\";if(c==\"Windows\"){b=\"92799\";i=\"92803\";g=\"92805\"}else if(c==\"MacIntel\"){b=\"92808\";i=\"92804\";g=\"92806\"}else if(c==\"MacPPC\"){b=\"92807\";i=\"92815\";g=\"92816\"}var l='By clicking <b>\"Get Microsoft Silverlight\"</b> you accept the<br /><a title=\"Silverlight License Agreement\" href=\"{2}\" target=\"_blank\" style=\"text-decoration: underline; color: #0000CC\"><b>Silverlight license agreement</b></a>',k='Silverlight updates automatically, <a title=\"Silverlight Privacy Statement\" href=\"{3}\" target=\"_blank\" style=\"text-decoration: underline; color: #0000CC\"><b>learn more</b></a>';a='<table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"206px\" style=\"font-size: 55%; font-family: Verdana;  color: #5E5D5D;\"><tr><td><img style=\"display: block; cursor: pointer; border= 0;\" title=\"'+f+'\" alt=\"'+f+'\" onclick=\"javascript:Silverlight.followFWLink({0});'+d+'\" src=\"{1}\" /></td></tr><tr><td style=\"width: 206px; margin: 0px; background: #FFFFFF; text-align: left; border-left-style: solid; border-right-style: solid; border-color: #c7c7bd;padding-left: 6px; padding-right: 6px; padding-top: 3px; padding-bottom: 10px; border-width: 2px; \">'+l+'</td></tr><tr><td style=\"width: 206px; margin: 0px; background: #FFFFFF; text-align: left; border-left-style: solid; border-right-style: solid; padding-left: 6px; padding-right: 6px; border-color: #c7c7bd;padding-top: 0px; padding-bottom: 2px; border-width: 2px; \">'+k+'</td></tr><tr><td><img alt=\"\" src=\"{4}\" /></td></tr></table>';a=a.replace(\"{2}\",h+i);a=a.replace(\"{3}\",h+g);a=a.replace(\"{4}\",h+n)}else{if(Silverlight.available)e=\"94377\";else e=\"92801\";if(c==\"Windows\")b=\"92800\";else if(c==\"MacIntel\")b=\"92812\";else if(c==\"MacPPC\")b=\"92811\";a='<div style=\"display:block; width: 205px; height: 67px;\"><img onclick=\"javascript:Silverlight.followFWLink({0});'+d+'\" style=\"border:0; cursor:pointer\" src=\"{1}\" title=\"'+f+'\" alt=\"'+f+'\"/></div>'}a=a.replace(\"{0}\",b);a=a.replace(\"{1}\",h+e+\"&amp;clcid=\"+m);return a};Silverlight.__cleanup=function(){for(var a=Silverlight._silverlightCount-1;a>=0;a--){window[\"__slLoad\"+a]=null;window[\"__slError\"+a]=null}if(window.removeEventListener)window.removeEventListener(\"unload\",Silverlight.__cleanup,false);else window.detachEvent(\"onunload\",Silverlight.__cleanup)};Silverlight.followFWLink=function(a){top.location=Silverlight.fwlinkRoot+String(a)};Silverlight.HtmlAttributeEncode=function(c){var a,b=\"\";if(c==null)return null;for(var d=0;d<c.length;d++){a=c.charCodeAt(d);if(a>96&&a<123||a>64&&a<91||a>43&&a<58&&a!=47||a==95)b=b+String.fromCharCode(a);else b=b+\"&#\"+a+\";\"}return b}";
            EHTML.Text = "<html xml:lang=\"en\">\n" +
    "	<head>\n" +
    "		<title>A Sample HTML Page</title>\n" +
    "		<script type=\"text/javascript\" src=\"Silverlight.js\"></script>\n" +
    "		<script type=\"text/javascript\" src=\"createSilverlight.js\"></script>\n" +
    "		<script type=\"text/javascript\" src=\"extra.js\"></script>\n" +
    "	</head>\n" +
    "	<body>\n" +
    "		<div id=\"mySilverlightPluginHost\"></div>\n" +
    "		<script type=\"text/javascript\">\n" +
    "			var parentElement = document.getElementById(\"mySilverlightPluginHost\");\n" +
    "			createMySilverlightPlugin();\n" +
    "		</script>\n" +
    "	</body>\n" +
    "</html>";
            createSilverlight.Text = "function createMySilverlightPlugin()\n" +
    "{  \n" +
    "    Silverlight.createObject(\n" +
    "        \"main.xaml\",                  // Source property value.\n" +
    "        parentElement,                  // DOM reference to hosting DIV tag.\n" +
    "        \"mySilverlightPlugin\",         // Unique plug-in ID value.\n" +
    "        {                               // Per-instance properties.\n" +
    "            width:'300',                // Width of rectangular region of\n" +
    "                                        // plug-in area in pixels.\n" +
    "            height:'300',               // Height of rectangular region of\n" +
    "                                        // plug-in area in pixels.\n" +
    "            inplaceInstallPrompt:false, // Determines whether to display\n" +
    "                                        // in-place install prompt if\n" +
    "                                        // invalid version detected.\n" +
    "            background:'white',       // Background color of plug-in.\n" +
    "            isWindowless:'false',       // Determines whether to display plug-in\n" +
    "                                        // in Windowless mode.\n" +
    "            framerate:'24',             // MaxFrameRate property value.\n" +
    "            version:'1.0'               // Silverlight version to use.\n" +
    "        },\n" +
    "        {\n" +
    "            onError:null,               // OnError property value --\n" +
    "                                        // event handler function name.\n" +
    "            onLoad:null                 // OnLoad property value --\n" +
    "                                        // event handler function name.\n" +
    "        },\n" +
    "        null);                          // Context value -- event handler function name.\n" +
    "}";
            textBox1.Text = "300,300";
            MAINPANEL.Width = 300;
            MAINPANEL.Height = 300;
            MAINPANEL.BackColor = Color.White;
        }
        #endregion
        #region Save
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            zz1.Title = "Select Project Save";
            if (zz1.ShowDialog() == DialogResult.OK)
            {
                SAVEPATH = zz1.FileName;

                string[] ppz = SAVEPATH.Split('\\');
                FILENAME = ppz[ppz.Length - 1];

                createSilverlight.Text = createSilverlight.Text.Replace("main.xaml", FILENAME + "-main.xaml");
                EHTML.Text = createSilverlight.Text.Replace("Silverlight.js", FILENAME + "-Silverlight.js");
                EHTML.Text = createSilverlight.Text.Replace("createSilverlight.js", FILENAME + "-createSilverlight.js");
                EHTML.Text = createSilverlight.Text.Replace("extra.js", FILENAME + "-extra.js");

                string SAVEDIRECTORY = SAVEPATH + "-BIN\\";
                Directory.CreateDirectory(SAVEDIRECTORY);

                EXAML.SaveFile(SAVEDIRECTORY + FILENAME + "-main.xaml");
                EHTML.SaveFile(SAVEDIRECTORY + FILENAME + ".html");
                EJS.SaveFile(SAVEDIRECTORY + FILENAME + "-extra.js");
                Silverlight.SaveFile(SAVEDIRECTORY + FILENAME + "-Silverlight.js");
                createSilverlight.SaveFile(SAVEDIRECTORY + FILENAME + "-createSilverlight.js");
                createSDProj.SaveFile(SAVEPATH + ".SDProj");
            }
            StatusLabel.Text = "Saved";
        }

        private void saveToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            string[] ppz = SAVEPATH.Split('\\');
            FILENAME = ppz[ppz.Length];

            createSilverlight.Text = createSilverlight.Text.Replace("main.xaml", FILENAME + "-main.xaml");
            EHTML.Text = createSilverlight.Text.Replace("Silverlight.js", FILENAME + "-Silverlight.js");
            EHTML.Text = createSilverlight.Text.Replace("createSilverlight.js", FILENAME + "-createSilverlight.js");
            EHTML.Text = createSilverlight.Text.Replace("extra.js", FILENAME + "-extra.js");

            string SAVEDIRECTORY = SAVEPATH + "-BIN\\";
            Directory.CreateDirectory(SAVEDIRECTORY);

            EXAML.SaveFile(SAVEDIRECTORY + FILENAME + "-main.xaml");
            EHTML.SaveFile(SAVEDIRECTORY + FILENAME + ".html");
            EJS.SaveFile(SAVEDIRECTORY + FILENAME + "-extra.js");
            Silverlight.SaveFile(SAVEDIRECTORY + FILENAME + "-Silverlight.js");
            createSilverlight.SaveFile(SAVEDIRECTORY + FILENAME + "-createSilverlight.js");
            createSDProj.SaveFile(SAVEPATH + ".SDProj");
        }
        #endregion
        #region Load
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog oo = new OpenFileDialog();
            oo.Filter = "Silverlight Designer Project|*.SDProj";
            if (oo.ShowDialog() == DialogResult.OK)
            {
                SAVEPATH = oo.FileName;

                string[] ppz = SAVEPATH.Split('\\');
                FILENAME = ppz[ppz.Length - 1];
                Render(oo.FileName + "-BIN//" + FILENAME+"-main.xaml");
            }
        }
        #endregion
        #endregion
        #region Renderer
        private void Render(string XAMLPath)
        {
            RichTextBox op = new RichTextBox();
            op.Text = XAMLPath;
            string[] Lines = op.Text.Split('\n');
            UseFunctions = false;
            MAINPANEL.Controls.Clear();
            for (int I = 0; I <= Lines.Length-1; I++)
            {
                if (Lines[I].Contains("<Rectangle"))
                {
                    MessageBox.Show("Rectangle Discovered!");
                    RSE Recta = new RSE();
                    Recta.BorderStyle = BorderStyle.FixedSingle;
                    Recta.MouseDown += new MouseEventHandler(zz_MouseDown);
                    Recta.MouseUp += new MouseEventHandler(zz_MouseUp);
                    Recta.PreviewKeyDown += new PreviewKeyDownEventHandler(zz_PreviewKeyDown);
                    string[] RSEAttributes = Lines[I].Split(' ');
                    for (int p = 0; p <= RSEAttributes.Length; p++)
                    {
                        if (RSEAttributes[p].StartsWith("Canvas.Top=\""))
                        {
                            string o1 = RSEAttributes[p].Replace("Canvas.Top=\"", "");
                            string o2 = o1.Replace("\"", "");
                            Recta.Top = Convert.ToInt32(o2);
                        }
                        if (RSEAttributes[p].Contains("Canvas.Left=\""))
                        {
                            string o1 = RSEAttributes[p].Replace("Canvas.Left=\"", "");
                            string o2 = o1.Replace("\"", "");
                            Recta.Left = Convert.ToInt32(o2);
                        }
                        if (RSEAttributes[p].Contains("Width=\""))
                        {
                            string o1 = RSEAttributes[p].Replace("Width=\"", "");
                            string o2 = o1.Replace("\"", "");
                            Recta.Width = Convert.ToInt32(o2);
                        }
                        if (RSEAttributes[p].Contains("Height=\""))
                        {
                            string o1 = RSEAttributes[p].Replace("Height=\"", "");
                            string o2 = o1.Replace("\"", "");
                            Recta.Height = Convert.ToInt32(o2);
                        }
                        if (RSEAttributes[p].Contains("Fill=\""))
                        {
                            string o1 = RSEAttributes[p].Replace("Fill=\"", "");
                            string o2 = o1.Replace("\"", "");
                            Recta.BackColor = Color.FromName(o2);
                        }
                    }
                    Recta.type = "RSE";
                    MAINPANEL.Controls.Add(Recta);
                    Recta.Cursor = Cursors.SizeAll;
                    Recta.ContextMenuStrip = Element;
                    Recta.type = "RSE";
                    Recta.Name = "Rect" + zRect;
                }

                if (I == Lines.Length)
                {
                    UseFunctions = true;
                }
            }
        }
        #endregion
        #region Draw Stats:
        #region Draw Main

        
        private void PropBoxStaNormal()
        {
            Properties.Visible = false;
            SelTYPE.Text = "";
            //LOBJECT.BorderStyle = BorderStyle.None;
            //RSEOBJECT.BorderStyle = BorderStyle.None;
            RSEOBJECT = null;
            LOBJECT = null;
        }

        #region Timers
        private void DragEffect_Tick(object sender, EventArgs e)
        {
            if (DD == "Text")
            {
                Point nextPosition = new Point(); 
                nextPosition = this.PointToClient(MousePosition);
                nextPosition.Offset(mouseX, mouseY); 
                LOBJECT.Location = nextPosition;  
            }
            if (DD == "RSE")
            {
                Point nextPosition = new Point();
                nextPosition = this.PointToClient(MousePosition);
                nextPosition.Offset(mouseX, mouseY);
                RSEOBJECT.Location = nextPosition; 
                RSEOBJECT.Refresh();
            }
        }

        private void rectResizeEffect_Tick(object sender, EventArgs e)
        {
            RSEOBJECT.Width = Cursor.Position.X - (Left + 145 + RSEOBJECT.Left);
            RSEOBJECT.Height = Cursor.Position.Y - (Top + 75 + RSEOBJECT.Top);
            RSEOBJECT.Refresh();
        }
        #endregion

        #region MainPANEL
        private void MAINPANEL_MouseDown(object sender, MouseEventArgs e)
        {
            if (UseFunctions == true)
            {
                if (e.Button == MouseButtons.Left)
                {
                    if (mode == "drawEllipse") { createEllipse(); }
                    if (mode == "drawText") { createText(); mode = "NONE"; }
                    if (mode == "none") { PropBoxStaNormal(); }
                    if (mode == "drawRectangle") { createRectangle(); }
                    if (mode == "drawImage") { createImage(); }

                }
                if (e.Button == MouseButtons.Right)
                {
                    mode = "none";
                    MAINPANEL.Cursor = Cursors.Default;
                }
            }
        }

        private void MAINPANEL_MouseUp(object sender, MouseEventArgs e)
        {
            if (UseFunctions == true)
            {
                if (mode == "drawRectangle") { createRectangle1(); mode = "NONE"; }
                if (mode == "drawEllipse") { createEllipse1(); }
            }

        }
        #endregion
        #endregion
        #region Sub-RTC-Properties
        private void richTextBox9_Leave(object sender, EventArgs e)
        {

            if (portion == "RSE")
            {
                try
                {
                    kk = RSEOBJECT;
                    if (kk.type == "RSE")
                    {
                        EXAML.Lines[RSEOBJECT.ObjectID] = EXAML.Lines[RSEOBJECT.ObjectID].Replace("StrokeThickness=\"" + RSEOBJECT.Top + "\"", "StrokeThickness=\"" + richTextBox9.Text + "\"");
                        RSEOBJECT.BackColor = Color.FromName(richTextBox9.Text);
                    }
                }
                catch { }
            }
        }

        private void richTextBox8_Leave(object sender, EventArgs e)
        {

            if (portion == "RSE")
            {
                try
                {
                    kk = RSEOBJECT;
                    if (kk.type == "RSE")
                    {
                        EXAML.Lines[RSEOBJECT.ObjectID] = EXAML.Lines[RSEOBJECT.ObjectID].Replace("Stroke=\"" + RSEOBJECT.Top + "\"", "Stroke=\"" + richTextBox8.Text + "\"");
                        RSEOBJECT.BackColor = Color.FromName(richTextBox8.Text);
                    }
                }
                catch { }
            }
        }

        private void richTextBox7_Leave(object sender, EventArgs e)
        {
            if (portion == "Text")
            {
                try
                {
                    yy = LOBJECT;
                    if (kk.type == "RSE")
                    {
                        EXAML.Lines[LOBJECT.ObjectID] = EXAML.Lines[LOBJECT.ObjectID].Replace("ForeColor=\"" + LOBJECT.Top + "\"", "ForeColor=\"" + richTextBox7.Text + "\"");
                        RSEOBJECT.BackColor = Color.FromName(richTextBox7.Text);
                    }
                }
                catch { }
            }
            if (portion == "RSE")
            {
                try
                {
                    kk = RSEOBJECT;
                    if (kk.type == "RSE")
                    {
                        EXAML.Lines[RSEOBJECT.ObjectID] = EXAML.Lines[RSEOBJECT.ObjectID].Replace("Fill=\"" + RSEOBJECT.Top + "\"", "Fill=\"" + richTextBox7.Text + "\"");
                        RSEOBJECT.BackColor = Color.FromName(richTextBox7.Text);
                    }
                }
                catch { }
            }
        }

        private void richTextBox6_Leave(object sender, EventArgs e)
        {

            if (richTextBox6.Text != " " || richTextBox6.Text != "" || richTextBox6.Text != null && portion == "RSE")
            {
                try
                {
                    yy = LOBJECT;
                    if (yy.type == "Text")
                    {
                        EXAML.Lines[LOBJECT.ObjectID] = EXAML.Lines[LOBJECT.ObjectID].Replace("ForeColor=\"" + LOBJECT.Top + "\"", "ForeColor=\"" + richTextBox6.Text + "\"");
                        LOBJECT.ForeColor = Color.FromName(richTextBox6.Text);
                    }
                }
                catch { }
                try
                {
                    kk = RSEOBJECT;
                    if (kk.type == "RSE")
                    {
                        EXAML.Lines[RSEOBJECT.ObjectID] = EXAML.Lines[RSEOBJECT.ObjectID].Replace("Height=\"" + RSEOBJECT.Top + "\"", "Height=\"" + richTextBox6.Text + "\"");
                        RSEOBJECT.Height = Convert.ToInt32(richTextBox6.Text);
                    }
                }
                catch { }
            }
        }

        private void richTextBox5_Leave(object sender, EventArgs e)
        {

            if (richTextBox5.Text != " " || richTextBox5.Text != "" || richTextBox5.Text != null)
            {
                try
                {
                    yy = LOBJECT;
                    if (yy.type == "Text")
                    {
                        EXAML.Lines[LOBJECT.ObjectID] = EXAML.Lines[LOBJECT.ObjectID].Replace("Top=\"" + LOBJECT.Top + "\"", "Top=\"" + richTextBox5.Text + "\"");
                        LOBJECT.Top = Convert.ToInt32(richTextBox5.Text);
                    }
                }
                catch { }
                try
                {
                    kk = RSEOBJECT;
                    if (kk.type == "RSE")
                    {
                        EXAML.Lines[RSEOBJECT.ObjectID] = EXAML.Lines[RSEOBJECT.ObjectID].Replace("Width=\"" + RSEOBJECT.Top + "\"", "Width=\"" + richTextBox5.Text + "\"");
                        RSEOBJECT.Height = Convert.ToInt32(richTextBox5.Text);
                    }
                }
                catch { }
            }
        }

        private void richTextBox4_Leave(object sender, EventArgs e)
        {
            if (richTextBox4.Text != " " || richTextBox4.Text != "" || richTextBox4.Text != null && portion == "Text")
            {
                try
                {
                    yy = LOBJECT;
                    if (yy.type == "Text")
                    {
                        EXAML.Lines[LOBJECT.ObjectID] = EXAML.Lines[LOBJECT.ObjectID].Replace("Left=\"" + LOBJECT.Left + "\"", "Left=\"" + richTextBox5.Text + "\"");
                        LOBJECT.Left = Convert.ToInt32(richTextBox4.Text);
                    }
                }
                catch { }
                try
                {
                    kk = RSEOBJECT;
                    if (kk.type == "RSE")
                    {
                        EXAML.Lines[RSEOBJECT.ObjectID] = EXAML.Lines[RSEOBJECT.ObjectID].Replace("Top=\"" + RSEOBJECT.Top + "\"", "Top=\"" + richTextBox4.Text + "\"");
                        RSEOBJECT.Top = Convert.ToInt32(richTextBox4.Text);
                    }
                }
                catch { }
            }
        }

        private void richTextBox3_Leave(object sender, EventArgs e)
        {


            try
            {
                yy = LOBJECT;
                if (yy.type == "Text")
                {
                    EXAML.Lines[LOBJECT.ObjectID] = EXAML.Lines[LOBJECT.ObjectID].Replace(">" + LOBJECT.Text + "<", ">" + richTextBox3.Text + "<");
                    LOBJECT.Text = richTextBox3.Text;
                }
            }
            catch { }
            try
            {
                kk = RSEOBJECT;
                if (kk.type == "RSE")
                {
                    EXAML.Lines[RSEOBJECT.ObjectID] = EXAML.Lines[RSEOBJECT.ObjectID].Replace("Left=\"" + RSEOBJECT.Left + "\"", "Left=\"" + richTextBox3.Text + "\"");
                    RSEOBJECT.Left = Convert.ToInt32(richTextBox3.Text);
                }
            }
            catch { }
        }

        private void richTextBox2_Leave(object sender, EventArgs e)
        {
            try
            {
                yy = LOBJECT;
                if (yy.type == "Text")
                {
                    EXAML.Lines[LOBJECT.ObjectID] = EXAML.Lines[LOBJECT.ObjectID].Replace("x:Name=\"" + LOBJECT.Text + "\"", "x:Name=\"" + richTextBox3.Text + "\"");
                    LOBJECT.Name = richTextBox3.Text;
                }
            }
            catch { }
            try
            {
                kk = RSEOBJECT;
                if (kk.type == "RSE")
                {

                    EXAML.Lines[RSEOBJECT.ObjectID] = EXAML.Lines[RSEOBJECT.ObjectID].Replace("x:Name=\"" + RSEOBJECT.Top + "\"", "x:Name=\"" + richTextBox3.Text + "\"");
                    RSEOBJECT.Name = richTextBox3.Text;
                    
                }
            }
            catch { }
        }
        #endregion
        #region Sub-ZZ-Properties
        public void zz_MouseUp(object sender, MouseEventArgs e)
        {
            if (UseFunctions == true)
            {
                if (DD == "Text")
                {
                    //LOBJECT.Invalidate(); 
                    richTextBox4.Text = LOBJECT.Left.ToString();
                    richTextBox5.Text = LOBJECT.Top.ToString();
                    //string z = Xaml.Items[LOBJECT.ObjectID];
                    SetMove(LOBJECT, LOBJECT.Left, LOBJECT.Top);
                    EXAML.Lines[LOBJECT.ObjectID] = EXAML.Lines[LOBJECT.ObjectID].Replace("Top=\"" + LOBJECT.Top + "\"", "Top=\"" + richTextBox5.Text + "\"");
                    EXAML.Lines[LOBJECT.ObjectID] = EXAML.Lines[LOBJECT.ObjectID].Replace("Left=\"" + LOBJECT.Left + "\"", "Left=\"" + richTextBox4.Text + "\"");
                }
                if (DD == "RSE")
                {
                    //RSEOBJECT.Invalidate(); 
                    richTextBox4.Text = RSEOBJECT.Left.ToString();
                    richTextBox5.Text = RSEOBJECT.Top.ToString();
                    SetMove(RSEOBJECT, RSEOBJECT.Left, RSEOBJECT.Top);
                    EXAML.Lines[RSEOBJECT.ObjectID] = EXAML.Lines[RSEOBJECT.ObjectID].Replace("Top=\"" + RSEOBJECT.Top + "\"", "Top=\"" + richTextBox5.Text + "\"");
                    EXAML.Lines[RSEOBJECT.ObjectID] = EXAML.Lines[RSEOBJECT.ObjectID].Replace("Left=\"" + RSEOBJECT.Left + "\"", "Left=\"" + richTextBox4.Text + "\"");                    
                }
                Dragging = false;
                Cursor.Clip = System.Drawing.Rectangle.Empty; 
                this.Cursor = Cursors.Default;  
            }
        }

        private void SetMove(object LO, int X2, int Y2)
        {
            StoryBoardEditor.Changed = true;
            StoryBoardEditor.Object1 = LO;
            StoryBoardEditor.Type = "Move";
            StoryBoardEditor.X2 = X2;
            StoryBoardEditor.Y2 = Y2;
        }

        private void SetDel(object LO)
        {
            StoryBoardEditor.Changed = true;
            StoryBoardEditor.Object1 = LO;
            StoryBoardEditor.Type = "Delete";
        }

        public void zz_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (UseFunctions == true)
            {
                if (e.KeyCode == Keys.Delete)
                {
                    MessageBox.Show("-");
                    try
                    {
                        LABEL1 yy = (LABEL1)sender;
                        if (yy.type == "Text")
                        {
                            SetDel(yy);
                            MAINPANEL.Controls.Remove(yy);
                            PropBoxStaNormal();
                        }
                    }
                    catch { }
                    try
                    {
                        RSE kk = (RSE)sender;
                        if (kk.type == "RSE")
                        {
                            SetDel(kk);
                            MAINPANEL.Controls.Remove(kk);
                            PropBoxStaNormal();
                        }
                    }
                    catch { }
                }
            }
        }

        public void zz_MouseDown(object sender, MouseEventArgs e)
        {
            if (UseFunctions == true)
            {
                if (e.Button == MouseButtons.Left)
                {
                    mouseX = -e.X; 
                    mouseY = -e.Y; 
                    this.Cursor = Cursors.Hand;  
                    try
                    {
                        LABEL1 yy = (LABEL1)sender;
                        if (yy.type == "Text")
                        {
                            //LABEL1 yy = (LABEL1)sender;
                            LOBJECT = yy;
                            RSEOBJECT = null;
                            Objectz("Text");
                            DD = "Text";
                            yy.BringToFront();
                            clipLeft = Main.Left;
                            clipTop = Main.Top;
                            clipWidth = MAINPANEL.Width;
                            clipHeight = MAINPANEL.Height;
                            Cursor.Clip = this.RectangleToScreen(new Rectangle(clipLeft, clipTop, clipWidth, clipHeight)); 
                            yy.Invalidate(); 
                        }
                    }
                    catch { }
                    try
                    {
                        RSE kk = (RSE)sender;
                        if (kk.type == "RSE")
                        {
                            //RSE kk = (RSE)sender;
                            LOBJECT = null;
                            RSEOBJECT = kk;
                            Objectz("Rectangle");
                            DD = "RSE";
                            kk.BringToFront();
                            clipLeft = Main.Left;
                            clipTop = Main.Top;
                            clipWidth = MAINPANEL.Width;
                            clipHeight = MAINPANEL.Height;
                            Cursor.Clip = this.RectangleToScreen(new Rectangle(clipLeft, clipTop, clipWidth, clipHeight)); 
                            kk.Invalidate(); 
                        }
                    }
                    catch { }
                                         
                }

                if (e.Button == MouseButtons.Right)
                {
                    try
                    {
                        LABEL1 yy = (LABEL1)sender;
                        if (yy.type == "Text")
                        {
                            LOBJECT = yy;
                            RSEOBJECT = null;
                        }
                    }
                    catch { }
                    try
                    {
                        RSE kk = (RSE)sender;
                        if (kk.type == "Rect")
                        {
                            RSEOBJECT = kk;
                            LOBJECT = null;
                        }
                    }
                    catch { }
                }
                Dragging = true;
            }
        }
        private void zz_MouseMove(object sender, MouseEventArgs e)
        {
            if (Dragging)
            {
                try
                {
                    RSEOBJECT = (RSE)sender;
                    Point nextPosition = new Point();
                    nextPosition = this.PointToClient(MousePosition);
                    nextPosition.Offset(mouseX - Main.Left, mouseY - Main.Top);
                    RSEOBJECT.Location = nextPosition;
                }
                catch { }
                try
                {
                    LOBJECT = (LABEL1)sender;
                    Point nextPosition = new Point();
                    nextPosition = this.PointToClient(MousePosition);
                    nextPosition.Offset(mouseX - Main.Left, mouseY - Main.Top);
                    LOBJECT.Location = nextPosition;
                }
                catch { }
            }
        }
        #endregion
        #region Create Resize Panels
        private void CreateResizePanels(RSE sender)
        {
            RESIZEPNL P5z = new RESIZEPNL();
            RESIZEPNL P7z = new RESIZEPNL();
            RESIZEPNL P8z = new RESIZEPNL();
            P5z.Width = 10;
            P5z.Height = 10;
            P5z.BackColor = Color.White;
            P5z.BorderStyle = BorderStyle.FixedSingle;
            P5z.Top = sender.Height / 2 - 3;
            P5z.Left = sender.Width - 10;
            P5z.Parent = sender;
            P5z.Cursor = Cursors.SizeWE;
            P5z.MouseDown += new MouseEventHandler(P5z_MouseDown);
            P5z.MouseUp += new MouseEventHandler(P5z_MouseUp);
            sender.Controls.Add(P5z);
            P7z.Width = 10;
            P7z.Height = 10;
            P7z.BackColor = Color.White;
            P7z.BorderStyle = BorderStyle.FixedSingle;
            P7z.Top = sender.Height - 10;
            P7z.Left = sender.Width / 2 - 3;
            P7z.Parent = sender;
            P7z.Cursor = Cursors.SizeNS;
            P7z.MouseDown += new MouseEventHandler(P7z_MouseDown);
            P7z.MouseUp += new MouseEventHandler(P7z_MouseUp);
            sender.Controls.Add(P7z);
            P8z.Width = 10;
            P8z.Height = 10;
            P8z.BackColor = Color.White;
            P8z.BorderStyle = BorderStyle.FixedSingle;
            P8z.Top = sender.Height - 10;
            P8z.Left = sender.Width - 10;
            P8z.Parent = sender;
            P8z.Cursor = Cursors.SizeNWSE;
            P8z.MouseDown += new MouseEventHandler(P8z_MouseDown);
            P8z.MouseUp += new MouseEventHandler(P8z_MouseUp);
            sender.Controls.Add(P8z);
            sender.P5 = P5z;
            sender.P7 = P7z;
            sender.P8 = P8z;
            P5 = P5z;
            P7 = P7z;
            P8 = P8z;
            P5.MouseEnter += new EventHandler(P_MouseLeave);
            P5.MouseLeave += new EventHandler(P_MouseEnter);
            P7.MouseEnter += new EventHandler(P_MouseLeave);
            P7.MouseEnter += new EventHandler(P_MouseEnter);
            P8.MouseLeave += new EventHandler(P_MouseLeave);
            P8.MouseLeave += new EventHandler(P_MouseEnter);
            OLDWidth = sender.Width;
        }

        void P_MouseEnter(object sender, EventArgs e)
        {
            MouseIsOverResizePanel = true;
        }

        void P_MouseLeave(object sender, EventArgs e)
        {
            MouseIsOverResizePanel = false;
        }

        void P8z_MouseUp(object sender, MouseEventArgs e)
        {
            P8t.Stop();
        }
        void P8z_MouseDown(object sender, MouseEventArgs e)
        {
            P8t.Start();
            P8 = (RESIZEPNL)sender;
        }
        void P7z_MouseUp(object sender, MouseEventArgs e)
        {
            P7t.Stop();
        }
        void P7z_MouseDown(object sender, MouseEventArgs e)
        {
            P7t.Start();
            P7 = (RESIZEPNL)sender;
        }
        void P5z_MouseUp(object sender, MouseEventArgs e)
        {
            P5t.Stop();
        }
        void P5z_MouseDown(object sender, MouseEventArgs e)
        {
            P5t.Start();
            P5 = (RESIZEPNL)sender;
        }
        #region resize panel Time-Events
        private void P5t_Tick(object sender, EventArgs e)
        {
            P5.Left = Cursor.Position.X - (Left + 150 + P5.Parent.Left + 5);
            P5.Parent.Width = P5.Left + 10;

            P8.Top = P7.Parent.Height - 10;
            P8.Left = P7.Parent.Width - 10;
            P7.Top = P7.Parent.Height - 10;
            P7.Left = P7.Parent.Width / 2 - 4;
            P7.Parent.Invalidate();
        }
        private void P7t_Tick(object sender, EventArgs e)
        {
            P7.Top = Cursor.Position.Y - (Top + 83 + P7.Parent.Top + 5);
            P7.Parent.Height = P7.Top + 10;
            P8.Top = P7.Parent.Height - 10;
            P8.Left = P7.Parent.Width - 10;
            P5.Top = P7.Parent.Height / 2 - 4;
            P5.Left = P7.Parent.Width - 10;
            P7.Parent.Refresh();
        }
        private void P8t_Tick(object sender, EventArgs e)
        {
            P8.Left = Cursor.Position.X - (Left + 150 + P8.Parent.Left + 5);
            P8.Top = Cursor.Position.Y - (Top + 83 + P8.Parent.Top + 5);
            P8.Parent.Width = P8.Left + 10;
            P8.Parent.Height = P8.Top + 10;
            P7.Top = P7.Parent.Height - 10;
            P7.Left = P7.Parent.Width / 2 - 4;
            P5.Top = P7.Parent.Height / 2 - 4;
            P5.Left = P7.Parent.Width - 10;
            P7.Parent.Refresh();
        }
        #endregion
        #endregion
        private void Objectz(string Type1)
        {
            Properties.Visible = true;
            #region Text
            if (Type1 == "Text")
            {
                LABEL = LOBJECT;
                LABEL.BorderStyle = BorderStyle.FixedSingle;
                SelTYPE.Text = "TextBox";
                Property2.Text = "Name";
                Property3.Text = "Text";
                Property4.Text = "X";
                Property5.Text = "Y";
                Property6.Text = "Text Color";
                Property7.Text = "";
                Property8.Text = "";
                Property9.Text = "";
                Properties.Height = (20 * 5) + 3;
                richTextBox2.Text = LABEL.Name;
                richTextBox3.Text = LABEL.Text;
                richTextBox4.Text = LABEL.Left.ToString();
                richTextBox5.Text = LABEL.Top.ToString();
                richTextBox6.Text = LABEL.ForeColor.ToKnownColor().ToString();
                portion = "Text";
                richTextBox6.Leave += new EventHandler(richTextBox6_Leave);
                richTextBox5.Leave += new EventHandler(richTextBox5_Leave);
                richTextBox4.Leave += new EventHandler(richTextBox4_Leave);
                richTextBox3.Leave += new EventHandler(richTextBox3_Leave);
                richTextBox2.Leave +=new EventHandler(richTextBox2_Leave);
            }
            #endregion
            #region Rectangle
            if (Type1 == "Rectangle")
            {
                RSEOBJECT.BorderStyle = BorderStyle.FixedSingle;
                SelTYPE.Text = "Rectangle";

                Properties.Height = (20 * 5) + 3;
                Property2.Text = "Name";
                Property3.Text = "X";
                Property4.Text = "Y";
                Property5.Text = "Width";
                Property6.Text = "Height";
                Property7.Text = "";
                Property8.Text = "";
                Property9.Text = "";
                richTextBox2.Text = RSEOBJECT.Name;
                richTextBox3.Text = RSEOBJECT.Left.ToString();
                richTextBox4.Text = RSEOBJECT.Top.ToString();
                richTextBox5.Text = RSEOBJECT.Width.ToString();
                richTextBox6.Text = RSEOBJECT.Height.ToString();
                portion = "Rect";
                richTextBox7.Leave += new EventHandler(richTextBox6_Leave);
                richTextBox6.Leave += new EventHandler(richTextBox5_Leave);
                richTextBox5.Leave += new EventHandler(richTextBox4_Leave);
                richTextBox4.Leave += new EventHandler(richTextBox3_Leave);
                richTextBox3.Leave += new EventHandler(richTextBox2_Leave);
            }
            #endregion
            #region Ellipse
            if (Type1 == "Ellipse")
            {
                RSEOBJECT.BorderStyle = BorderStyle.FixedSingle;
                SelTYPE.Text = "Ellipse";
                Property2.Text = "Name";
                Property3.Text = "X";
                Property4.Text = "Y";
                Property5.Text = "Width";
                Property6.Text = "Height";
                Property7.Text = "BackColor";
                Property8.Text = "Stroke";
                Property9.Text = "StrokeWidth";
                Properties.Height = (20 * 5) + 3;
                richTextBox3.Text = RSEOBJECT.Name;
                richTextBox4.Text = RSEOBJECT.Left.ToString();
                richTextBox5.Text = RSEOBJECT.Top.ToString();
                richTextBox6.Text = RSEOBJECT.Width.ToString();
                richTextBox7.Text = RSEOBJECT.Height.ToString();
                portion = "RSE";
                richTextBox7.Leave += new EventHandler(richTextBox6_Leave);
                richTextBox6.Leave += new EventHandler(richTextBox5_Leave);
                richTextBox5.Leave += new EventHandler(richTextBox4_Leave);
                richTextBox4.Leave += new EventHandler(richTextBox3_Leave);
                richTextBox3.Leave += new EventHandler(richTextBox2_Leave);
            }
            #endregion
            #region Image
            if (Type1 == "Image")
            {
                RSEOBJECT.BorderStyle = BorderStyle.FixedSingle;
                SelTYPE.Text = "Ellipse";
                Property2.Text = "Name";
                Property3.Text = "X";
                Property4.Text = "Y";
                Property5.Text = "Width";
                Property6.Text = "Height";
                Property7.Text = "Source";
                Property8.Text = "Stretch";
                //ComboBox C1 = MakeTempComboBox(richTextBox7);
                //C1.Items.AddRange("None", "Fill", "Uniform");
                Properties.Height = (20 * 5) + 3;
                richTextBox3.Text = RSEOBJECT.Name;
                richTextBox4.Text = RSEOBJECT.Left.ToString();
                richTextBox5.Text = RSEOBJECT.Top.ToString();
                richTextBox6.Text = RSEOBJECT.Width.ToString();
                richTextBox7.Text = RSEOBJECT.Height.ToString();
                portion = "RSE";
                //C1.SelectedIndexChanged += new EventHandler(C1_SelectedIndexChanged);
                richTextBox7.Leave += new EventHandler(richTextBox6_Leave);
                richTextBox6.Leave += new EventHandler(richTextBox5_Leave);
                richTextBox5.Leave += new EventHandler(richTextBox4_Leave);
                richTextBox4.Leave += new EventHandler(richTextBox3_Leave);
                richTextBox3.Leave += new EventHandler(richTextBox2_Leave);
            }
            #endregion
        }

        void C1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }
        #endregion
        #region CREATE ELLIPSE
        private void createEllipse()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SD));
            //RSEOBJECT.BorderStyle = BorderStyle.None;
            LOBJECT = null;
            MAINPANEL.Cursor = Cursors.Default;
            RSE zz = new RSE();
            zz.BackColor = Color.White;
            zz.BorderStyle = BorderStyle.None;
            zz.Cursor = Cursors.SizeAll;
            zz.Top = Cursor.Position.Y - (Top + 83);
            zz.Left = Cursor.Position.X - (Left + 150);
            zz.Name = "Ellipse" + zEcli;
            MAINPANEL.Controls.Add(zz);
            RSEOBJECT = zz;
            zz.type = "RSE";
            zEcli = zEcli + 1;
            zz.Paint += new PaintEventHandler(zz_PaintEcli);
            zz.MouseDown += new MouseEventHandler(zz_MouseDown);
            zz.MouseUp += new MouseEventHandler(zz_MouseUp);
            rectResizeEffect.Start();
            rectResizeEffect.Tick += new EventHandler(rectResizeEffect_Tick);
            zz.PreviewKeyDown += new PreviewKeyDownEventHandler(zz_PreviewKeyDown);
            zz.ContextMenuStrip = Element;
            zz.MouseMove +=new MouseEventHandler(zz_MouseMove);
            zz.Invalidate();
            StatusLabel.Text = "Release your mouse to draw the Ellipse.";
        }

        private void createEllipse1()
        {
            rectResizeEffect.Stop();
            RSE zz = RSEOBJECT;
            RSEOBJECT.Width = Cursor.Position.X - (Left + 145 + RSEOBJECT.Left); RSEOBJECT.Height = Cursor.Position.Y - (Top + 75 + RSEOBJECT.Top);
            EXAML.Text = EXAML.Text.Insert(EXAML.Text.IndexOf(">") + 1, NL + "<Ellipse Canvas.Top=\"" + zz.Top + "\" Canvas.Left=\"" + zz.Left + "\" Width=\"" + zz.Width + "\" Height=\"" + zz.Height + "\" Fill=\"LightYellow\" Stroke=\"Black\" StrokeThickness=\"1\" x:Name=\"" + zz.Name + "\" />");
            CreateResizePanels(zz);
            Objectz("Ellipse");
            StatusLabel.Text = "Ellipse has been drawn.";
        }
        #endregion
        #region CREATE Image
        private void createImage()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SD));
            //RSEOBJECT.BorderStyle = BorderStyle.None;
            LOBJECT = null;
            MAINPANEL.Cursor = Cursors.Default;
            RSE zz = new RSE();
            zz.BackColor = Color.LightYellow;
            zz.BorderStyle = BorderStyle.FixedSingle;
            zz.Cursor = Cursors.SizeAll;
            zz.Top = Cursor.Position.Y - (Top + 83);
            zz.Left = Cursor.Position.X - (Left + 150);
            MAINPANEL.Controls.Add(zz);
            RSEOBJECT = zz;
            zz.type = "RSE";
            zz.Type1 = "Image";
            zz.Name = "Image" + zImage;
            zRect = zRect + 1;
            zz.MouseDown += new MouseEventHandler(zz_MouseDown);
            zz.MouseUp += new MouseEventHandler(zz_MouseUp);
            rectResizeEffect.Start();
            rectResizeEffect.Tick += new EventHandler(rectResizeEffect_Tick);
            zz.ContextMenuStrip = Element;
            //zz.ObjectID = AddImage(zz);
            zz.MouseMove += new MouseEventHandler(zz_MouseMove);
            StatusLabel.Text = "Release your mouse, to draw the Image.";
        }
        private void createImage1()
        {
            rectResizeEffect.Stop();
            RSE zz = RSEOBJECT;
            RSEOBJECT.Width = Cursor.Position.X - (Left + 145 + RSEOBJECT.Left); RSEOBJECT.Height = Cursor.Position.Y - (Top + 75 + RSEOBJECT.Top);
            EXAML.Text = EXAML.Text.Insert(EXAML.Text.IndexOf(">") + 1, NL + "<Rectangle Canvas.Top=\"" + zz.Top + "\" Canvas.Left=\"" + zz.Left + "\" Width=\"" + zz.Width + "\" Height=\"" + zz.Height + "\" Fill=\"LightYellow\" Stroke=\"Black\" StrokeThickness=\"1\" x:Name=\"" + zz.Name + "\" />");
            mode = "none";
            CreateResizePanels(zz);
            Objectz("Image");
            zz.PreviewKeyDown += new PreviewKeyDownEventHandler(zz_PreviewKeyDown);
            StatusLabel.Text = "Image has been drawn.";
        }
        #endregion
        #region CREATE Media
        private void createMedia() { }
        #endregion //*
        #region CREATE StoryBoard
        private void createStoryBoard() { }
        #endregion //*
        #region CREATE Canvas
        private void createCanvas() { }
        #endregion //*
        #region CREATE RECTANGLE
        private void createRectangle()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SD));
            //RSEOBJECT.BorderStyle = BorderStyle.None;
            LOBJECT = null;
            MAINPANEL.Cursor = Cursors.Default;
            RSE zz = new RSE();
            zz.BackColor = Color.LightYellow;
            zz.BorderStyle = BorderStyle.FixedSingle;
            zz.Cursor = Cursors.SizeAll;
            zz.Top = Cursor.Position.Y - (Top + 83);
            zz.Left = Cursor.Position.X - (Left + 150);            
            MAINPANEL.Controls.Add(zz);
            RSEOBJECT = zz;
            zz.type = "RSE";
            zz.Type1 = "Rect";
            zz.Name = "Rect" + zRect;
            zRect = zRect + 1;
            zz.MouseDown += new MouseEventHandler(zz_MouseDown);
            zz.MouseUp += new MouseEventHandler(zz_MouseUp);
            rectResizeEffect.Start();
            rectResizeEffect.Tick += new EventHandler(rectResizeEffect_Tick);
            zz.ContextMenuStrip = Element;
            zz.ObjectID = AddRect(zz);
            zz.MouseMove += new MouseEventHandler(zz_MouseMove);
            StatusLabel.Text = "Release your mouse, to draw the rectangle.";
        }

        private void createRectangle1()
        {
            rectResizeEffect.Stop();
            RSE zz = RSEOBJECT;
            RSEOBJECT.Width = Cursor.Position.X - (Left + 145 + RSEOBJECT.Left); RSEOBJECT.Height = Cursor.Position.Y - (Top + 75 + RSEOBJECT.Top);
            EXAML.Text = EXAML.Text.Insert(EXAML.Text.IndexOf(">") + 1, NL + "<Rectangle Canvas.Top=\"" + zz.Top + "\" Canvas.Left=\"" + zz.Left + "\" Width=\"" + zz.Width + "\" Height=\"" + zz.Height + "\" Fill=\"LightYellow\" Stroke=\"Black\" StrokeThickness=\"1\" x:Name=\"" + zz.Name + "\" />");
            mode = "none";
            CreateResizePanels(zz);
            Objectz("Rectangle");
            zz.PreviewKeyDown += new PreviewKeyDownEventHandler(zz_PreviewKeyDown);
            StatusLabel.Text = "Rectangle has been drawn.";
        }
        #endregion
        #region CREATE TEXT
        private void createText()
        {
            //LOBJECT.BorderStyle = BorderStyle.None;
            RSEOBJECT = null;
            MAINPANEL.Cursor = Cursors.Default;
            LABEL1 zz = new LABEL1();
            zz.BorderStyle = BorderStyle.FixedSingle;
            zz.Cursor = Cursors.SizeAll;
            zz.Top = Cursor.Position.Y - (Top + 90);
            zz.Left = Cursor.Position.X - (Left + 167);
            zz.Text = "Text" + zText;
            zz.Name = "Text" + zText;
            MAINPANEL.Controls.Add(zz);
            zz.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
            LOBJECT = zz;
            zz.ForeColor = Color.Black;
            zz.type = "Text";
            Objectz("Text");
            zText = zText + 1;
            Label OBJECT = zz;
            zz.MouseDown += new MouseEventHandler(zz_MouseDown);
            EXAML.Text = EXAML.Text.Insert(EXAML.Text.IndexOf(">") + 1, NL + "<TextBlock FontSize=\"12\" FontFamily=\"Microsoft Sans Serif\" Canvas.Top=\"" + (Cursor.Position.Y - (Top + 82)) + "\" Canvas.Left=\"" + (Cursor.Position.X - (Left + 149)) + "\" x:Name=\"" + zz.Name + "\">" + zz.Text + "</TextBlock>");
            zz.AutoSize = true; mode = "none";
            zz.MouseDown += new MouseEventHandler(zz_MouseDown);
            zz.MouseUp += new MouseEventHandler(zz_MouseUp);
            zz.Focus();
            zz.PreviewKeyDown += new PreviewKeyDownEventHandler(zz_PreviewKeyDown);
            //zz.Leave += new EventHandler(zz_Leave);
            //zz.Enter += new EventHandler(zz_Enter);
            zz.ContextMenuStrip = Element;
            zz.ObjectID = AddLabel(zz);
            zz.MouseMove += new MouseEventHandler(zz_MouseMove);
            zz.Invalidate();
            StatusLabel.Text = "TextBlock has been drawn.";
        }
        #endregion

        //Need-Make: Canvas; StoryBoard; Image; Media;

        #region Misc
        private void Main_MouseMove(object sender, MouseEventArgs e)
        {
            if (UseFunctions == true)
            { StatusLabel.Text = (Cursor.Position.X - (Left + 149)) + ", " + (Cursor.Position.Y - (Top + 82)); }
        }

        private void vScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            if (UseFunctions == true)
            { Properties.Top = -vScrollBar1.Value * 5; }
        }

        
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (UseFunctions == true)
            {
                AboutBox1 zz = new AboutBox1();
                zz.ShowDialog();
            }
        }

        private void SD_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }


        private void button9_Click(object sender, EventArgs e)
        {
            if (UseFunctions == true)
            {
                string pz = createSilverlight.Text;
                string[] pz1 = textBox1.Text.Split(',');
                string pzz = pz.Replace("width:'" + originalSizeWidth + "'", "width:'" + pz1[0].ToString() + "'");
                string pzz1 = pzz.Replace("height:'" + originalSizeHeight + "'", "height:'" + pz1[1].ToString() + "'");
                MAINPANEL.Width = Convert.ToInt32(pz1[0]);
                MAINPANEL.Height = Convert.ToInt32(pz1[1]);
                originalSizeWidth = pz1[0].ToString();
                originalSizeHeight = pz1[1].ToString();
                createSilverlight.Text = pzz1;
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {

        }

        private void button11_Click(object sender, EventArgs e)
        {
            if (UseFunctions == true)
            {
                if (BC.ShowDialog() == DialogResult.OK)
                {
                    string pz = createSilverlight.Text;
                    string BCC = BC.Color.Name.ToLower();
                    string pzz = pz.Replace("background:'" + originalBC.ToString() + "'", "background:'" + BCC + "'");
                    originalBC = BCC;
                    createSilverlight.Text = pzz;
                    MAINPANEL.BackColor = BC.Color;
                }
            }
        }



        private void speedyViewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (UseFunctions == true)
            {
                string EDITEDXAML = "<Canvas xmlns=\"http://schemas.microsoft.com/client/2007\" xmlns:x=\"http://schemas.microsoft.com/winfx/2006/xaml\" Width=\"225\" Height=\"139\">"+EXAML.Text+"</Canvas>";
                SpeedyView SV = new SpeedyView(EDITEDXAML, EHTML.Text, EJS.Text, createSilverlight.Text);
                SV.ShowDialog();
                PropBoxStaNormal();
            }
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            if (UseFunctions == true)
            {
                Editor ED = new Editor(EHTML.Text, "html");
                ED.ShowDialog();
                Edit_Timer.Start();
                PropBoxStaNormal();
            }
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            if (UseFunctions == true)
            {
                Editor ED = new Editor(EJS.Text, "ejs");
                ED.ShowDialog();
                Edit_Timer.Start();
                PropBoxStaNormal();
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (UseFunctions == true)
            {
                Editor ED = new Editor(EXAML.Text, "xaml");
                ED.ShowDialog();
                Edit_Timer.Start();
                PropBoxStaNormal();
            }
        }

        private void Edit_Timer_Tick(object sender, EventArgs e)
        {if (Settings.Default.HTML != " ") { EHTML.Text = Settings.Default.HTML; Edit_Timer.Stop(); Settings.Default.HTML = " "; }
            if (Settings.Default.EJS != " ") { EJS.Text = Settings.Default.EJS; Edit_Timer.Stop(); Settings.Default.EJS = " "; }
            if (Settings.Default.XAML != " ") { EXAML.Text = Settings.Default.XAML; Edit_Timer.Stop(); Settings.Default.XAML = " "; }
        }

        private void silverlightnetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (UseFunctions == true)
            { System.Diagnostics.Process.Start("http://silverlight.net"); }
        }

        private void websiteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (UseFunctions == true)
            { System.Diagnostics.Process.Start("http://silverldesigner.sourceforge.net/"); }
        }
        private void viewCodeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (UseFunctions == true)
            {
                try
                {
                    if (LOBJECT.type == "Text")
                    {
                        Editor oo = new Editor(EXAML.Text, "xaml");
                        oo.gotoLineWithNAME(LOBJECT.Name);
                        oo.ShowDialog();
                        Edit_Timer.Start();
                    }
                }
                catch { }
                try
                {
                    if (RSEOBJECT.type == "RSE")
                    {
                        Editor oo = new Editor(EXAML.Text, "xaml");
                        oo.gotoLineWithNAME(RSEOBJECT.Name);
                        oo.ShowDialog();
                        Edit_Timer.Start();
                    }
                }
                catch { }
            }
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (UseFunctions == true)
            {
                try
                {
                    if (LOBJECT.type == "Text")
                    {
                        PropBoxStaNormal();
                        MAINPANEL.Controls.Remove(yy);
                    }
                }
                catch { }
                try
                {
                    if (RSEOBJECT.type == "RSE")
                    {
                        PropBoxStaNormal();
                        MAINPANEL.Controls.Remove(kk);
                    }
                }
                catch { }
            }
        }

        private void helpToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (UseFunctions == true)
            { Process.Start("SDhelp.chm"); }
        }

        private int AddRect(RSE RSE1) { return (int)EXAML.Lines.Length; EXAML.Text += "<Rectangle Canvas.Top=\"" + RSE1.Top + "\" Canvas.Top=\"" + RSE1.Left + " />\n"; }
        private int AddLabel(LABEL1 LABEL) { return (int)EXAML.Lines.Length-1; EXAML.Text += "<TextBlock Canvas.Top=\"" + LABEL.Top + "\" Canvas.Top=\"" + LABEL.Left + " />" + LABEL.Text + "</TextBlock>\n"; }
        #endregion

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void MAINPANEL_Resize(object sender, EventArgs e)
        {
            if (MAINPANEL.Height >= Main.Height - 17)
            {
                vScrollBar3.Enabled = true;
                vScrollBar3.Maximum = MAINPANEL.Height / 40;
            }
            else { vScrollBar3.Enabled = false; }

            if (MAINPANEL.Width >= Main.Width - 17)
            {
                hScrollBar1.Enabled = true;
                hScrollBar1.Maximum = MAINPANEL.Width / 40;
            }
            else { hScrollBar1.Enabled = false; }
        }

        private void vScrollBar3_Scroll(object sender, ScrollEventArgs e)
        {
            MAINPANEL.Top = -(vScrollBar3.Value*40);
        }

        private void hScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            MAINPANEL.Left = -(hScrollBar1.Value * 40);
        }

        private void Main_Paint(object sender, PaintEventArgs e)
        {
            Color[] Colors1 = new Color[]{Color.FromArgb(200, 231, 255),
                                Color.FromArgb(200, 231, 255),
                                Color.FromArgb(10, 100, 255)};

            ColorBlend mycolor = new ColorBlend();
            mycolor.Colors = Colors1;
            float[] Positions2 = { 0.0f, 0.5f, 1.0f };
            mycolor.Positions = Positions2;


            LinearGradientBrush linGrBrush = new LinearGradientBrush(
                new Point(Main.Width, 0),
                new Point(Main.Width, Main.Height),
                Color.FromArgb(10, 100, 255),
                Color.FromArgb(200, 231, 255));
            linGrBrush.InterpolationColors = mycolor;
            e.Graphics.FillRectangle(linGrBrush, 0, 0, Main.Width, Main.Height);
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            if (!(SBEOpened))
            {
                //SBE.Show();
                //SBEDataReceiver.Start();
                //StoryBoardEditor.Changed = false;
            }
        }

        private void SBEDataReceiver_Tick(object sender, EventArgs e)
        {
            if (SBESave == true)
            {
                SBESave = false;
                SBEDataReceiver.Stop();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            mode = "drawRectangle";
            MAINPANEL.Cursor = Cursors.Cross;
        }

        private void button1_MouseEnter(object sender, EventArgs e)
        {
            Button Sender = (Button)sender;
            Sender.FlatAppearance.BorderSize = 1;
            Sender.Paint += new PaintEventHandler(button1_Paint);
            Sender.Paint -= new PaintEventHandler(button1_Paint1);
        }

        private void button1_Paint1(object sender, PaintEventArgs e)
        {
            Button Sender = (Button)sender;

            string drawString = Sender.Text;
            System.Drawing.Font drawFont = new System.Drawing.Font("New Times Roman", 8, FontStyle.Regular);
            System.Drawing.SolidBrush drawBrush = new System.Drawing.SolidBrush(Color.Black);
            e.Graphics.DrawString(drawString, drawFont, drawBrush, Sender.Width / 2 - 27, Sender.Height / 2 - 6);
        }

        private void button1_Paint(object sender, PaintEventArgs e)
        {
            Button Sender = (Button)sender;
            
            LinearGradientBrush linGrBrush = new LinearGradientBrush(
                new Point(Sender.Width, 0),
                new Point(Sender.Width, Sender.Height),
                Color.FromArgb(240, 255, 192, 111),
                Color.FromArgb(240, 255, 213, 140));
            e.Graphics.FillRectangle(linGrBrush, 0, 0, Sender.Width, Sender.Height);

            string drawString = Sender.Text;
            System.Drawing.Font drawFont = new System.Drawing.Font("New Times Roman", 8, FontStyle.Regular);
            System.Drawing.SolidBrush drawBrush = new System.Drawing.SolidBrush(Color.Black);
            e.Graphics.DrawString(drawString, drawFont, drawBrush, Sender.Width / 2 - 27, Sender.Height / 2 - 6);
            Rectangle Rect1 = new Rectangle(new Point(5, 2), new Size(Sender.Height-4,Sender.Height-5));
            e.Graphics.DrawImage(Sender.Image, Rect1);
        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
            Button Sender = (Button)sender;
            Sender.FlatAppearance.BorderSize = 0;
            Sender.Paint -= new PaintEventHandler(button1_Paint);
            Sender.Paint += new PaintEventHandler(button1_Paint1);
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            mode = "drawRectangle";
            MAINPANEL.Cursor = Cursors.Cross;
        }

        private void button2_Click(object sender, EventArgs e)
        {
           //mode = "drawEllipse";
           //MAINPANEL.Cursor = Cursors.Cross;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            mode = "drawText";
            MAINPANEL.Cursor = Cursors.Cross;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //mode = "drawImage";
            //MAINPANEL.Cursor = Cursors.Cross;
        }
    }
    #region Custom Classes

    public class CANVASOBJECT : Panel
    {
        public string background;
        public RESIZEPNL P1;
        public RESIZEPNL P2;
        public RESIZEPNL P3;
        public RESIZEPNL P4;
        public RESIZEPNL P5;
        public RESIZEPNL P6;
        public RESIZEPNL P7;
        public RESIZEPNL P8;
        public int ObjectID;
    }
    public class RSE : Panel
    {
        public string type;
        public string Type1;
        public Color Fill;
        public Color Stroke;
        public Color StrokeThickness;
        public int LastX;
        public int LastY;
        public int LastWidth;
        public int LastHeight;
        public RESIZEPNL P1;
        public RESIZEPNL P2;
        public RESIZEPNL P3;
        public RESIZEPNL P4;
        public RESIZEPNL P5;
        public RESIZEPNL P6;
        public RESIZEPNL P7;
        public RESIZEPNL P8;
        public int ObjectID;
    }
    public class LABEL1 : Label
    {
        public string type = "Text";
        public string Type1 = "TBlock";
        public int ObjectID;
    }
    public class RESIZEPNL : PictureBox
    {
    }
    #endregion
}