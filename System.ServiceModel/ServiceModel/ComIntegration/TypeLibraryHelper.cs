using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Runtime;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Threading;

namespace System.ServiceModel.ComIntegration
{
	// Token: 0x02000275 RID: 629
	internal sealed class TypeLibraryHelper
	{
		// Token: 0x060011F6 RID: 4598 RVA: 0x00041DA0 File Offset: 0x0003FFA0
		internal static Assembly GenerateAssemblyFromNativeTypeLibrary(Guid iid, Guid typeLibraryID, ITypeLib typeLibrary)
		{
			TypeLibraryHelper helperInstance = TypeLibraryHelper.GetHelperInstance();
			Assembly result;
			try
			{
				result = helperInstance.GenerateAssemblyFromNativeTypeLibInternal(iid, typeLibraryID, typeLibrary);
			}
			finally
			{
				TypeLibraryHelper.ReleaseHelperInstance();
			}
			return result;
		}

		// Token: 0x060011F7 RID: 4599 RVA: 0x00041DD8 File Offset: 0x0003FFD8
		private static TypeLibraryHelper GetHelperInstance()
		{
			object obj = TypeLibraryHelper.instanceLock;
			lock (obj)
			{
				if (TypeLibraryHelper.instance == null)
				{
					TypeLibraryHelper typeLibraryHelper = new TypeLibraryHelper();
					Thread.MemoryBarrier();
					TypeLibraryHelper.instance = typeLibraryHelper;
				}
			}
			Interlocked.Increment(ref TypeLibraryHelper.instanceCount);
			return TypeLibraryHelper.instance;
		}

		// Token: 0x060011F8 RID: 4600 RVA: 0x00041E3C File Offset: 0x0004003C
		private static void ReleaseHelperInstance()
		{
			if (Interlocked.Decrement(ref TypeLibraryHelper.instanceCount) == 0)
			{
				TypeLibraryHelper.instance = null;
			}
		}

		// Token: 0x060011F9 RID: 4601 RVA: 0x00041E50 File Offset: 0x00040050
		private string GetRandomName()
		{
			string text = Guid.NewGuid().ToString();
			return text.Replace('-', '_');
		}

		// Token: 0x060011FA RID: 4602 RVA: 0x00041E7C File Offset: 0x0004007C
		private Assembly GenerateAssemblyFromNativeTypeLibInternal(Guid iid, Guid typeLibraryID, ITypeLib typeLibrary)
		{
			Assembly assembly = null;
			try
			{
				lock (this)
				{
					this.TypelibraryAssembly.TryGetValue(typeLibraryID, out assembly);
					if (assembly == null)
					{
						string text = "";
						string text2 = "";
						string text3;
						int num;
						typeLibrary.GetDocumentation(-1, out text3, out text, out num, out text2);
						if (string.IsNullOrEmpty(text3))
						{
							throw Fx.AssertAndThrowFatal("Assembly cannot be null");
						}
						string asmFileName = text3 + this.GetRandomName() + ".dll";
						assembly = this.TypelibraryConverter.ConvertTypeLibToAssembly(typeLibrary, asmFileName, TypeLibImporterFlags.SerializableValueClasses, new TypeLibraryHelper.ConversionEventHandler(iid, typeLibraryID), null, null, text3, null);
						this.TypelibraryAssembly[typeLibraryID] = assembly;
					}
				}
			}
			catch (ReflectionTypeLoadException)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("FailedToConvertTypelibraryToAssembly")));
			}
			if (assembly == null)
			{
				throw Fx.AssertAndThrowFatal("Assembly cannot be null");
			}
			return assembly;
		}

		// Token: 0x040019C1 RID: 6593
		private static object instanceLock = new object();

		// Token: 0x040019C2 RID: 6594
		private static TypeLibraryHelper instance;

		// Token: 0x040019C3 RID: 6595
		private static int instanceCount = 0;

		// Token: 0x040019C4 RID: 6596
		private TypeLibConverter TypelibraryConverter = new TypeLibConverter();

		// Token: 0x040019C5 RID: 6597
		private Dictionary<Guid, Assembly> TypelibraryAssembly = new Dictionary<Guid, Assembly>();

		// Token: 0x02000B17 RID: 2839
		internal class ConversionEventHandler : ITypeLibImporterNotifySink
		{
			// Token: 0x06006F88 RID: 28552 RVA: 0x0019E0EF File Offset: 0x0019C2EF
			public ConversionEventHandler(Guid iid, Guid typeLibraryID)
			{
				this.iid = iid;
				this.typeLibraryID = typeLibraryID;
			}

			// Token: 0x06006F89 RID: 28553 RVA: 0x0019E105 File Offset: 0x0019C305
			void ITypeLibImporterNotifySink.ReportEvent(ImporterEventKind eventKind, int eventCode, string eventMsg)
			{
				ComPlusTLBImportTrace.Trace(TraceEventType.Verbose, 327696, "TraceCodeComIntegrationTLBImportConverterEvent", this.iid, this.typeLibraryID, eventKind, eventCode, eventMsg);
			}

			// Token: 0x06006F8A RID: 28554 RVA: 0x0019E128 File Offset: 0x0019C328
			Assembly ITypeLibImporterNotifySink.ResolveRef(object typeLib)
			{
				ITypeLib typeLib2 = typeLib as ITypeLib;
				IntPtr zero = IntPtr.Zero;
				Assembly result;
				try
				{
					typeLib2.GetLibAttr(out zero);
					System.Runtime.InteropServices.ComTypes.TYPELIBATTR typelibattr = (System.Runtime.InteropServices.ComTypes.TYPELIBATTR)Marshal.PtrToStructure(zero, typeof(System.Runtime.InteropServices.ComTypes.TYPELIBATTR));
					result = TypeLibraryHelper.GenerateAssemblyFromNativeTypeLibrary(this.iid, typelibattr.guid, typeLib as ITypeLib);
				}
				finally
				{
					if (zero != IntPtr.Zero && typeLib2 != null)
					{
						typeLib2.ReleaseTLibAttr(zero);
					}
				}
				return result;
			}

			// Token: 0x04003FCB RID: 16331
			private Guid iid;

			// Token: 0x04003FCC RID: 16332
			private Guid typeLibraryID;
		}
	}
}
