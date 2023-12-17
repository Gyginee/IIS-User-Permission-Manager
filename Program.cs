using System;
using System.IO;
using System.Security.AccessControl;
using System.Windows.Forms;

class Program : Form
{
    private FileSystemWatcher watcher;
    private NotifyIcon trayIcon;
    private ListBox messageList;
    private Button clearButton;
    private FolderBrowserDialog folderBrowserDialog;

    public Program()
    {
        InitializeComponent();
        InitializeFormComponents();
        SetupFileSystemWatcher();
    }

    private void InitializeComponent()
    {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Program));
            this.SuspendLayout();
            // 
            // Program
            // 
            resources.ApplyResources(this, "$this");
            this.Name = "Program";
            this.Load += new System.EventHandler(this.Form_Load);
            this.ResumeLayout(false);

    }

    private void InitializeFormComponents()
    {
        this.Size = new System.Drawing.Size(400, 300);

        // Setup tray icon
        trayIcon = new NotifyIcon
        {
            Icon = new Icon(@"Icon.ico"),
            Visible = true
        };
        trayIcon.DoubleClick += (sender, args) =>
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
        };

        // Setup message list
        messageList = new ListBox
        {
            Dock = DockStyle.Fill
        };
        this.Controls.Add(messageList);

        // Setup clear button
        clearButton = new Button
        {
            Text = "Clear Messages",
            Dock = DockStyle.Bottom
        };
        clearButton.Click += (sender, args) => messageList.Items.Clear();
        this.Controls.Add(clearButton);

        // Setup folder browser dialog
        folderBrowserDialog = new FolderBrowserDialog();
    }

    private void Form_Load(object sender, EventArgs e)
    {
        // Form Load actions can be handled here if needed
    }

    private void SetupFileSystemWatcher()
    {
        string path = @"C:\DEPLOY"; // Default path

        if (!Directory.Exists(path))
        {
            MessageBox.Show("Default directory not found. Please select a directory.","NOT FOUND", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //AppendMessage("Default directory not found. Please select a directory.");
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                path = folderBrowserDialog.SelectedPath;
                this.BringWindowToFront(); // Bring the form to the front after folder selection
            }
            else
            {
                AppendMessage("No directory selected. File watcher not started.");
                return;
            }
        }

        watcher = new FileSystemWatcher
        {
            Path = path,
            IncludeSubdirectories = true,
            NotifyFilter = NotifyFilters.FileName,
            Filter = "*.*"
        };

        watcher.Created += OnCreated;
        watcher.EnableRaisingEvents = true;

        AppendMessage($"Watching directory: {path}");
    }


    private void OnCreated(object source, FileSystemEventArgs e)
    {
        try
        {
            if (File.Exists(e.FullPath))
            {
                UpdateFileAccess(e.FullPath);
            }
        }
        catch (Exception ex)
        {
            AppendMessage("Error updating permissions: " + ex.Message);
        }
    }


    private void UpdateFileAccess(string filePath)
    {
        FileInfo fileInfo = new FileInfo(filePath);
        FileSecurity fileSecurity = fileInfo.GetAccessControl();
        FileSystemAccessRule accessRule = new FileSystemAccessRule("IIS_IUSRS", FileSystemRights.FullControl, AccessControlType.Allow);

        fileSecurity.AddAccessRule(accessRule);
        fileInfo.SetAccessControl(fileSecurity);

        AppendMessage($"Permissions updated for file: {fileInfo.Name}");
    }

    private void AppendMessage(string message)
    {
        if (this.InvokeRequired)
        {
            this.Invoke(new Action(() => messageList.Items.Add(message)));
        }
        else
        {
            messageList.Items.Add(message);
        }
    }

    protected override void OnResize(EventArgs e)
    {
        base.OnResize(e);
        if (this.WindowState == FormWindowState.Minimized)
        {
            this.Hide();
        }
    }
    private void BringWindowToFront()
    {
        // Check if we are running on the UI thread
        if (this.InvokeRequired)
        {
            this.Invoke(new Action(BringWindowToFront));
            return;
        }

        this.TopMost = true; // Make the form topmost
        this.Focus(); // Focus on the form
        this.BringToFront(); // Bring form to the front
        this.TopMost = false; // Set back TopMost to false
    }
    [STAThread]
    static void Main(string[] args)
    {
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        Application.Run(new Program());
    }
}