using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Permissions;
using OracleInternal.Common;

namespace OracleInternal.I18N
{
	// Token: 0x020000FF RID: 255
	[Serializable]
	internal class TLBConvBoot
	{
		// Token: 0x06000AE0 RID: 2784 RVA: 0x00078C04 File Offset: 0x00076E04
		public static TLBConvBoot GetInstance()
		{
			try
			{
				if (TLBConvBoot.upCache == null)
				{
					TLBConvBoot.upCache = (TLBConvBoot)TLBConvBoot.ReadObj("lx0boot.glb");
				}
			}
			catch
			{
				return null;
			}
			return TLBConvBoot.upCache;
		}

		// Token: 0x06000AE1 RID: 2785 RVA: 0x00078C4C File Offset: 0x00076E4C
		public string GetCharSetFileName(string charSetName)
		{
			return TLBConvBoot.FormatFileName("lx2", this.charSetIdMap[charSetName.ToUpper(new CultureInfo("en-US"))]);
		}

		// Token: 0x06000AE2 RID: 2786 RVA: 0x00078C74 File Offset: 0x00076E74
		public string GetCharSetId(string charSetName)
		{
			if (this.charSetIdMap.ContainsKey(charSetName.ToUpper(new CultureInfo("en-US"))))
			{
				return this.charSetIdMap[charSetName.ToUpper(new CultureInfo("en-US"))];
			}
			return string.Empty;
		}

		// Token: 0x06000AE3 RID: 2787 RVA: 0x00078CB4 File Offset: 0x00076EB4
		public string GetCharSetName(string id)
		{
			if (this.idtoCharSetMap.ContainsKey(id))
			{
				return this.idtoCharSetMap[id];
			}
			return string.Empty;
		}

		// Token: 0x06000AE4 RID: 2788 RVA: 0x00078CD8 File Offset: 0x00076ED8
		public string GetCharsetMaxCharLen(string id)
		{
			string result = "-1";
			if (!this.idtoCharSetMaxLen.ContainsKey(id) && id != null && id.Equals(this.GetCharSetId("AL16UTF16LE")))
			{
				return this.idtoCharSetMaxLen[this.GetCharSetId("AL16UTF16")];
			}
			if (!this.idtoCharSetMaxLen.ContainsKey(id))
			{
				return result;
			}
			return this.idtoCharSetMaxLen[id];
		}

		// Token: 0x06000AE5 RID: 2789 RVA: 0x00078D44 File Offset: 0x00076F44
		public IList<string> GetCharSetIsAscii()
		{
			return this.lstCharSetIsAscii;
		}

		// Token: 0x06000AE6 RID: 2790 RVA: 0x00078D4C File Offset: 0x00076F4C
		public IList<string> GetCharSetIsEbcdic()
		{
			return this.lstCharSetIsEbcdic;
		}

		// Token: 0x06000AE7 RID: 2791 RVA: 0x00078D54 File Offset: 0x00076F54
		public IList<string> GetCharSetIsFixed()
		{
			return this.lstCharSetIsFixed;
		}

		// Token: 0x06000AE8 RID: 2792 RVA: 0x00078D5C File Offset: 0x00076F5C
		public IList<string> GetCharSetIsStorage()
		{
			return this.lstCharSetIsStorage;
		}

		// Token: 0x06000AE9 RID: 2793 RVA: 0x00078D64 File Offset: 0x00076F64
		public string[] GetAvailableCharacterSets()
		{
			return this.availableCharSet;
		}

		// Token: 0x06000AEA RID: 2794 RVA: 0x00078D6C File Offset: 0x00076F6C
		protected static string FormatFileName(string prefix, string id)
		{
			string text = "0000";
			int num = Convert.ToInt32(id);
			string text2 = string.Format("{0:X}", num);
			string str = text.Substring(0, text.Length - text2.Length) + text2;
			return prefix + str;
		}

