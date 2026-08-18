using System;
using System.Collections;
using System.Collections.Specialized;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.CodeDom.Compiler
{
	// Token: 0x02000672 RID: 1650
	[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
	[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
	public class CodeGeneratorOptions
	{
		// Token: 0x17000E6B RID: 3691
		public object this[string index]
		{
			get
			{
				return this.options[index];
			}
			set
			{
				this.options[index] = value;
			}
		}

		// Token: 0x17000E6C RID: 3692
		// (get) Token: 0x06003C69 RID: 15465 RVA: 0x000F980C File Offset: 0x000F7A0C
		// (set) Token: 0x06003C6A RID: 15466 RVA: 0x000F9839 File Offset: 0x000F7A39
		public string IndentString
		{
			get
			{
				object obj = this.options["IndentString"];
				if (obj != null)
				{
					return (string)obj;
				}
				return "    ";
			}
			set
			{
				this.options["IndentString"] = value;
			}
		}

		// Token: 0x17000E6D RID: 3693
		// (get) Token: 0x06003C6B RID: 15467 RVA: 0x000F984C File Offset: 0x000F7A4C
		// (set) Token: 0x06003C6C RID: 15468 RVA: 0x000F9879 File Offset: 0x000F7A79
		public string BracingStyle
		{
			get
			{
				object obj = this.options["BracingStyle"];
				if (obj != null)
				{
					return (string)obj;
				}
				return "Block";
			}
			set
			{
				this.options["BracingStyle"] = value;
			}
		}

		// Token: 0x17000E6E RID: 3694
		// (get) Token: 0x06003C6D RID: 15469 RVA: 0x000F988C File Offset: 0x000F7A8C
		// (set) Token: 0x06003C6E RID: 15470 RVA: 0x000F98B5 File Offset: 0x000F7AB5
		public bool ElseOnClosing
		{
			get
			{
				object obj = this.options["ElseOnClosing"];
				return obj != null && (bool)obj;
			}
			set
			{
				this.options["ElseOnClosing"] = value;
			}
		}

		// Token: 0x17000E6F RID: 3695
		// (get) Token: 0x06003C6F RID: 15471 RVA: 0x000F98D0 File Offset: 0x000F7AD0
		// (set) Token: 0x06003C70 RID: 15472 RVA: 0x000F98F9 File Offset: 0x000F7AF9
		public bool BlankLinesBetweenMembers
		{
			get
			{
				object obj = this.options["BlankLinesBetweenMembers"];
				return obj == null || (bool)obj;
			}
			set
			{
				this.options["BlankLinesBetweenMembers"] = value;
			}
		}

		// Token: 0x17000E70 RID: 3696
		// (get) Token: 0x06003C71 RID: 15473 RVA: 0x000F9914 File Offset: 0x000F7B14
		// (set) Token: 0x06003C72 RID: 15474 RVA: 0x000F993D File Offset: 0x000F7B3D
		[ComVisible(false)]
		public bool VerbatimOrder
		{
			get
			{
				object obj = this.options["VerbatimOrder"];
				return obj != null && (bool)obj;
			}
			set
			{
				this.options["VerbatimOrder"] = value;
			}
		}

		// Token: 0x04002C75 RID: 11381
		private IDictionary options = new ListDictionary();
	}
}
