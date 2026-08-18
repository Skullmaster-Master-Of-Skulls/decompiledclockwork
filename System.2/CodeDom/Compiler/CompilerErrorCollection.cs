using System;
using System.Collections;
using System.Security.Permissions;

namespace System.CodeDom.Compiler
{
	// Token: 0x02000676 RID: 1654
	[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
	[Serializable]
	public class CompilerErrorCollection : CollectionBase
	{
		// Token: 0x06003CDB RID: 15579 RVA: 0x000FB151 File Offset: 0x000F9351
		public CompilerErrorCollection()
		{
		}

		// Token: 0x06003CDC RID: 15580 RVA: 0x000FB159 File Offset: 0x000F9359
		public CompilerErrorCollection(CompilerErrorCollection value)
		{
			this.AddRange(value);
		}

		// Token: 0x06003CDD RID: 15581 RVA: 0x000FB168 File Offset: 0x000F9368
		public CompilerErrorCollection(CompilerError[] value)
		{
			this.AddRange(value);
		}

		// Token: 0x17000E7A RID: 3706
		public CompilerError this[int index]
		{
			get
			{
				return (CompilerError)base.List[index];
			}
			set
			{
				base.List[index] = value;
			}
		}

		// Token: 0x06003CE0 RID: 15584 RVA: 0x000FB199 File Offset: 0x000F9399
		public int Add(CompilerError value)
		{
			return base.List.Add(value);
		}

		// Token: 0x06003CE1 RID: 15585 RVA: 0x000FB1A8 File Offset: 0x000F93A8
		public void AddRange(CompilerError[] value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			for (int i = 0; i < value.Length; i++)
			{
				this.Add(value[i]);
			}
		}

		// Token: 0x06003CE2 RID: 15586 RVA: 0x000FB1DC File Offset: 0x000F93DC
		public void AddRange(CompilerErrorCollection value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			int count = value.Count;
			for (int i = 0; i < count; i++)
			{
				this.Add(value[i]);
			}
		}

		// Token: 0x06003CE3 RID: 15587 RVA: 0x000FB218 File Offset: 0x000F9418
		public bool Contains(CompilerError value)
		{
			return base.List.Contains(value);
		}

		// Token: 0x06003CE4 RID: 15588 RVA: 0x000FB226 File Offset: 0x000F9426
		public void CopyTo(CompilerError[] array, int index)
		{
			base.List.CopyTo(array, index);
		}

		// Token: 0x17000E7B RID: 3707
		// (get) Token: 0x06003CE5 RID: 15589 RVA: 0x000FB238 File Offset: 0x000F9438
		public bool HasErrors
		{
			get
			{
				if (base.Count > 0)
				{
					foreach (object obj in this)
					{
						CompilerError compilerError = (CompilerError)obj;
						if (!compilerError.IsWarning)
						{
							return true;
						}
					}
					return false;
				}
				return false;
			}
		}

		// Token: 0x17000E7C RID: 3708
		// (get) Token: 0x06003CE6 RID: 15590 RVA: 0x000FB2A0 File Offset: 0x000F94A0
		public bool HasWarnings
		{
			get
			{
				if (base.Count > 0)
				{
					foreach (object obj in this)
					{
						CompilerError compilerError = (CompilerError)obj;
						if (compilerError.IsWarning)
						{
							return true;
						}
					}
					return false;
				}
				return false;
			}
		}

		// Token: 0x06003CE7 RID: 15591 RVA: 0x000FB308 File Offset: 0x000F9508
		public int IndexOf(CompilerError value)
		{
			return base.List.IndexOf(value);
		}

		// Token: 0x06003CE8 RID: 15592 RVA: 0x000FB316 File Offset: 0x000F9516
		public void Insert(int index, CompilerError value)
		{
			base.List.Insert(index, value);
		}

		// Token: 0x06003CE9 RID: 15593 RVA: 0x000FB325 File Offset: 0x000F9525
		public void Remove(CompilerError value)
		{
			base.List.Remove(value);
		}
	}
}
