using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO; // Needed for file and folder operations
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Windows_Temp_Cleaner
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            // No action needed here unless you want live behavior.
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            // No action needed here unless you want live behavior.
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            // No action needed here unless you want live behavior.
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            // You can define this one too if you want.
        }

       

        private void DeleteFilesInFolder(string folderPath)
        {
            if (Directory.Exists(folderPath))
            {
                foreach (string file in Directory.GetFiles(folderPath))
                {
                    try
                    {
                        File.Delete(file);
                    }
                    catch (Exception) { /* You can log errors if you want */ }
                }

                foreach (string dir in Directory.GetDirectories(folderPath))
                {
                    try
                    {
                        Directory.Delete(dir, true);
                    }
                    catch (Exception) { /* You can log errors if you want */ }
                }
            }
        }
        private void CleanCinema4DCache()
        {
            try
            {
                string roamingAppData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                string localAppData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

                // 1. Clean C4D Temp folder
                string c4dTempPath = Path.Combine(localAppData, "Temp", "MAXON");
                if (Directory.Exists(c4dTempPath))
                {
                    DeleteFilesInFolder(c4dTempPath);
                }

                // 2. Search for Cinema 4D versions in Roaming\MAXON\
                string maxonRoamingPath = Path.Combine(roamingAppData, "MAXON");

                if (Directory.Exists(maxonRoamingPath))
                {
                    var cinema4DFolders = Directory.GetDirectories(maxonRoamingPath)
                                                   .Where(dir => dir.Contains("2023") || dir.Contains("2024") || dir.Contains("R24"))
                                                   .ToList();

                    foreach (string c4dVersionFolder in cinema4DFolders)
                    {
                        // Clean _bugreports
                        string bugReportsPath = Path.Combine(c4dVersionFolder, "_bugreports");
                        if (Directory.Exists(bugReportsPath))
                        {
                            DeleteFilesInFolder(bugReportsPath);
                        }

                        // Clean _prefs\previewimages
                        string previewImagesPath = Path.Combine(c4dVersionFolder, "_prefs", "previewimages");
                        if (Directory.Exists(previewImagesPath))
                        {
                            DeleteFilesInFolder(previewImagesPath);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error cleaning Cinema 4D cache: " + ex.Message);
            }
        }
        private void guna2Button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (checkBox1.Checked) // Prefetch
                {
                    DeleteFilesInFolder(@"C:\Windows\Prefetch");
                }

                if (checkBox2.Checked) // Temp
                {
                    DeleteFilesInFolder(@"C:\Windows\Temp");
                }

                if (checkBox3.Checked) // User Local Temp
                {
                    string userTempPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Temp");
                    DeleteFilesInFolder(userTempPath);
                }

                if (checkBox4.Checked) // Junk Files
                {
                    DeleteFilesInFolder(@"C:\Windows\SoftwareDistribution\Download");

                    string inetCachePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), @"Microsoft\Windows\INetCache");
                    DeleteFilesInFolder(inetCachePath);

                    string windowsCachesPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), @"Microsoft\Windows\Caches");
                    DeleteFilesInFolder(windowsCachesPath);

                    string crashDumpsPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "CrashDumps");
                    DeleteFilesInFolder(crashDumpsPath);

                    DeleteFilesInFolder(@"C:\$Recycle.Bin"); // Optional
                }
                if (checkBox5.Checked) // Cinema 4D cache
                {
                    CleanCinema4DCache();
                }
                if (checkBox6.Checked) // Redshift Cache
                {
                    string redshiftFolderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Redshift");
                    DeleteFilesInFolder(redshiftFolderPath);
                }
                if (checkBox7.Checked)
                {
                    string redshiftFolderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Redshift");
                    DeleteFilesInFolder(redshiftFolderPath);
                }
                MessageBox.Show("Selected directories cleaned successfully!", "Cleanup Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error during cleanup: " + ex.Message);
            }
        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox6_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox7_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
