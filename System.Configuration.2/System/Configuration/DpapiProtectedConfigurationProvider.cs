using System;
using System.Collections.Specialized;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Text;
using System.Xml;
using Microsoft.Win32;

namespace System.Configuration
{
	// Token: 0x02000055 RID: 85
	[PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
	public sealed class DpapiProtectedConfigurationProvider : ProtectedConfigurationProvider
	{
		// Token: 0x06000359 RID: 857 RVA: 0x00012B00 File Offset: 0x00010D00
		public override XmlNode Decrypt(XmlNode encryptedNode)
		{
			if (encryptedNode.NodeType != XmlNodeType.Element || encryptedNode.Name != "EncryptedData")
			{
				throw new ConfigurationErrorsException(SR.GetString("DPAPI_bad_data"));
			}
			XmlNode xmlNode = DpapiProtectedConfigurationProvider.TraverseToChild(encryptedNode, "CipherData", false);
			if (xmlNode == null)
			{
				throw new ConfigurationErrorsException(SR.GetString("DPAPI_bad_data"));
			}
			XmlNode xmlNode2 = DpapiProtectedConfigurationProvider.TraverseToChild(xmlNode, "CipherValue", true);
			if (xmlNode2 == null)
			{
				throw new ConfigurationErrorsException(SR.GetString("DPAPI_bad_data"));
			}
			string innerText = xmlNode2.InnerText;
			if (innerText == null)
			{
				throw new ConfigurationErrorsException(SR.GetString("DPAPI_bad_data"));
			}
			string xmlText = this.DecryptText(innerText);
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.PreserveWhitespace = true;
			ProtectedConfigurationProvider.LoadXml(xmlDocument, xmlText);
			return xmlDocument.DocumentElement;
		}

		// Token: 0x0600035A RID: 858 RVA: 0x00012BB8 File Offset: 0x00010DB8
		public override XmlNode Encrypt(XmlNode node)
		{
			string outerXml = node.OuterXml;
			string str = this.EncryptText(outerXml);
			string str2 = "<EncryptedData><CipherData><CipherValue>";
			string str3 = "</CipherValue></CipherData></EncryptedData>";
			string xmlText = str2 + str + str3;
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.PreserveWhitespace = true;
			ProtectedConfigurationProvider.LoadXml(xmlDocument, xmlText);
			return xmlDocument.DocumentElement;
		}

		// Token: 0x0600035B RID: 859 RVA: 0x00012C0C File Offset: 0x00010E0C
		private string EncryptText(string clearText)
		{
			if (clearText == null || clearText.Length < 1)
			{
				return clearText;
			}
			SafeNativeMemoryHandle safeNativeMemoryHandle = new SafeNativeMemoryHandle();
			SafeNativeMemoryHandle safeNativeMemoryHandle2 = new SafeNativeMemoryHandle(true);
			SafeNativeMemoryHandle safeNativeMemoryHandle3 = new SafeNativeMemoryHandle();
			DATA_BLOB data_BLOB;
			DATA_BLOB data_BLOB2;
			DATA_BLOB data_BLOB3;
			data_BLOB.pbData = (data_BLOB2.pbData = (data_BLOB3.pbData = IntPtr.Zero));
			data_BLOB.cbData = (data_BLOB2.cbData = (data_BLOB3.cbData = 0));
			string result;
			try
			{
				RuntimeHelpers.PrepareConstrainedRegions();
				try
				{
				}
				finally
				{
					data_BLOB = DpapiProtectedConfigurationProvider.PrepareDataBlob(clearText);
					safeNativeMemoryHandle.SetDataHandle(data_BLOB.pbData);
					data_BLOB2 = DpapiProtectedConfigurationProvider.PrepareDataBlob(this._KeyEntropy);
					safeNativeMemoryHandle3.SetDataHandle(data_BLOB2.pbData);
				}
				CRYPTPROTECT_PROMPTSTRUCT cryptprotect_PROMPTSTRUCT = DpapiProtectedConfigurationProvider.PreparePromptStructure();
				uint num = 1U;
				if (this.UseMachineProtection)
				{
					num |= 4U;
				}
				bool flag = false;
				RuntimeHelpers.PrepareConstrainedRegions();
				try
				{
				}
				finally
				{
					flag = UnsafeNativeMethods.CryptProtectData(ref data_BLOB, "", ref data_BLOB2, IntPtr.Zero, ref cryptprotect_PROMPTSTRUCT, num, ref data_BLOB3);
					safeNativeMemoryHandle2.SetDataHandle(data_BLOB3.pbData);
				}
				if (!flag || data_BLOB3.pbData == IntPtr.Zero)
				{
					data_BLOB3.pbData = IntPtr.Zero;
					Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());
				}
				byte[] array = new byte[data_BLOB3.cbData];
				Marshal.Copy(data_BLOB3.pbData, array, 0, array.Length);
				result = Convert.ToBase64String(array);
			}
			finally
			{
				if (safeNativeMemoryHandle2 != null && !safeNativeMemoryHandle2.IsInvalid)
				{
					safeNativeMemoryHandle2.Dispose();
					data_BLOB3.pbData = IntPtr.Zero;
				}
				if (safeNativeMemoryHandle3 != null && !safeNativeMemoryHandle3.IsInvalid)
				{
					safeNativeMemoryHandle3.Dispose();
					data_BLOB2.pbData = IntPtr.Zero;
				}
				if (safeNativeMemoryHandle != null && !safeNativeMemoryHandle.IsInvalid)
				{
					safeNativeMemoryHandle.Dispose();
					data_BLOB.pbData = IntPtr.Zero;
				}
			}
			return result;
		}

		// Token: 0x0600035C RID: 860 RVA: 0x00012DE0 File Offset: 0x00010FE0
		private string DecryptText(string encText)
		{
			if (encText == null || encText.Length < 1)
			{
				return encText;
			}
			SafeNativeMemoryHandle safeNativeMemoryHandle = new SafeNativeMemoryHandle();
			SafeNativeMemoryHandle safeNativeMemoryHandle2 = new SafeNativeMemoryHandle(true);
			SafeNativeMemoryHandle safeNativeMemoryHandle3 = new SafeNativeMemoryHandle();
			DATA_BLOB data_BLOB;
			DATA_BLOB data_BLOB2;
			DATA_BLOB data_BLOB3;
			data_BLOB.pbData = (data_BLOB2.pbData = (data_BLOB3.pbData = IntPtr.Zero));
			data_BLOB.cbData = (data_BLOB2.cbData = (data_BLOB3.cbData = 0));
			string @string;
			try
			{
				RuntimeHelpers.PrepareConstrainedRegions();
				try
				{
				}
				finally
				{
					data_BLOB = DpapiProtectedConfigurationProvider.PrepareDataBlob(Convert.FromBase64String(encText));
					safeNativeMemoryHandle.SetDataHandle(data_BLOB.pbData);
					data_BLOB2 = DpapiProtectedConfigurationProvider.PrepareDataBlob(this._KeyEntropy);
					safeNativeMemoryHandle3.SetDataHandle(data_BLOB2.pbData);
				}
				CRYPTPROTECT_PROMPTSTRUCT cryptprotect_PROMPTSTRUCT = DpapiProtectedConfigurationProvider.PreparePromptStructure();
				uint num = 1U;
				if (this.UseMachineProtection)
				{
					num |= 4U;
				}
				bool flag = false;
				RuntimeHelpers.PrepareConstrainedRegions();
				try
				{
				}
				finally
				{
					flag = UnsafeNativeMethods.CryptUnprotectData(ref data_BLOB, IntPtr.Zero, ref data_BLOB2, IntPtr.Zero, ref cryptprotect_PROMPTSTRUCT, num, ref data_BLOB3);
					safeNativeMemoryHandle2.SetDataHandle(data_BLOB3.pbData);
				}
				if (!flag || data_BLOB3.pbData == IntPtr.Zero)
				{
					data_BLOB3.pbData = IntPtr.Zero;
					Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());
				}
				byte[] array = new byte[data_BLOB3.cbData];
				Marshal.Copy(data_BLOB3.pbData, array, 0, array.Length);
				@string = Encoding.Unicode.GetString(array);
			}
			finally
			{
				if (safeNativeMemoryHandle2 != null && !safeNativeMemoryHandle2.IsInvalid)
				{
					safeNativeMemoryHandle2.Dispose();
					data_BLOB3.pbData = IntPtr.Zero;
				}
				if (safeNativeMemoryHandle3 != null && !safeNativeMemoryHandle3.IsInvalid)
				{
					safeNativeMemoryHandle3.Dispose();
					data_BLOB2.pbData = IntPtr.Zero;
				}
				if (safeNativeMemoryHandle != null && !safeNativeMemoryHandle.IsInvalid)
				{
					safeNativeMemoryHandle.Dispose();
					data_BLOB.pbData = IntPtr.Zero;
				}
			}
			return @string;
		}

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x0600035D RID: 861 RVA: 0x00012FBC File Offset: 0x000111BC
		public bool UseMachineProtection
		{
			get
			{
				return this._UseMachineProtection;
			}
		}

