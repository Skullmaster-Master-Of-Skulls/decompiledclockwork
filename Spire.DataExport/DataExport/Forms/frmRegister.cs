using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Spire.DataExport.CollectionEditors;
using Spire.DataExport.Utils;

namespace Spire.DataExport.Forms
{
	// Token: 0x0200019F RID: 415
	public partial class frmRegister : Form
	{
		// Token: 0x06000B5F RID: 2911 RVA: 0x00077D44 File Offset: 0x00076D44
		public frmRegister()
		{
			this.ᜀ();
		}

		// Token: 0x06000B61 RID: 2913 RVA: 0x00077E08 File Offset: 0x00076E08
		private void ᜀ()
		{
			int a_ = 5;
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			this.ᜀ = new Label();
			this.ᜁ = new Label();
			this.ᜂ = new TextBox();
			this.ᜃ = new Label();
			this.ᜇ = new TextBox();
			this.ᜆ = new sprᯅ();
			this.ᜄ = new sprᯅ();
			this.ᜅ = new GroupBox();
			base.SuspendLayout();
			this.ᜀ.Location = new Point(8, 8);
			this.ᜀ.Name = HyperlinksCollectionEditor.b("䴠䄢䤤甦䰨䰪䐬尮䔰嘲䜴", a_);
			this.ᜀ.Size = new Size(384, 48);
			this.ᜀ.TabIndex = 0;
			this.ᜀ.Text = HyperlinksCollectionEditor.b("焠伢䀤䘦娨个ബ䨮弰䜲倴䔶ᤸ䈺刼䨾㍀捂㝄≆⹈≊㹌㭎⍐㉒⅔㹖㙘㕚絜㹞འݢ䕤୦hࡪ࡬Ůɰᙲ啴፶ᱸེᱼᙾꖄ놐杖랖힠슢톤슦覨\udfaa얬쪮醰\udfb2\udcb4풶\udcb8햺캼\udabeꗀ도ꋆ믈룊꓌ꃎ뿐﷒雔믖냘룚뛜￞苠賢诤鏦胨藪飬諮퇰蟲髴ퟶ賸裺飼\udffe甀欂怄✆䰈紊氌挎搐爒愔縖瘘甚㴜椞䐠儢嘤並䘨䔪̬", a_);
			this.ᜁ.Location = new Point(32, 68);
			this.ᜁ.Name = HyperlinksCollectionEditor.b("䴠䄢䤤爦娨个弬愮倰帲倴", a_);
			this.ᜁ.Size = new Size(61, 16);
			this.ᜁ.TabIndex = 1;
			this.ᜁ.Text = HyperlinksCollectionEditor.b("琠倢䀤唦न攪䰬䈮吰ल", a_);
			this.ᜂ.Location = new Point(104, 64);
			this.ᜂ.MaxLength = 255;
			this.ᜂ.Name = HyperlinksCollectionEditor.b("唠嬢儤爦娨个弬愮倰帲倴", a_);
			this.ᜂ.Size = new Size(192, 21);
			this.ᜂ.TabIndex = 2;
			this.ᜂ.Text = "";
			this.ᜂ.TextChanged += this.ᜁ;
			this.ᜃ.Location = new Point(16, 96);
			this.ᜃ.Name = HyperlinksCollectionEditor.b("䴠䄢䤤琦木", a_);
			this.ᜃ.Size = new Size(88, 23);
			this.ᜃ.TabIndex = 3;
			this.ᜃ.Text = HyperlinksCollectionEditor.b("洠䨢䘤䈦䜨堪䠬༮爰尲儴制̸", a_);
			this.ᜇ.Location = new Point(104, 88);
			this.ᜇ.MaxLength = 255;
			this.ᜇ.Name = HyperlinksCollectionEditor.b("唠嬢儤琦木", a_);
			this.ᜇ.Size = new Size(192, 21);
			this.ᜇ.TabIndex = 2;
			this.ᜇ.Text = "";
			this.ᜇ.TextChanged += this.ᜁ;
			this.ᜆ.ᜀ(new Point(0, 0));
			this.ᜆ.ᜀ(emunType.BtnShape.Rectangle);
			this.ᜆ.ᜀ(emunType.XPStyle.Default);
			this.ᜆ.DialogResult = DialogResult.OK;
			this.ᜆ.Enabled = false;
			this.ᜆ.Location = new Point(200, 144);
			this.ᜆ.Name = HyperlinksCollectionEditor.b("䌠圢䬤栦戨", a_);
			this.ᜆ.TabIndex = 4;
			this.ᜆ.Text = HyperlinksCollectionEditor.b("猠䘢䈤並娨弪䠬崮", a_);
			this.ᜆ.Click += this.ᜀ;
			this.ᜄ.ᜀ(new Point(0, 0));
			this.ᜄ.ᜀ(emunType.BtnShape.Rectangle);
			this.ᜄ.ᜀ(emunType.XPStyle.Default);
			this.ᜄ.DialogResult = DialogResult.Cancel;
			this.ᜄ.Location = new Point(280, 144);
			this.ᜄ.Name = HyperlinksCollectionEditor.b("䌠圢䬤搦䠨䔪丬䨮崰", a_);
			this.ᜄ.TabIndex = 4;
			this.ᜄ.Text = HyperlinksCollectionEditor.b("戠䰢䬤匦䀨䔪堬䨮", a_);
			this.ᜅ.Location = new Point(0, 120);
			this.ᜅ.Name = HyperlinksCollectionEditor.b("䘠儢䜤欦䀨䔪䠬", a_);
			this.ᜅ.Size = new Size(432, 8);
			this.ᜅ.TabIndex = 5;
			this.ᜅ.TabStop = false;
			this.AutoScaleBaseSize = new Size(6, 14);
			base.ClientSize = new Size(392, 182);
			base.Controls.Add(this.ᜅ);
			base.Controls.Add(this.ᜆ);
			base.Controls.Add(this.ᜃ);
			base.Controls.Add(this.ᜂ);
			base.Controls.Add(this.ᜁ);
			base.Controls.Add(this.ᜀ);
			base.Controls.Add(this.ᜇ);
			base.Controls.Add(this.ᜄ);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = HyperlinksCollectionEditor.b("䜠儢䠤甦䰨䰪䐬尮䔰嘲䜴", a_);
			base.ShowInTaskbar = false;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = HyperlinksCollectionEditor.b("猠䘢䈤並娨弪䠬崮", a_);
			base.ResumeLayout(false);
		}

