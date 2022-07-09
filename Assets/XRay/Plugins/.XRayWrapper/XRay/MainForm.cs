using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using XRay.Core;
using XRay.Core.Enums;

namespace XRay
{
    public partial class MainForm : Form
    {
        public static MainForm Instance { get; private set; }
        public MainForm()
        {
            InitializeComponent();
            Instance = this;
            Console.Log("Initialized...");
        }


        xr_object x = new xr_object();
        xr_ogf ogf = new xr_ogf();

        void BuildTree(TreeNode node, xr_bone bone)
        {
            List<xr_bone> childrens = bone.m_children;
            
            if(childrens.Count > 0)
            {
                for(int i = 0; i < childrens.Count; i++)
                {
                    TreeNode child_node = new TreeNode(childrens[i].m_name);
                    node.Nodes.Add(child_node);
                    BuildTree(child_node, childrens[i]);
                }
            }
        }

        private void MainForm_Load(object sender, System.EventArgs e)
        {
            openFileDialog1.Filter = "OGF Model (*.ogf)|*.ogf|Object (*.object)|*.object";
            openFileDialog1.FileOk += delegate
            {
                if (x.Load(openFileDialog1.FileName))
                {
                    Console.Log("owner_name = "+x.owner_name);
                    Console.Log("creating_time = "+ new DateTime(1970, 1, 1).AddSeconds(x.creating_time));
                    Console.Log("modified_time = "+ new DateTime(1970, 1, 1).AddSeconds(x.modified_time));

                    //build tree
                    if (x.root_bone != null)
                    {
                        TreeNode node = new TreeNode(x.root_bone.m_name);
                        BuildTree(
                            node,
                            x.root_bone);

                        BonesTree.Nodes.Add(node);
                    }

                    if(x.mesh[0].GetChunk((int)(emesh_chunk_id.EMESH_CHUNK_VMREFS), out xr_chunk chunk0))
                    {
                        int vmrefs = chunk0.reader.ReadInt32();

                        Console.Log("VMRefs: "+ vmrefs);

                        for(int i =0;i < vmrefs; i++)
                        {
                            Console.Log("r8:"+chunk0.reader.ReadByte()+" vmap: " + chunk0.reader.ReadInt32()+" offset: "+chunk0.reader.ReadInt32());
                        }
                    }
                }
            };

            DialogResult result = openFileDialog1.ShowDialog();

            if(result == DialogResult.Cancel || result == DialogResult.Abort || result == DialogResult.No)
            {
                Application.Exit();
            }
        }

        private void DumpChunks_Click(object sender, System.EventArgs e)
        {
            x.DumpChunks();
        }
    }
}