		// Token: 0x0600035E RID: 862 RVA: 0x00012FC4 File Offset: 0x000111C4
		public override void Initialize(string name, NameValueCollection configurationValues)
		{
			base.Initialize(name, configurationValues);
			this._UseMachineProtection = DpapiProtectedConfigurationProvider.GetBooleanValue(configurationValues, "useMachineProtection", true);
			this._KeyEntropy = configurationValues["keyEntropy"];
			configurationValues.Remove("keyEntropy");
			if (configurationValues.Count > 0)
			{
				throw new ConfigurationErrorsException(SR.GetString("Unrecognized_initialization_value", new object[]
				{
					configurationValues.GetKey(0)
				}));
			}
		}

		// Token: 0x0600035F RID: 863 RVA: 0x00013030 File Offset: 0x00011230
		private static XmlNode TraverseToChild(XmlNode node, string name, bool onlyChild)
		{
			foreach (object obj in node.ChildNodes)
			{
				XmlNode xmlNode = (XmlNode)obj;
				if (xmlNode.NodeType == XmlNodeType.Element)
				{
					if (xmlNode.Name == name)
					{
						return xmlNode;
					}
					if (onlyChild)
					{
						return null;
					}
				}
			}
			return null;
		}

		// Token: 0x06000360 RID: 864 RVA: 0x000130A8 File Offset: 0x000112A8
		private static DATA_BLOB PrepareDataBlob(byte[] buf)
		{
			if (buf == null)
			{
				buf = new byte[0];
			}
			DATA_BLOB data_BLOB = default(DATA_BLOB);
			data_BLOB.cbData = buf.Length;
			data_BLOB.pbData = Marshal.AllocHGlobal(data_BLOB.cbData);
			Marshal.Copy(buf, 0, data_BLOB.pbData, data_BLOB.cbData);
			return data_BLOB;
		}

