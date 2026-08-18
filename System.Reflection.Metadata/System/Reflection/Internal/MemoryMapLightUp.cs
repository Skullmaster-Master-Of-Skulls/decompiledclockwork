using System;
using System.IO;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;

namespace System.Reflection.Internal
{
	// Token: 0x02000165 RID: 357
	internal static class MemoryMapLightUp
	{
		// Token: 0x170002B9 RID: 697
		// (get) Token: 0x06000B2C RID: 2860 RVA: 0x00020094 File Offset: 0x0001E294
		internal static bool IsAvailable
		{
			get
			{
				if (MemoryMapLightUp.s_lazyIsAvailable == null)
				{
					MemoryMapLightUp.s_lazyIsAvailable = new bool?(MemoryMapLightUp.TryLoadTypes());
				}
				return MemoryMapLightUp.s_lazyIsAvailable.Value;
			}
		}

		// Token: 0x06000B2D RID: 2861 RVA: 0x000200BB File Offset: 0x0001E2BB
		private static bool TryLoadType(string typeName, string modernAssembly, string classicAssembly, out Type type)
		{
			type = LightUpHelper.GetType(typeName, new string[]
			{
				modernAssembly,
				classicAssembly
			});
			return type != null;
		}

		// Token: 0x06000B2E RID: 2862 RVA: 0x000200D8 File Offset: 0x0001E2D8
		private static bool TryLoadTypes()
		{
			MemoryMapLightUp.TryLoadType("System.IO.MemoryMappedFiles.MemoryMappedFileSecurity", "System.IO.MemoryMappedFiles, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089", out MemoryMapLightUp.s_lazyMemoryMappedFileSecurityType);
			return FileStreamReadLightUp.FileStreamType.Value != null && MemoryMapLightUp.TryLoadType("System.IO.MemoryMappedFiles.MemoryMappedFile", "System.IO.MemoryMappedFiles, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089", out MemoryMapLightUp.s_lazyMemoryMappedFileType) && MemoryMapLightUp.TryLoadType("System.IO.MemoryMappedFiles.MemoryMappedViewAccessor", "System.IO.MemoryMappedFiles, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089", out MemoryMapLightUp.s_lazyMemoryMappedViewAccessorType) && MemoryMapLightUp.TryLoadType("System.IO.MemoryMappedFiles.MemoryMappedFileAccess", "System.IO.MemoryMappedFiles, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089", out MemoryMapLightUp.s_lazyMemoryMappedFileAccessType) && MemoryMapLightUp.TryLoadType("System.IO.HandleInheritability", "System.Runtime.Handles, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089", out MemoryMapLightUp.s_lazyHandleInheritabilityType) && MemoryMapLightUp.TryLoadMembers();
		}

