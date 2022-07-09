using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using XRay.Core;

namespace XRay
{
    public partial class MainForm : Form
    {
        public static MainForm Instance { get; private set; }
        static bool xrCoreFound = false;
        public MainForm()
        {
            InitializeComponent();
            Instance = this;
            Console.Log("Initialized...");

            try
            {
                Console.Log(Wrapper.get_version().GetString());
                xrCoreFound = true;
            }
            catch
            {
                xrCoreFound = false;
                Console.Error("xrCore not found!");
            }
        }

        void BuildTree(TreeNode node, xr_bone bone)
        {
            List<xr_bone> childrens = bone.children;

            if (childrens.Count > 0)
            {
                for (int i = 0; i < childrens.Count; i++)
                {
                    TreeNode child_node = new TreeNode(childrens[i].name);
                    node.Nodes.Add(child_node);
                    BuildTree(child_node, childrens[i]);
                }
            }
        }

        xr_object b;
        private void MainForm_Load(object sender, EventArgs e)
        {
            if (!xrCoreFound)
                return;

            openFileDialog1.Filter = "All (*.*)|*.*|OGF Model (*.ogf)|*.ogf|Object (*.object)|*.object";
            openFileDialog1.FileOk += delegate
            {
                if (Path.GetExtension(openFileDialog1.FileName).ToLower() == ".object")
                {
                    if (xr_object.Load(openFileDialog1.FileName, out xr_object _object))
                    {
                        b = _object;
                        if (_object.root_bone != null)
                        {
                            Console.Log("Building visual tree...");
                            TreeNode node = new TreeNode(_object.root_bone.name);
                            BuildTree(
                                node,
                                _object.root_bone);

                            BonesTree.Nodes.Add(node);
                        }
                        Console.Log("Complete!");
                    }
                    else
                    {
                        Console.Error("Error while loading object file...");
                    }
                }
                else if (Path.GetExtension(openFileDialog1.FileName).ToLower() == ".ogf")
                {
                    if(xr_ogf.Load(openFileDialog1.FileName, out xr_ogf ogf))
                    {
                        Console.Log("OGF Version is "+ogf.version);
                        Console.Log("OGF Model Type is "+ogf.model_type);
                    }
                    else
                    {
                        Console.Error("Error while loading ogf file...");
                    }
                }
            };

            DialogResult result = openFileDialog1.ShowDialog();

            if(result == DialogResult.Cancel || result == DialogResult.Abort || result == DialogResult.No)
            {
                Application.Exit();
            }
        }
    }
}
