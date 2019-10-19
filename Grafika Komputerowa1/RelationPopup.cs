using Grafika_Komputerowa1.Constans;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Grafika_Komputerowa1
{
    public partial class RelationPopup : Form
    {
        RelationEnum relation;
        public RelationPopup()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            relation = RelationEnum.None;
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            relation = RelationEnum.Equal;
            Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            relation = RelationEnum.Perpendicular;
            Close();
        }

        public RelationEnum GetChoosenRelation()
        {
            return relation;
        }
    }
}
