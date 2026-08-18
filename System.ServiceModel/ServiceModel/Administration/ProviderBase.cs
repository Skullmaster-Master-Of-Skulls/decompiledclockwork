using System;
using System.Collections;

namespace System.ServiceModel.Administration
{
	// Token: 0x02000450 RID: 1104
	internal abstract class ProviderBase : IWmiProvider
	{
		// Token: 0x06002AEB RID: 10987 RVA: 0x000A865C File Offset: 0x000A685C
		public static void FillCollectionInfo(ICollection info, IWmiInstance instance, string propertyName)
		{
			string[] array = new string[info.Count];
			int num = 0;
			foreach (object obj in info)
			{
				array[num++] = obj.ToString();
			}
			instance.SetProperty(propertyName, array);
		}

		// Token: 0x06002AEC RID: 10988 RVA: 0x000A86CC File Offset: 0x000A68CC
		public static void FillCollectionInfo(IEnumerable info, IWmiInstance instance, string propertyName)
		{
			int num = 0;
			foreach (object obj in info)
			{
				num++;
			}
			string[] array = new string[num];
			num = 0;
			foreach (object obj2 in info)
			{
				array[num++] = obj2.ToString();
			}
			instance.SetProperty(propertyName, array);
		}

		// Token: 0x06002AED RID: 10989 RVA: 0x000A877C File Offset: 0x000A697C
		void IWmiProvider.EnumInstances(IWmiInstances instances)
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new WbemNotSupportedException());
		}

		// Token: 0x06002AEE RID: 10990 RVA: 0x000A878D File Offset: 0x000A698D
		bool IWmiProvider.GetInstance(IWmiInstance contract)
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new WbemNotSupportedException());
		}

		// Token: 0x06002AEF RID: 10991 RVA: 0x000A879E File Offset: 0x000A699E
		bool IWmiProvider.PutInstance(IWmiInstance instance)
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new WbemNotSupportedException());
		}

		// Token: 0x06002AF0 RID: 10992 RVA: 0x000A87AF File Offset: 0x000A69AF
		bool IWmiProvider.DeleteInstance(IWmiInstance instance)
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new WbemNotSupportedException());
		}

		// Token: 0x06002AF1 RID: 10993 RVA: 0x000A87C0 File Offset: 0x000A69C0
		bool IWmiProvider.InvokeMethod(IWmiMethodContext method)
		{
			method.ReturnParameter = 0;
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new WbemNotSupportedException());
		}
	}
}