		// Token: 0x06000B2F RID: 2863 RVA: 0x00020180 File Offset: 0x0001E380
		private static bool TryLoadMembers()
		{
			MemoryMapLightUp.s_lazyCreateFromFile = LightUpHelper.GetMethod(MemoryMapLightUp.s_lazyMemoryMappedFileType, "CreateFromFile", new Type[]
			{
				FileStreamReadLightUp.FileStreamType.Value,
				typeof(string),
				typeof(long),
				MemoryMapLightUp.s_lazyMemoryMappedFileAccessType,
				MemoryMapLightUp.s_lazyHandleInheritabilityType,
				typeof(bool)
			});
			if (MemoryMapLightUp.s_lazyCreateFromFile == null)
			{
				if (MemoryMapLightUp.s_lazyMemoryMappedFileSecurityType != null)
				{
					MemoryMapLightUp.s_lazyCreateFromFileClassic = LightUpHelper.GetMethod(MemoryMapLightUp.s_lazyMemoryMappedFileType, "CreateFromFile", new Type[]
					{
						FileStreamReadLightUp.FileStreamType.Value,
						typeof(string),
						typeof(long),
						MemoryMapLightUp.s_lazyMemoryMappedFileAccessType,
						MemoryMapLightUp.s_lazyMemoryMappedFileSecurityType,
						MemoryMapLightUp.s_lazyHandleInheritabilityType,
						typeof(bool)
					});
				}
				if (MemoryMapLightUp.s_lazyCreateFromFileClassic == null)
				{
					return false;
				}
			}
			MemoryMapLightUp.s_lazyCreateViewAccessor = LightUpHelper.GetMethod(MemoryMapLightUp.s_lazyMemoryMappedFileType, "CreateViewAccessor", new Type[]
			{
				typeof(long),
				typeof(long),
				MemoryMapLightUp.s_lazyMemoryMappedFileAccessType
			});
			if (MemoryMapLightUp.s_lazyCreateViewAccessor == null)
			{
				return false;
			}
			MemoryMapLightUp.s_lazySafeMemoryMappedViewHandle = MemoryMapLightUp.s_lazyMemoryMappedViewAccessorType.GetTypeInfo().GetDeclaredProperty("SafeMemoryMappedViewHandle");
			if (MemoryMapLightUp.s_lazySafeMemoryMappedViewHandle == null)
			{
				return false;
			}
			MemoryMapLightUp.s_lazyPointerOffset = MemoryMapLightUp.s_lazyMemoryMappedViewAccessorType.GetTypeInfo().GetDeclaredProperty("PointerOffset");
			if (MemoryMapLightUp.s_lazyPointerOffset == null)
			{
				MemoryMapLightUp.s_lazyInternalViewField = MemoryMapLightUp.s_lazyMemoryMappedViewAccessorType.GetTypeInfo().GetDeclaredField("m_view");
				if (MemoryMapLightUp.s_lazyInternalViewField == null)
				{
					return false;
				}
				MemoryMapLightUp.s_lazyInternalPointerOffset = MemoryMapLightUp.s_lazyInternalViewField.FieldType.GetTypeInfo().GetDeclaredProperty("PointerOffset");
				if (MemoryMapLightUp.s_lazyInternalPointerOffset == null)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000B30 RID: 2864 RVA: 0x0002033C File Offset: 0x0001E53C
		internal static IDisposable CreateMemoryMap(Stream stream)
		{
			IDisposable result;
			try
			{
				if (MemoryMapLightUp.s_lazyCreateFromFile != null)
				{
					result = (IDisposable)MemoryMapLightUp.s_lazyCreateFromFile.Invoke(null, new object[]
					{
						stream,
						null,
						MemoryMapLightUp.s_LongZero,
						MemoryMapLightUp.s_MemoryMappedFileAccess_Read,
						MemoryMapLightUp.s_HandleInheritability_None,
						MemoryMapLightUp.s_True
					});
				}
				else
				{
					result = (IDisposable)MemoryMapLightUp.s_lazyCreateFromFileClassic.Invoke(null, new object[]
					{
						stream,
						null,
						MemoryMapLightUp.s_LongZero,
						MemoryMapLightUp.s_MemoryMappedFileAccess_Read,
						null,
						MemoryMapLightUp.s_HandleInheritability_None,
						MemoryMapLightUp.s_True
					});
				}
			}
			catch (MemberAccessException)
			{
				MemoryMapLightUp.s_lazyIsAvailable = new bool?(false);
				result = null;
			}
			catch (InvalidOperationException)
			{
				MemoryMapLightUp.s_lazyIsAvailable = new bool?(false);
				result = null;
			}
			catch (TargetInvocationException ex)
			{
				ExceptionDispatchInfo.Capture(ex.InnerException).Throw();
				throw;
			}
			return result;
		}

		// Token: 0x06000B31 RID: 2865 RVA: 0x00020424 File Offset: 0x0001E624
		internal static IDisposable CreateViewAccessor(object memoryMap, long start, int size)
		{
			IDisposable result;
			try
			{
				result = (IDisposable)MemoryMapLightUp.s_lazyCreateViewAccessor.Invoke(memoryMap, new object[]
				{
					start,
					(long)size,
					MemoryMapLightUp.s_MemoryMappedFileAccess_Read
				});
			}
			catch (MemberAccessException)
			{
				MemoryMapLightUp.s_lazyIsAvailable = new bool?(false);
				result = null;
			}
			catch (InvalidOperationException)
			{
				MemoryMapLightUp.s_lazyIsAvailable = new bool?(false);
				result = null;
			}
			catch (TargetInvocationException ex)
			{
				ExceptionDispatchInfo.Capture(ex.InnerException).Throw();
				throw;
			}
			return result;
		}

		// Token: 0x06000B32 RID: 2866 RVA: 0x000204C0 File Offset: 0x0001E6C0
		internal unsafe static byte* AcquirePointer(object accessor, out SafeBuffer safeBuffer)
		{
			safeBuffer = (SafeBuffer)MemoryMapLightUp.s_lazySafeMemoryMappedViewHandle.GetValue(accessor);
			byte* ptr = null;
			safeBuffer.AcquirePointer(ref ptr);
			byte* result;
			try
			{
				long num;
				if (MemoryMapLightUp.s_lazyPointerOffset != null)
				{
					num = (long)MemoryMapLightUp.s_lazyPointerOffset.GetValue(accessor);
				}
				else
				{
					object value = MemoryMapLightUp.s_lazyInternalViewField.GetValue(accessor);
					num = (long)MemoryMapLightUp.s_lazyInternalPointerOffset.GetValue(value);
				}
				result = ptr + num;
			}
			catch (MemberAccessException)
			{
				MemoryMapLightUp.s_lazyIsAvailable = new bool?(false);
				result = null;
			}
			catch (InvalidOperationException)
			{
				MemoryMapLightUp.s_lazyIsAvailable = new bool?(false);
				result = null;
			}
			catch (TargetInvocationException ex)
			{
				ExceptionDispatchInfo.Capture(ex.InnerException).Throw();
				throw;
			}
			return result;
		}

		// Token: 0x04000918 RID: 2328
		private static Type s_lazyMemoryMappedFileType;

		// Token: 0x04000919 RID: 2329
		private static Type s_lazyMemoryMappedViewAccessorType;

		// Token: 0x0400091A RID: 2330
		private static Type s_lazyMemoryMappedFileAccessType;

		// Token: 0x0400091B RID: 2331
		private static Type s_lazyMemoryMappedFileSecurityType;

		// Token: 0x0400091C RID: 2332
		private static Type s_lazyHandleInheritabilityType;

		// Token: 0x0400091D RID: 2333
		private static MethodInfo s_lazyCreateFromFile;

		// Token: 0x0400091E RID: 2334
		private static MethodInfo s_lazyCreateFromFileClassic;

		// Token: 0x0400091F RID: 2335
		private static MethodInfo s_lazyCreateViewAccessor;

		// Token: 0x04000920 RID: 2336
		private static PropertyInfo s_lazySafeMemoryMappedViewHandle;

		// Token: 0x04000921 RID: 2337
		private static PropertyInfo s_lazyPointerOffset;

		// Token: 0x04000922 RID: 2338
		private static FieldInfo s_lazyInternalViewField;

		// Token: 0x04000923 RID: 2339
		private static PropertyInfo s_lazyInternalPointerOffset;

		// Token: 0x04000924 RID: 2340
		private static readonly object s_MemoryMappedFileAccess_Read = 1;

		// Token: 0x04000925 RID: 2341
		private static readonly object s_HandleInheritability_None = 0;

		// Token: 0x04000926 RID: 2342
		private static readonly object s_LongZero = 0L;

		// Token: 0x04000927 RID: 2343
		private static readonly object s_True = true;

		// Token: 0x04000928 RID: 2344
		private static bool? s_lazyIsAvailable;
	}
}
