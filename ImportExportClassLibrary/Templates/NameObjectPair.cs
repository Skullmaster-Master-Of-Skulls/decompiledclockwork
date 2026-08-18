using System;

namespace ImportExportClassLibrary.Templates
{
	// Token: 0x0200003B RID: 59
	public class NameObjectPair
	{
		// Token: 0x0600020D RID: 525 RVA: 0x00015FF0 File Offset: 0x00014FF0
		public NameObjectPair(string name, object obj)
		{
			this.Name = name;
			this.Obj = obj;
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x0600020E RID: 526 RVA: 0x00016006 File Offset: 0x00015006
		// (set) Token: 0x0600020F RID: 527 RVA: 0x0001600E File Offset: 0x0001500E
		public string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				this.name = value;
				this.nameLCase = value.ToLower();
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000210 RID: 528 RVA: 0x00016023 File Offset: 0x00015023
		// (set) Token: 0x06000211 RID: 529 RVA: 0x0001602B File Offset: 0x0001502B
		public object Obj
		{
			get
			{
				return this.obj;
			}
			set
			{
				this.obj = value;
			}
		}

		// Token: 0x06000212 RID: 530 RVA: 0x00016034 File Offset: 0x00015034
		public bool MatchesWith(string nameToMatch)
		{
			return nameToMatch.ToLower().CompareTo(this.nameLCase) == 0;
		}

		// Token: 0x04000108 RID: 264
		private string nameLCase;

		// Token: 0x04000109 RID: 265
		private string name;

		// Token: 0x0400010A RID: 266
		private object obj;
	}
}
