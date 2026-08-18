using System;
using System.Collections.Generic;
using System.Globalization;

namespace System.Web.ModelBinding
{
	// Token: 0x02000686 RID: 1670
	[AttributeUsage(AttributeTargets.Parameter, Inherited = false, AllowMultiple = false)]
	public sealed class SessionAttribute : ValueProviderSourceAttribute
	{
		// Token: 0x17001741 RID: 5953
		// (get) Token: 0x060050F6 RID: 20726 RVA: 0x001171EF File Offset: 0x001153EF
		// (set) Token: 0x060050F7 RID: 20727 RVA: 0x001171F7 File Offset: 0x001153F7
		public string Name { get; private set; }

		// Token: 0x060050F8 RID: 20728 RVA: 0x00117200 File Offset: 0x00115400
		public SessionAttribute() : this(null)
		{
		}

		// Token: 0x060050F9 RID: 20729 RVA: 0x00117209 File Offset: 0x00115409
		public SessionAttribute(string name)
		{
			this.Name = name;
		}

		// Token: 0x060050FA RID: 20730 RVA: 0x00117218 File Offset: 0x00115418
		public override IValueProvider GetValueProvider(ModelBindingExecutionContext modelBindingExecutionContext)
		{
			if (modelBindingExecutionContext == null)
			{
				throw new ArgumentNullException("modelBindingExecutionContext");
			}
			HttpSessionStateBase session = modelBindingExecutionContext.HttpContext.Session;
			if (session == null)
			{
				return null;
			}
			Dictionary<string, object> dictionary = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
			foreach (object obj in session)
			{
				string text = (string)obj;
				if (text != null)
				{
					dictionary[text] = session[text];
				}
			}
			return new DictionaryValueProvider<object>(dictionary, CultureInfo.InvariantCulture);
		}

		// Token: 0x060050FB RID: 20731 RVA: 0x001172B0 File Offset: 0x001154B0
		public override string GetModelName()
		{
			return this.Name;
		}
	}
}