		// Token: 0x06000361 RID: 865 RVA: 0x000130F8 File Offset: 0x000112F8
		private static DATA_BLOB PrepareDataBlob(string s)
		{
			return DpapiProtectedConfigurationProvider.PrepareDataBlob((s != null) ? Encoding.Unicode.GetBytes(s) : new byte[0]);
		}

		// Token: 0x06000362 RID: 866 RVA: 0x00013118 File Offset: 0x00011318
		private static CRYPTPROTECT_PROMPTSTRUCT PreparePromptStructure()
		{
			return new CRYPTPROTECT_PROMPTSTRUCT
			{
				cbSize = Marshal.SizeOf(typeof(CRYPTPROTECT_PROMPTSTRUCT)),
				dwPromptFlags = 0,
				hwndApp = IntPtr.Zero,
				szPrompt = null
			};
		}

		// Token: 0x06000363 RID: 867 RVA: 0x00013160 File Offset: 0x00011360
		private static bool GetBooleanValue(NameValueCollection configurationValues, string valueName, bool defaultValue)
		{
			string text = configurationValues[valueName];
			if (text == null)
			{
				return defaultValue;
			}
			configurationValues.Remove(valueName);
			if (text == "true")
			{
				return true;
			}
			if (text == "false")
			{
				return false;
			}
			throw new ConfigurationErrorsException(SR.GetString("Config_invalid_boolean_attribute", new object[]
			{
				valueName
			}));
		}

		// Token: 0x04000255 RID: 597
		private const int CRYPTPROTECT_UI_FORBIDDEN = 1;

		// Token: 0x04000256 RID: 598
		private const int CRYPTPROTECT_LOCAL_MACHINE = 4;

		// Token: 0x04000257 RID: 599
		private bool _UseMachineProtection = true;

		// Token: 0x04000258 RID: 600
		private string _KeyEntropy;
	}
}
