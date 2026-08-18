using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Runtime.Serialization;

namespace Telerik.Web.UI.PivotGrid.Core
{
	// Token: 0x02000678 RID: 1656
	[DataContract]
	public abstract class Cloneable
	{
		// Token: 0x06003C75 RID: 15477 RVA: 0x000C3CB0 File Offset: 0x000C1EB0
		public Cloneable Clone()
		{
			Cloneable cloneable = this.CreateInstance();
			cloneable.CloneCore(this);
			return cloneable;
		}

		// Token: 0x06003C76 RID: 15478
		protected abstract Cloneable CreateInstanceCore();

		// Token: 0x06003C77 RID: 15479
		protected abstract void CloneCore(Cloneable source);

		// Token: 0x06003C78 RID: 15480 RVA: 0x000C3CCC File Offset: 0x000C1ECC
		private Cloneable CreateInstance()
		{
			Cloneable cloneable = this.CreateInstanceCore();
			Cloneable.VerifyInstance(this, cloneable);
			return cloneable;
		}

		// Token: 0x06003C79 RID: 15481 RVA: 0x000C3CE8 File Offset: 0x000C1EE8
		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "CreateInstance", Justification = "Design choice.")]
		private static void VerifyInstance(Cloneable original, Cloneable newInstance)
		{
			if (newInstance == null)
			{
				throw new ArgumentNullException("newInstance");
			}
			if (original == newInstance)
			{
				throw new InvalidOperationException("CreateInstance should not return the same instance as the original.");
			}
		}

		// Token: 0x06003C7A RID: 15482 RVA: 0x000C3D08 File Offset: 0x000C1F08
		internal static T CloneOrDefault<T>(T source) where T : Cloneable
		{
			if (source == null)
			{
				return default(T);
			}
			Cloneable cloneable = source.Clone();
			if (cloneable == null)
			{
				throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "Clone of {0} returned null. Type of {1} or derived type expected.", new object[]
				{
					source.GetType(),
					typeof(T)
				}));
			}
			T t = cloneable as T;
			if (t == null)
			{
				throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "Cloneable of type {0} resulted into a clone of type {1}. Type of {2} or derived type expected.", new object[]
				{
					source.GetType(),
					cloneable.GetType(),
					typeof(T)
				}));
			}
			return t;
		}
	}
}
