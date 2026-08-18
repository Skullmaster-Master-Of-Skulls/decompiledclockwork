using System;

namespace ClockWorkWebAPI.Templates
{
	// Token: 0x02000048 RID: 72
	public class Template
	{
		// Token: 0x06000390 RID: 912 RVA: 0x000199D8 File Offset: 0x00017BD8
		public Template(string text, NameObjectPairCollection args, db conn)
		{
			this.text = text;
			this.args = args;
			this.subject = "";
		}

		// Token: 0x06000391 RID: 913 RVA: 0x000199FB File Offset: 0x00017BFB
		public Template(string subject, string text, NameObjectPairCollection args, db conn)
		{
			this.text = text;
			this.args = args;
			this.subject = subject;
		}

		// Token: 0x06000392 RID: 914 RVA: 0x000199D8 File Offset: 0x00017BD8
		public Template(string text, NameObjectPairCollection args)
		{
			this.text = text;
			this.args = args;
			this.subject = "";
		}

		// Token: 0x06000393 RID: 915 RVA: 0x000199FB File Offset: 0x00017BFB
		public Template(string subject, string text, NameObjectPairCollection args)
		{
			this.text = text;
			this.args = args;
			this.subject = subject;
		}

		// Token: 0x06000394 RID: 916 RVA: 0x00019A1A File Offset: 0x00017C1A
		public void MergeMail(out string Subject, out string Text)
		{
			Subject = this.Merge(this.subject);
			Text = this.Merge(this.text);
		}

		// Token: 0x06000395 RID: 917 RVA: 0x00019A3C File Offset: 0x00017C3C
		public string Merge()
		{
			return this.Merge(this.text);
		}

		// Token: 0x06000396 RID: 918 RVA: 0x00019A5C File Offset: 0x00017C5C
		private string Merge(string template)
		{
			string text = template;
			foreach (object obj in this.args)
			{
				NameObjectPair nameObjectPair = (NameObjectPair)obj;
				text = text.Replace(nameObjectPair.Name, nameObjectPair.Value.ToString());
			}
			return text;
		}

		// Token: 0x040001CC RID: 460
		private string subject;

		// Token: 0x040001CD RID: 461
		private string text;

		// Token: 0x040001CE RID: 462
		private int studentPid;

		// Token: 0x040001CF RID: 463
		private int instructorPid;

		// Token: 0x040001D0 RID: 464
		private int luCourseId;

		// Token: 0x040001D1 RID: 465
		private int notetakerId;

		// Token: 0x040001D2 RID: 466
		private NameObjectPairCollection args;
	}
}