		// Token: 0x06000B62 RID: 2914 RVA: 0x00078388 File Offset: 0x00077388
		private void ᜁ(object A_0, EventArgs A_1)
		{
			if (true)
			{
			}
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			this.ᜆ.Enabled = (this.ᜂ.Text.Trim().Length > 0 && this.ᜄ.Text.Trim().Length > 0);
		}

		// Token: 0x06000B63 RID: 2915 RVA: 0x00078408 File Offset: 0x00077408
		private void ᜀ(object A_0, EventArgs A_1)
		{
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			if (true)
			{
			}
			spr\u2561.ᜃ(this.ᜂ.Text, this.ᜇ.Text);
		}

		// Token: 0x040008A9 RID: 2217
		private Label ᜀ;

		// Token: 0x040008AA RID: 2218
		private byte[] \u2609\u00A7\u0080\u0085;

		// Token: 0x040008AB RID: 2219
		private Label ᜁ;

		// Token: 0x040008AC RID: 2220
		private TextBox ᜂ;

		// Token: 0x040008AD RID: 2221
		private long[] \u2460\u0083\u0094\u0084;

		// Token: 0x040008AE RID: 2222
		private Label ᜃ;

		// Token: 0x040008AF RID: 2223
		private int \u2460\u009B\u00A9\u0099;

		// Token: 0x040008B0 RID: 2224
		private sprᯅ ᜄ;

		// Token: 0x040008B1 RID: 2225
		private GroupBox ᜅ;

		// Token: 0x040008B2 RID: 2226
		private sprᯅ ᜆ;

		// Token: 0x040008B3 RID: 2227
		private TextBox ᜇ;

		// Token: 0x040008B4 RID: 2228
		private bool \u2609\u008A\u007F\u00A2;

		// Token: 0x040008B5 RID: 2229
		private long \u2593\u00AD\u008B\u009C;

		// Token: 0x040008B6 RID: 2230
		private bool \u25D9\u008A\u0084\u0096;
	}
}