		// Token: 0x06000AEB RID: 2795 RVA: 0x00078DBC File Offset: 0x00076FBC
		[ReflectionPermission(SecurityAction.Assert, Unrestricted = true)]
		[SecurityPermission(SecurityAction.Assert, Unrestricted = true)]
		[FileIOPermission(SecurityAction.Assert, Unrestricted = true)]
		public static object ReadObj(string entryName)
		{
			Assembly executingAssembly = Assembly.GetExecutingAssembly();
			Stream manifestResourceStream = executingAssembly.GetManifestResourceStream("Oracle.ManagedDataAccess.src.I18N.Resources." + entryName);
			BinaryFormatter binaryFormatter = new BinaryFormatter();
			binaryFormatter.Binder = new DeserializationBinder();
			object result = null;
			using (GZipStream gzipStream = new GZipStream(manifestResourceStream, CompressionMode.Decompress, true))
			{
				result = binaryFormatter.Deserialize(gzipStream);
			}
			return result;
		}

		// Token: 0x06000AEC RID: 2796 RVA: 0x00078E28 File Offset: 0x00077028
		public void SetAvailableCharSets(string[] array)
		{
			this.availableCharSet = array;
		}

		// Token: 0x06000AED RID: 2797 RVA: 0x00078E34 File Offset: 0x00077034
		public void setCharSet(Dictionary<string, string> h)
		{
			this.charSetIdMap = h;
		}

		// Token: 0x06000AEE RID: 2798 RVA: 0x00078E40 File Offset: 0x00077040
		public void setIdtoCharSet(Dictionary<string, string> h)
		{
			this.idtoCharSetMap = h;
		}

		// Token: 0x06000AEF RID: 2799 RVA: 0x00078E4C File Offset: 0x0007704C
		public void setCharSetMaxLen(Dictionary<string, string> h)
		{
			this.idtoCharSetMaxLen = h;
		}

		// Token: 0x06000AF0 RID: 2800 RVA: 0x00078E58 File Offset: 0x00077058
		public void setCharSetFlags(IList<string> isAscii, IList<string> isEbcdic, IList<string> isFixed, IList<string> isStorage)
		{
			this.lstCharSetIsAscii = isAscii;
			this.lstCharSetIsEbcdic = isEbcdic;
			this.lstCharSetIsFixed = isFixed;
			this.lstCharSetIsStorage = isStorage;
		}

		// Token: 0x06000AF1 RID: 2801 RVA: 0x00078E78 File Offset: 0x00077078
		public Dictionary<string, string> getCharSetName2IdMap()
		{
			return this.charSetIdMap;
		}

		// Token: 0x04000CE1 RID: 3297
		private const string FILENAME = "lx0boot.glb";

		// Token: 0x04000CE2 RID: 3298
		public const int FLAG_FIXEDWIDTH = 256;

		// Token: 0x04000CE3 RID: 3299
		public const int FLAG_ASCII = 16;

		// Token: 0x04000CE4 RID: 3300
		public const int FLAG_EBCDIC = 1;

		// Token: 0x04000CE5 RID: 3301
		private static TLBConvBoot upCache;

		// Token: 0x04000CE6 RID: 3302
		protected Dictionary<string, string> charSetIdMap;

		// Token: 0x04000CE7 RID: 3303
		protected Dictionary<string, string> idtoCharSetMap;

		// Token: 0x04000CE8 RID: 3304
		protected Dictionary<string, string> idtoCharSetMaxLen;

		// Token: 0x04000CE9 RID: 3305
		protected IList<string> lstCharSetIsAscii;

		// Token: 0x04000CEA RID: 3306
		protected IList<string> lstCharSetIsEbcdic;

		// Token: 0x04000CEB RID: 3307
		protected IList<string> lstCharSetIsFixed;

		// Token: 0x04000CEC RID: 3308
		protected IList<string> lstCharSetIsStorage;

		// Token: 0x04000CED RID: 3309
		protected string[] availableCharSet;
	}
}
