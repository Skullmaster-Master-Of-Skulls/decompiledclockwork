using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity.Internal;
using System.Data.Entity.Utilities;
using System.Diagnostics.CodeAnalysis;

namespace System.Data.Entity.Infrastructure
{
	// Token: 0x02000750 RID: 1872
	public class DbPropertyValues
	{
		// Token: 0x060054E4 RID: 21732 RVA: 0x00172458 File Offset: 0x00170658
		internal DbPropertyValues(InternalPropertyValues internalValues)
		{
			this._internalValues = internalValues;
		}

		// Token: 0x060054E5 RID: 21733 RVA: 0x00172467 File Offset: 0x00170667
		public object ToObject()
		{
			return this._internalValues.ToObject();
		}

		// Token: 0x060054E6 RID: 21734 RVA: 0x00172474 File Offset: 0x00170674
		[SuppressMessage("Microsoft.Naming", "CA1720:IdentifiersShouldNotContainTypeNames", MessageId = "obj", Justification = "Naming is intentional.")]
		public void SetValues(object obj)
		{
			Check.NotNull<object>(obj, "obj");
			this._internalValues.SetValues(obj);
		}

		// Token: 0x060054E7 RID: 21735 RVA: 0x0017248E File Offset: 0x0017068E
		public DbPropertyValues Clone()
		{
			return new DbPropertyValues(this._internalValues.Clone());
		}

		// Token: 0x060054E8 RID: 21736 RVA: 0x001724A0 File Offset: 0x001706A0
		public void SetValues(DbPropertyValues propertyValues)
		{
			Check.NotNull<DbPropertyValues>(propertyValues, "propertyValues");
			this._internalValues.SetValues(propertyValues._internalValues);
		}

		// Token: 0x17000E7D RID: 3709
		// (get) Token: 0x060054E9 RID: 21737 RVA: 0x001724BF File Offset: 0x001706BF
		public IEnumerable<string> PropertyNames
		{
			get
			{
				return this._internalValues.PropertyNames;
			}
		}

		// Token: 0x17000E7E RID: 3710
		public object this[string propertyName]
		{
			get
			{
				Check.NotEmpty(propertyName, "propertyName");
				object obj = this._internalValues[propertyName];
				InternalPropertyValues internalPropertyValues = obj as InternalPropertyValues;
				if (internalPropertyValues != null)
				{
					obj = new DbPropertyValues(internalPropertyValues);
				}
				return obj;
			}
			set
			{
				Check.NotEmpty(propertyName, "propertyName");
				this._internalValues[propertyName] = value;
			}
		}

		// Token: 0x060054EC RID: 21740 RVA: 0x0017251F File Offset: 0x0017071F
		public TValue GetValue<TValue>(string propertyName)
		{
			return (TValue)((object)this[propertyName]);
		}

		// Token: 0x17000E7F RID: 3711
		// (get) Token: 0x060054ED RID: 21741 RVA: 0x0017252D File Offset: 0x0017072D
		internal InternalPropertyValues InternalPropertyValues
		{
			get
			{
				return this._internalValues;
			}
		}

		// Token: 0x060054EE RID: 21742 RVA: 0x00172535 File Offset: 0x00170735
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string ToString()
		{
			return base.ToString();
		}

		// Token: 0x060054EF RID: 21743 RVA: 0x0017253D File Offset: 0x0017073D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x060054F0 RID: 21744 RVA: 0x00172546 File Offset: 0x00170746
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x060054F1 RID: 21745 RVA: 0x0017254E File Offset: 0x0017074E
		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new Type GetType()
		{
			return base.GetType();
		}

		// Token: 0x04002299 RID: 8857
		private readonly InternalPropertyValues _internalValues;
	}
}
