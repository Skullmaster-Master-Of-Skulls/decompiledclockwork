using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using iTextSharp.text.error_messages;

namespace iTextSharp.text.pdf
{
	// Token: 0x020000C8 RID: 200
	public class PdfName : PdfObject, IComparable<PdfName>
	{
		// Token: 0x06000701 RID: 1793 RVA: 0x00022E48 File Offset: 0x00021E48
		static PdfName()
		{
			FieldInfo[] fields = typeof(PdfName).GetFields(BindingFlags.DeclaredOnly | BindingFlags.Static | BindingFlags.Public);
			PdfName.staticNames = new Dictionary<string, PdfName>(fields.Length);
			try
			{
				foreach (FieldInfo fieldInfo in fields)
				{
					if (fieldInfo.FieldType.Equals(typeof(PdfName)))
					{
						PdfName pdfName = (PdfName)fieldInfo.GetValue(null);
						PdfName.staticNames[PdfName.DecodeName(pdfName.ToString())] = pdfName;
					}
				}
			}
			catch
			{
			}
		}

		// Token: 0x06000702 RID: 1794 RVA: 0x000254DC File Offset: 0x000244DC
		public PdfName(string name) : this(name, true)
		{
		}

		// Token: 0x06000703 RID: 1795 RVA: 0x000254E8 File Offset: 0x000244E8
		public PdfName(string name, bool lengthCheck) : base(4)
		{
			int length = name.Length;
			if (lengthCheck && length > 127)
			{
				throw new ArgumentException(MessageLocalization.GetComposedMessage("the.name.1.is.too.long.2.characters", name, length));
			}
			this.bytes = PdfName.EncodeName(name);
		}

		// Token: 0x06000704 RID: 1796 RVA: 0x0002552E File Offset: 0x0002452E
		public PdfName(byte[] bytes) : base(4, bytes)
		{
		}

		// Token: 0x06000705 RID: 1797 RVA: 0x00025538 File Offset: 0x00024538
		public int CompareTo(PdfName name)
		{
			byte[] bytes = this.bytes;
			byte[] bytes2 = name.bytes;
			int num = Math.Min(bytes.Length, bytes2.Length);
			for (int i = 0; i < num; i++)
			{
				if (bytes[i] > bytes2[i])
				{
					return 1;
				}
				if (bytes[i] < bytes2[i])
				{
					return -1;
				}
			}
			if (bytes.Length < bytes2.Length)
			{
				return -1;
			}
			if (bytes.Length > bytes2.Length)
			{
				return 1;
			}
			return 0;
		}

		// Token: 0x06000706 RID: 1798 RVA: 0x00025594 File Offset: 0x00024594
		public override bool Equals(object obj)
		{
			return this == obj || (obj is PdfName && this.CompareTo((PdfName)obj) == 0);
		}

		// Token: 0x06000707 RID: 1799 RVA: 0x000255B8 File Offset: 0x000245B8
		public override int GetHashCode()
		{
			int num = this.hash;
			if (num == 0)
			{
				int num2 = 0;
				int num3 = this.bytes.Length;
				for (int i = 0; i < num3; i++)
				{
					num = 31 * num + (int)(this.bytes[num2++] & byte.MaxValue);
				}
				this.hash = num;
			}
			return num;
		}

		// Token: 0x06000708 RID: 1800 RVA: 0x00025608 File Offset: 0x00024608
		public static byte[] EncodeName(string name)
		{
			int length = name.Length;
			ByteBuffer byteBuffer = new ByteBuffer(length + 20);
			byteBuffer.Append('/');
			char[] array = name.ToCharArray();
			char[] array2 = array;
			int i = 0;
			while (i < array2.Length)
			{
				char c = array2[i];
				char c2 = c & 'ÿ';
				char c3 = c2;
				if (c3 <= '/')
				{
					if (c3 == ' ')
					{
						goto IL_BE;
					}
					switch (c3)
					{
					case '#':
					case '%':
					case '(':
					case ')':
						goto IL_BE;
					case '$':
					case '&':
					case '\'':
						goto IL_D8;
					default:
						if (c3 != '/')
						{
							goto IL_D8;
						}
						goto IL_BE;
					}
				}
				else
				{
					switch (c3)
					{
					case '<':
					case '>':
						goto IL_BE;
					case '=':
						goto IL_D8;
					default:
						switch (c3)
						{
						case '[':
						case ']':
							goto IL_BE;
						case '\\':
							goto IL_D8;
						default:
							switch (c3)
							{
							case '{':
							case '}':
								goto IL_BE;
							case '|':
								goto IL_D8;
							default:
								goto IL_D8;
							}
							break;
						}
						break;
					}
				}
				IL_112:
				i++;
				continue;
				IL_BE:
				byteBuffer.Append('#');
				byteBuffer.Append(Convert.ToString((int)c2, 16));
				goto IL_112;
				IL_D8:
				if (c2 > '~' || c2 < ' ')
				{
					byteBuffer.Append('#');
					if (c2 < '\u0010')
					{
						byteBuffer.Append('0');
					}
					byteBuffer.Append(Convert.ToString((int)c2, 16));
					goto IL_112;
				}
				byteBuffer.Append(c2);
				goto IL_112;
			}
			return byteBuffer.ToByteArray();
		}

		// Token: 0x06000709 RID: 1801 RVA: 0x00025740 File Offset: 0x00024740
		public static string DecodeName(string name)
		{
			StringBuilder stringBuilder = new StringBuilder();
			int length = name.Length;
			for (int i = 1; i < length; i++)
			{
				char c = name[i];
				if (c == '#')
				{
					c = (char)((PRTokeniser.GetHex((int)name[i + 1]) << 4) + PRTokeniser.GetHex((int)name[i + 2]));
					i += 2;
				}
				stringBuilder.Append(c);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0400037D RID: 893
		public static readonly PdfName _3D = new PdfName("3D");

		// Token: 0x0400037E RID: 894
		public static readonly PdfName A = new PdfName("A");

		// Token: 0x0400037F RID: 895
		public static readonly PdfName A85 = new PdfName("A85");

		// Token: 0x04000380 RID: 896
		public static readonly PdfName AA = new PdfName("AA");

		// Token: 0x04000381 RID: 897
		public static readonly PdfName ABSOLUTECOLORIMETRIC = new PdfName("AbsoluteColorimetric");

		// Token: 0x04000382 RID: 898
		public static readonly PdfName AC = new PdfName("AC");

		// Token: 0x04000383 RID: 899
		public static readonly PdfName ACROFORM = new PdfName("AcroForm");

		// Token: 0x04000384 RID: 900
		public static readonly PdfName ACTION = new PdfName("Action");

		// Token: 0x04000385 RID: 901
		public static readonly PdfName ACTIVATION = new PdfName("Activation");

		// Token: 0x04000386 RID: 902
		public static readonly PdfName ADBE = new PdfName("ADBE");

		// Token: 0x04000387 RID: 903
		public static readonly PdfName ACTUALTEXT = new PdfName("ActualText");

		// Token: 0x04000388 RID: 904
		public static readonly PdfName ADBE_PKCS7_DETACHED = new PdfName("adbe.pkcs7.detached");

		// Token: 0x04000389 RID: 905
		public static readonly PdfName ADBE_PKCS7_S4 = new PdfName("adbe.pkcs7.s4");

		// Token: 0x0400038A RID: 906
		public static readonly PdfName ADBE_PKCS7_S5 = new PdfName("adbe.pkcs7.s5");

		// Token: 0x0400038B RID: 907
		public static readonly PdfName ADBE_PKCS7_SHA1 = new PdfName("adbe.pkcs7.sha1");

		// Token: 0x0400038C RID: 908
		public static readonly PdfName ADBE_X509_RSA_SHA1 = new PdfName("adbe.x509.rsa_sha1");

		// Token: 0x0400038D RID: 909
		public static readonly PdfName ADOBE_PPKLITE = new PdfName("Adobe.PPKLite");

		// Token: 0x0400038E RID: 910
		public static readonly PdfName ADOBE_PPKMS = new PdfName("Adobe.PPKMS");

		// Token: 0x0400038F RID: 911
		public static readonly PdfName AESV2 = new PdfName("AESV2");

		// Token: 0x04000390 RID: 912
		public static readonly PdfName AHX = new PdfName("AHx");

		// Token: 0x04000391 RID: 913
		public static readonly PdfName AIS = new PdfName("AIS");

		// Token: 0x04000392 RID: 914
		public static readonly PdfName ALLPAGES = new PdfName("AllPages");

		// Token: 0x04000393 RID: 915
		public static readonly PdfName ALT = new PdfName("Alt");

		// Token: 0x04000394 RID: 916
		public static readonly PdfName ALTERNATE = new PdfName("Alternate");

		// Token: 0x04000395 RID: 917
		public static readonly PdfName AND = new PdfName("And");

		// Token: 0x04000396 RID: 918
		public static readonly PdfName ANIMATION = new PdfName("Animation");

		// Token: 0x04000397 RID: 919
		public static readonly PdfName ANNOT = new PdfName("Annot");

		// Token: 0x04000398 RID: 920
		public static readonly PdfName ANNOTS = new PdfName("Annots");

		// Token: 0x04000399 RID: 921
		public static readonly PdfName ANTIALIAS = new PdfName("AntiAlias");

		// Token: 0x0400039A RID: 922
		public static readonly PdfName AP = new PdfName("AP");

		// Token: 0x0400039B RID: 923
		public static readonly PdfName APPDEFAULT = new PdfName("AppDefault");

		// Token: 0x0400039C RID: 924
		public static readonly PdfName ART = new PdfName("Art");

		// Token: 0x0400039D RID: 925
		public static readonly PdfName ARTBOX = new PdfName("ArtBox");

		// Token: 0x0400039E RID: 926
		public static readonly PdfName ASCENT = new PdfName("Ascent");

		// Token: 0x0400039F RID: 927
		public static readonly PdfName AS = new PdfName("AS");

		// Token: 0x040003A0 RID: 928
		public static readonly PdfName ASCII85DECODE = new PdfName("ASCII85Decode");

		// Token: 0x040003A1 RID: 929
		public static readonly PdfName ASCIIHEXDECODE = new PdfName("ASCIIHexDecode");

		// Token: 0x040003A2 RID: 930
		public static readonly PdfName ASSET = new PdfName("Asset");

		// Token: 0x040003A3 RID: 931
		public static readonly PdfName ASSETS = new PdfName("Assets");

		// Token: 0x040003A4 RID: 932
		public static readonly PdfName AUTHEVENT = new PdfName("AuthEvent");

		// Token: 0x040003A5 RID: 933
		public static readonly PdfName AUTHOR = new PdfName("Author");

		// Token: 0x040003A6 RID: 934
		public static readonly PdfName B = new PdfName("B");

		// Token: 0x040003A7 RID: 935
		public static readonly PdfName BACKGROUND = new PdfName("Background");

		// Token: 0x040003A8 RID: 936
		public static readonly PdfName BASEENCODING = new PdfName("BaseEncoding");

		// Token: 0x040003A9 RID: 937
		public static readonly PdfName BASEFONT = new PdfName("BaseFont");

		// Token: 0x040003AA RID: 938
		public static readonly PdfName BASEVERSION = new PdfName("BaseVersion");

		// Token: 0x040003AB RID: 939
		public static readonly PdfName BBOX = new PdfName("BBox");

		// Token: 0x040003AC RID: 940
		public static readonly PdfName BC = new PdfName("BC");

		// Token: 0x040003AD RID: 941
		public static readonly PdfName BG = new PdfName("BG");

		// Token: 0x040003AE RID: 942
		public static readonly PdfName BIBENTRY = new PdfName("BibEntry");

		// Token: 0x040003AF RID: 943
		public static readonly PdfName BIGFIVE = new PdfName("BigFive");

		// Token: 0x040003B0 RID: 944
		public static readonly PdfName BINDING = new PdfName("Binding");

		// Token: 0x040003B1 RID: 945
		public static readonly PdfName BINDINGMATERIALNAME = new PdfName("BindingMaterialName");

		// Token: 0x040003B2 RID: 946
		public static readonly PdfName BITSPERCOMPONENT = new PdfName("BitsPerComponent");

		// Token: 0x040003B3 RID: 947
		public static readonly PdfName BITSPERSAMPLE = new PdfName("BitsPerSample");

		// Token: 0x040003B4 RID: 948
		public static readonly PdfName BL = new PdfName("Bl");

		// Token: 0x040003B5 RID: 949
		public static readonly PdfName BLACKIS1 = new PdfName("BlackIs1");

		// Token: 0x040003B6 RID: 950
		public static readonly PdfName BLACKPOINT = new PdfName("BlackPoint");

		// Token: 0x040003B7 RID: 951
		public static readonly PdfName BLOCKQUOTE = new PdfName("BlockQuote");

		// Token: 0x040003B8 RID: 952
		public static readonly PdfName BLEEDBOX = new PdfName("BleedBox");

		// Token: 0x040003B9 RID: 953
		public static readonly PdfName BLINDS = new PdfName("Blinds");

		// Token: 0x040003BA RID: 954
		public static readonly PdfName BM = new PdfName("BM");

		// Token: 0x040003BB RID: 955
		public static readonly PdfName BORDER = new PdfName("Border");

		// Token: 0x040003BC RID: 956
		public static readonly PdfName BOUNDS = new PdfName("Bounds");

		// Token: 0x040003BD RID: 957
		public static readonly PdfName BOX = new PdfName("Box");

		// Token: 0x040003BE RID: 958
		public static readonly PdfName BS = new PdfName("BS");

		// Token: 0x040003BF RID: 959
		public static readonly PdfName BTN = new PdfName("Btn");

		// Token: 0x040003C0 RID: 960
		public static readonly PdfName BYTERANGE = new PdfName("ByteRange");

		// Token: 0x040003C1 RID: 961
		public static readonly PdfName C = new PdfName("C");

		// Token: 0x040003C2 RID: 962
		public static readonly PdfName C0 = new PdfName("C0");

		// Token: 0x040003C3 RID: 963
		public static readonly PdfName C1 = new PdfName("C1");

		// Token: 0x040003C4 RID: 964
		public static readonly PdfName CA = new PdfName("CA");

		// Token: 0x040003C5 RID: 965
		public static readonly PdfName ca_ = new PdfName("ca");

		// Token: 0x040003C6 RID: 966
		public static readonly PdfName CALGRAY = new PdfName("CalGray");

		// Token: 0x040003C7 RID: 967
		public static readonly PdfName CALRGB = new PdfName("CalRGB");

		// Token: 0x040003C8 RID: 968
		public static readonly PdfName CAPHEIGHT = new PdfName("CapHeight");

		// Token: 0x040003C9 RID: 969
		public static readonly PdfName CAPTION = new PdfName("Caption");

		// Token: 0x040003CA RID: 970
		public static readonly PdfName CATALOG = new PdfName("Catalog");

		// Token: 0x040003CB RID: 971
		public static readonly PdfName CATEGORY = new PdfName("Category");

		// Token: 0x040003CC RID: 972
		public static readonly PdfName CCITTFAXDECODE = new PdfName("CCITTFaxDecode");

		// Token: 0x040003CD RID: 973
		public static readonly PdfName CENTER = new PdfName("Center");

		// Token: 0x040003CE RID: 974
		public static readonly PdfName CENTERWINDOW = new PdfName("CenterWindow");

		// Token: 0x040003CF RID: 975
		public static readonly PdfName CERT = new PdfName("Cert");

		// Token: 0x040003D0 RID: 976
		public static readonly PdfName CF = new PdfName("CF");

		// Token: 0x040003D1 RID: 977
		public static readonly PdfName CFM = new PdfName("CFM");

		// Token: 0x040003D2 RID: 978
		public static readonly PdfName CH = new PdfName("Ch");

		// Token: 0x040003D3 RID: 979
		public static readonly PdfName CHARPROCS = new PdfName("CharProcs");

		// Token: 0x040003D4 RID: 980
		public static readonly PdfName CHECKSUM = new PdfName("CheckSum");

		// Token: 0x040003D5 RID: 981
		public static readonly PdfName CI = new PdfName("CI");

		// Token: 0x040003D6 RID: 982
		public static readonly PdfName CIDFONTTYPE0 = new PdfName("CIDFontType0");

		// Token: 0x040003D7 RID: 983
		public static readonly PdfName CIDFONTTYPE2 = new PdfName("CIDFontType2");

		// Token: 0x040003D8 RID: 984
		public static readonly PdfName CIDSET = new PdfName("CIDSet");

		// Token: 0x040003D9 RID: 985
		public static readonly PdfName CIDSYSTEMINFO = new PdfName("CIDSystemInfo");

		// Token: 0x040003DA RID: 986
		public static readonly PdfName CIDTOGIDMAP = new PdfName("CIDToGIDMap");

		// Token: 0x040003DB RID: 987
		public static readonly PdfName CIRCLE = new PdfName("Circle");

		// Token: 0x040003DC RID: 988
		public static readonly PdfName CMD = new PdfName("CMD");

		// Token: 0x040003DD RID: 989
		public static readonly PdfName CO = new PdfName("CO");

		// Token: 0x040003DE RID: 990
		public static readonly PdfName CODE = new PdfName("Code");

		// Token: 0x040003DF RID: 991
		public static readonly PdfName COLORS = new PdfName("Colors");

		// Token: 0x040003E0 RID: 992
		public static readonly PdfName COLORSPACE = new PdfName("ColorSpace");

		// Token: 0x040003E1 RID: 993
		public static readonly PdfName COLLECTION = new PdfName("Collection");

		// Token: 0x040003E2 RID: 994
		public static readonly PdfName COLLECTIONFIELD = new PdfName("CollectionField");

		// Token: 0x040003E3 RID: 995
		public static readonly PdfName COLLECTIONITEM = new PdfName("CollectionItem");

		// Token: 0x040003E4 RID: 996
		public static readonly PdfName COLLECTIONSCHEMA = new PdfName("CollectionSchema");

		// Token: 0x040003E5 RID: 997
		public static readonly PdfName COLLECTIONSORT = new PdfName("CollectionSort");

		// Token: 0x040003E6 RID: 998
		public static readonly PdfName COLLECTIONSUBITEM = new PdfName("CollectionSubitem");

		// Token: 0x040003E7 RID: 999
		public static readonly PdfName COLUMNS = new PdfName("Columns");

		// Token: 0x040003E8 RID: 1000
		public static readonly PdfName CONDITION = new PdfName("Condition");

		// Token: 0x040003E9 RID: 1001
		public static readonly PdfName CONFIGURATION = new PdfName("Configuration");

		// Token: 0x040003EA RID: 1002
		public static readonly PdfName CONFIGURATIONS = new PdfName("Configurations");

		// Token: 0x040003EB RID: 1003
		public static readonly PdfName CONTACTINFO = new PdfName("ContactInfo");

		// Token: 0x040003EC RID: 1004
		public static readonly PdfName CONTENT = new PdfName("Content");

		// Token: 0x040003ED RID: 1005
		public static readonly PdfName CONTENTS = new PdfName("Contents");

		// Token: 0x040003EE RID: 1006
		public static readonly PdfName COORDS = new PdfName("Coords");

		// Token: 0x040003EF RID: 1007
		public static readonly PdfName COUNT = new PdfName("Count");

		// Token: 0x040003F0 RID: 1008
		public static readonly PdfName COURIER = new PdfName("Courier");

		// Token: 0x040003F1 RID: 1009
		public static readonly PdfName COURIER_BOLD = new PdfName("Courier-Bold");

		// Token: 0x040003F2 RID: 1010
		public static readonly PdfName COURIER_OBLIQUE = new PdfName("Courier-Oblique");

		// Token: 0x040003F3 RID: 1011
		public static readonly PdfName COURIER_BOLDOBLIQUE = new PdfName("Courier-BoldOblique");

		// Token: 0x040003F4 RID: 1012
		public static readonly PdfName CREATIONDATE = new PdfName("CreationDate");

		// Token: 0x040003F5 RID: 1013
		public static readonly PdfName CREATOR = new PdfName("Creator");

		// Token: 0x040003F6 RID: 1014
		public static readonly PdfName CREATORINFO = new PdfName("CreatorInfo");

		// Token: 0x040003F7 RID: 1015
		public static readonly PdfName CROPBOX = new PdfName("CropBox");

		// Token: 0x040003F8 RID: 1016
		public static readonly PdfName CRYPT = new PdfName("Crypt");

		// Token: 0x040003F9 RID: 1017
		public static readonly PdfName CS = new PdfName("CS");

		// Token: 0x040003FA RID: 1018
		public static readonly PdfName CUEPOINT = new PdfName("CuePoint");

		// Token: 0x040003FB RID: 1019
		public static readonly PdfName CUEPOINTS = new PdfName("CuePoints");

		// Token: 0x040003FC RID: 1020
		public static readonly PdfName D = new PdfName("D");

		// Token: 0x040003FD RID: 1021
		public static readonly PdfName DA = new PdfName("DA");

		// Token: 0x040003FE RID: 1022
		public static readonly PdfName DATA = new PdfName("Data");

		// Token: 0x040003FF RID: 1023
		public static readonly PdfName DC = new PdfName("DC");

		// Token: 0x04000400 RID: 1024
		public static readonly PdfName DCTDECODE = new PdfName("DCTDecode");

		// Token: 0x04000401 RID: 1025
		public static readonly PdfName DEACTIVATION = new PdfName("Deactivation");

		// Token: 0x04000402 RID: 1026
		public static readonly PdfName DECODE = new PdfName("Decode");

		// Token: 0x04000403 RID: 1027
		public static readonly PdfName DECODEPARMS = new PdfName("DecodeParms");

		// Token: 0x04000404 RID: 1028
		public static readonly PdfName DEFAULT = new PdfName("Default");

		// Token: 0x04000405 RID: 1029
		public static readonly PdfName DEFAULTCRYPTFILTER = new PdfName("DefaultCryptFilter");

		// Token: 0x04000406 RID: 1030
		public static readonly PdfName DEFAULTCMYK = new PdfName("DefaultCMYK");

		// Token: 0x04000407 RID: 1031
		public static readonly PdfName DEFAULTGRAY = new PdfName("DefaultGray");

		// Token: 0x04000408 RID: 1032
		public static readonly PdfName DEFAULTRGB = new PdfName("DefaultRGB");

		// Token: 0x04000409 RID: 1033
		public static readonly PdfName DESC = new PdfName("Desc");

		// Token: 0x0400040A RID: 1034
		public static readonly PdfName DESCENDANTFONTS = new PdfName("DescendantFonts");

		// Token: 0x0400040B RID: 1035
		public static readonly PdfName DESCENT = new PdfName("Descent");

		// Token: 0x0400040C RID: 1036
		public static readonly PdfName DEST = new PdfName("Dest");

		// Token: 0x0400040D RID: 1037
		public static readonly PdfName DESTOUTPUTPROFILE = new PdfName("DestOutputProfile");

		// Token: 0x0400040E RID: 1038
		public static readonly PdfName DESTS = new PdfName("Dests");

		// Token: 0x0400040F RID: 1039
		public static readonly PdfName DEVICEGRAY = new PdfName("DeviceGray");

		// Token: 0x04000410 RID: 1040
		public static readonly PdfName DEVICERGB = new PdfName("DeviceRGB");

		// Token: 0x04000411 RID: 1041
		public static readonly PdfName DEVICECMYK = new PdfName("DeviceCMYK");

		// Token: 0x04000412 RID: 1042
		public static readonly PdfName DI = new PdfName("Di");

		// Token: 0x04000413 RID: 1043
		public static readonly PdfName DIFFERENCES = new PdfName("Differences");

		// Token: 0x04000414 RID: 1044
		public static readonly PdfName DISSOLVE = new PdfName("Dissolve");

		// Token: 0x04000415 RID: 1045
		public static readonly PdfName DIRECTION = new PdfName("Direction");

		// Token: 0x04000416 RID: 1046
		public static readonly PdfName DISPLAYDOCTITLE = new PdfName("DisplayDocTitle");

		// Token: 0x04000417 RID: 1047
		public static readonly PdfName DIV = new PdfName("Div");

		// Token: 0x04000418 RID: 1048
		public static readonly PdfName DL = new PdfName("DL");

		// Token: 0x04000419 RID: 1049
		public static readonly PdfName DM = new PdfName("Dm");

		// Token: 0x0400041A RID: 1050
		public static readonly PdfName DOCMDP = new PdfName("DocMDP");

		// Token: 0x0400041B RID: 1051
		public static readonly PdfName DOCOPEN = new PdfName("DocOpen");

		// Token: 0x0400041C RID: 1052
		public static readonly PdfName DOCUMENT = new PdfName("Document");

		// Token: 0x0400041D RID: 1053
		public static readonly PdfName DOMAIN = new PdfName("Domain");

		// Token: 0x0400041E RID: 1054
		public static readonly PdfName DP = new PdfName("DP");

		// Token: 0x0400041F RID: 1055
		public static readonly PdfName DR = new PdfName("DR");

		// Token: 0x04000420 RID: 1056
		public static readonly PdfName DS = new PdfName("DS");

		// Token: 0x04000421 RID: 1057
		public static readonly PdfName DUR = new PdfName("Dur");

		// Token: 0x04000422 RID: 1058
		public static readonly PdfName DUPLEX = new PdfName("Duplex");

		// Token: 0x04000423 RID: 1059
		public static readonly PdfName DUPLEXFLIPSHORTEDGE = new PdfName("DuplexFlipShortEdge");

		// Token: 0x04000424 RID: 1060
		public static readonly PdfName DUPLEXFLIPLONGEDGE = new PdfName("DuplexFlipLongEdge");

		// Token: 0x04000425 RID: 1061
		public static readonly PdfName DV = new PdfName("DV");

		// Token: 0x04000426 RID: 1062
		public static readonly PdfName DW = new PdfName("DW");

		// Token: 0x04000427 RID: 1063
		public static readonly PdfName E = new PdfName("E");

		// Token: 0x04000428 RID: 1064
		public static readonly PdfName EARLYCHANGE = new PdfName("EarlyChange");

		// Token: 0x04000429 RID: 1065
		public static readonly PdfName EF = new PdfName("EF");

		// Token: 0x0400042A RID: 1066
		public static readonly PdfName EFF = new PdfName("EFF");

		// Token: 0x0400042B RID: 1067
		public static readonly PdfName EFOPEN = new PdfName("EFOpen");

		// Token: 0x0400042C RID: 1068
		public static readonly PdfName EMBEDDED = new PdfName("Embedded");

		// Token: 0x0400042D RID: 1069
		public static readonly PdfName EMBEDDEDFILE = new PdfName("EmbeddedFile");

		// Token: 0x0400042E RID: 1070
		public static readonly PdfName EMBEDDEDFILES = new PdfName("EmbeddedFiles");

		// Token: 0x0400042F RID: 1071
		public static readonly PdfName ENCODE = new PdfName("Encode");

		// Token: 0x04000430 RID: 1072
		public static readonly PdfName ENCODEDBYTEALIGN = new PdfName("EncodedByteAlign");

		// Token: 0x04000431 RID: 1073
		public static readonly PdfName ENCODING = new PdfName("Encoding");

		// Token: 0x04000432 RID: 1074
		public static readonly PdfName ENCRYPT = new PdfName("Encrypt");

		// Token: 0x04000433 RID: 1075
		public static readonly PdfName ENCRYPTMETADATA = new PdfName("EncryptMetadata");

		// Token: 0x04000434 RID: 1076
		public static readonly PdfName ENDOFBLOCK = new PdfName("EndOfBlock");

		// Token: 0x04000435 RID: 1077
		public static readonly PdfName ENDOFLINE = new PdfName("EndOfLine");

		// Token: 0x04000436 RID: 1078
		public static readonly PdfName EXTEND = new PdfName("Extend");

		// Token: 0x04000437 RID: 1079
		public static readonly PdfName EXTENSIONS = new PdfName("Extensions");

		// Token: 0x04000438 RID: 1080
		public static readonly PdfName EXTENSIONLEVEL = new PdfName("ExtensionLevel");

		// Token: 0x04000439 RID: 1081
		public static readonly PdfName EXTGSTATE = new PdfName("ExtGState");

		// Token: 0x0400043A RID: 1082
		public static readonly PdfName EXPORT = new PdfName("Export");

		// Token: 0x0400043B RID: 1083
		public static readonly PdfName EXPORTSTATE = new PdfName("ExportState");

		// Token: 0x0400043C RID: 1084
		public static readonly PdfName EVENT = new PdfName("Event");

		// Token: 0x0400043D RID: 1085
		public static readonly PdfName F = new PdfName("F");

		// Token: 0x0400043E RID: 1086
		public static readonly PdfName FAR = new PdfName("Far");

		// Token: 0x0400043F RID: 1087
		public static readonly PdfName FB = new PdfName("FB");

		// Token: 0x04000440 RID: 1088
		public static readonly PdfName FDECODEPARMS = new PdfName("FDecodeParms");

		// Token: 0x04000441 RID: 1089
		public static readonly PdfName FDF = new PdfName("FDF");

		// Token: 0x04000442 RID: 1090
		public static readonly PdfName FF = new PdfName("Ff");

		// Token: 0x04000443 RID: 1091
		public static readonly PdfName FFILTER = new PdfName("FFilter");

		// Token: 0x04000444 RID: 1092
		public static readonly PdfName FG = new PdfName("FG");

		// Token: 0x04000445 RID: 1093
		public static readonly PdfName FIELDS = new PdfName("Fields");

		// Token: 0x04000446 RID: 1094
		public static readonly PdfName FIGURE = new PdfName("Figure");

		// Token: 0x04000447 RID: 1095
		public static readonly PdfName FILEATTACHMENT = new PdfName("FileAttachment");

		// Token: 0x04000448 RID: 1096
		public static readonly PdfName FILESPEC = new PdfName("Filespec");

		// Token: 0x04000449 RID: 1097
		public static readonly PdfName FILTER = new PdfName("Filter");

		// Token: 0x0400044A RID: 1098
		public static readonly PdfName FIRST = new PdfName("First");

		// Token: 0x0400044B RID: 1099
		public static readonly PdfName FIRSTCHAR = new PdfName("FirstChar");

		// Token: 0x0400044C RID: 1100
		public static readonly PdfName FIRSTPAGE = new PdfName("FirstPage");

		// Token: 0x0400044D RID: 1101
		public static readonly PdfName FIT = new PdfName("Fit");

		// Token: 0x0400044E RID: 1102
		public static readonly PdfName FITH = new PdfName("FitH");

		// Token: 0x0400044F RID: 1103
		public static readonly PdfName FITV = new PdfName("FitV");

		// Token: 0x04000450 RID: 1104
		public static readonly PdfName FITR = new PdfName("FitR");

		// Token: 0x04000451 RID: 1105
		public static readonly PdfName FITB = new PdfName("FitB");

		// Token: 0x04000452 RID: 1106
		public static readonly PdfName FITBH = new PdfName("FitBH");

		// Token: 0x04000453 RID: 1107
		public static readonly PdfName FITBV = new PdfName("FitBV");

		// Token: 0x04000454 RID: 1108
		public static readonly PdfName FITWINDOW = new PdfName("FitWindow");

		// Token: 0x04000455 RID: 1109
		public static readonly PdfName FL = new PdfName("Fl");

		// Token: 0x04000456 RID: 1110
		public static readonly PdfName FLAGS = new PdfName("Flags");

		// Token: 0x04000457 RID: 1111
		public static readonly PdfName FLASH = new PdfName("Flash");

		// Token: 0x04000458 RID: 1112
		public static readonly PdfName FLASHVARS = new PdfName("FlashVars");

		// Token: 0x04000459 RID: 1113
		public static readonly PdfName FLATEDECODE = new PdfName("FlateDecode");

		// Token: 0x0400045A RID: 1114
		public static readonly PdfName FO = new PdfName("Fo");

		// Token: 0x0400045B RID: 1115
		public static readonly PdfName FONT = new PdfName("Font");

		// Token: 0x0400045C RID: 1116
		public static readonly PdfName FONTBBOX = new PdfName("FontBBox");

		// Token: 0x0400045D RID: 1117
		public static readonly PdfName FONTDESCRIPTOR = new PdfName("FontDescriptor");

		// Token: 0x0400045E RID: 1118
		public static readonly PdfName FONTFILE = new PdfName("FontFile");

		// Token: 0x0400045F RID: 1119
		public static readonly PdfName FONTFILE2 = new PdfName("FontFile2");

		// Token: 0x04000460 RID: 1120
		public static readonly PdfName FONTFILE3 = new PdfName("FontFile3");

		// Token: 0x04000461 RID: 1121
		public static readonly PdfName FONTMATRIX = new PdfName("FontMatrix");

		// Token: 0x04000462 RID: 1122
		public static readonly PdfName FONTNAME = new PdfName("FontName");

		// Token: 0x04000463 RID: 1123
		public static readonly PdfName FOREGROUND = new PdfName("Foreground");

		// Token: 0x04000464 RID: 1124
		public static readonly PdfName FORM = new PdfName("Form");

		// Token: 0x04000465 RID: 1125
		public static readonly PdfName FORMTYPE = new PdfName("FormType");

		// Token: 0x04000466 RID: 1126
		public static readonly PdfName FORMULA = new PdfName("Formula");

		// Token: 0x04000467 RID: 1127
		public static readonly PdfName FREETEXT = new PdfName("FreeText");

		// Token: 0x04000468 RID: 1128
		public static readonly PdfName FRM = new PdfName("FRM");

		// Token: 0x04000469 RID: 1129
		public static readonly PdfName FS = new PdfName("FS");

		// Token: 0x0400046A RID: 1130
		public static readonly PdfName FT = new PdfName("FT");

		// Token: 0x0400046B RID: 1131
		public static readonly PdfName FULLSCREEN = new PdfName("FullScreen");

		// Token: 0x0400046C RID: 1132
		public static readonly PdfName FUNCTION = new PdfName("Function");

		// Token: 0x0400046D RID: 1133
		public static readonly PdfName FUNCTIONS = new PdfName("Functions");

		// Token: 0x0400046E RID: 1134
		public static readonly PdfName FUNCTIONTYPE = new PdfName("FunctionType");

		// Token: 0x0400046F RID: 1135
		public static readonly PdfName GAMMA = new PdfName("Gamma");

		// Token: 0x04000470 RID: 1136
		public static readonly PdfName GBK = new PdfName("GBK");

		// Token: 0x04000471 RID: 1137
		public static readonly PdfName GLITTER = new PdfName("Glitter");

		// Token: 0x04000472 RID: 1138
		public static readonly PdfName GOTO = new PdfName("GoTo");

		// Token: 0x04000473 RID: 1139
		public static readonly PdfName GOTOE = new PdfName("GoToE");

		// Token: 0x04000474 RID: 1140
		public static readonly PdfName GOTOR = new PdfName("GoToR");

		// Token: 0x04000475 RID: 1141
		public static readonly PdfName GROUP = new PdfName("Group");

		// Token: 0x04000476 RID: 1142
		public static readonly PdfName GTS_PDFA1 = new PdfName("GTS_PDFA1");

		// Token: 0x04000477 RID: 1143
		public static readonly PdfName GTS_PDFX = new PdfName("GTS_PDFX");

		// Token: 0x04000478 RID: 1144
		public static readonly PdfName GTS_PDFXVERSION = new PdfName("GTS_PDFXVersion");

		// Token: 0x04000479 RID: 1145
		public static readonly PdfName H = new PdfName("H");

		// Token: 0x0400047A RID: 1146
		public static readonly PdfName H1 = new PdfName("H1");

		// Token: 0x0400047B RID: 1147
		public static readonly PdfName H2 = new PdfName("H2");

		// Token: 0x0400047C RID: 1148
		public static readonly PdfName H3 = new PdfName("H3");

		// Token: 0x0400047D RID: 1149
		public static readonly PdfName H4 = new PdfName("H4");

		// Token: 0x0400047E RID: 1150
		public static readonly PdfName H5 = new PdfName("H5");

		// Token: 0x0400047F RID: 1151
		public static readonly PdfName H6 = new PdfName("H6");

		// Token: 0x04000480 RID: 1152
		public static readonly PdfName HALIGN = new PdfName("HAlign");

		// Token: 0x04000481 RID: 1153
		public static readonly PdfName HEIGHT = new PdfName("Height");

		// Token: 0x04000482 RID: 1154
		public static readonly PdfName HELV = new PdfName("Helv");

		// Token: 0x04000483 RID: 1155
		public static readonly PdfName HELVETICA = new PdfName("Helvetica");

		// Token: 0x04000484 RID: 1156
		public static readonly PdfName HELVETICA_BOLD = new PdfName("Helvetica-Bold");

		// Token: 0x04000485 RID: 1157
		public static readonly PdfName HELVETICA_OBLIQUE = new PdfName("Helvetica-Oblique");

		// Token: 0x04000486 RID: 1158
		public static readonly PdfName HELVETICA_BOLDOBLIQUE = new PdfName("Helvetica-BoldOblique");

		// Token: 0x04000487 RID: 1159
		public static readonly PdfName HF = new PdfName("HF");

		// Token: 0x04000488 RID: 1160
		public static readonly PdfName HID = new PdfName("Hid");

		// Token: 0x04000489 RID: 1161
		public static readonly PdfName HIDE = new PdfName("Hide");

		// Token: 0x0400048A RID: 1162
		public static readonly PdfName HIDEMENUBAR = new PdfName("HideMenubar");

		// Token: 0x0400048B RID: 1163
		public static readonly PdfName HIDETOOLBAR = new PdfName("HideToolbar");

		// Token: 0x0400048C RID: 1164
		public static readonly PdfName HIDEWINDOWUI = new PdfName("HideWindowUI");

		// Token: 0x0400048D RID: 1165
		public static readonly PdfName HIGHLIGHT = new PdfName("Highlight");

		// Token: 0x0400048E RID: 1166
		public static readonly PdfName HOFFSET = new PdfName("HOffset");

		// Token: 0x0400048F RID: 1167
		public static readonly PdfName I = new PdfName("I");

		// Token: 0x04000490 RID: 1168
		public static readonly PdfName ICCBASED = new PdfName("ICCBased");

		// Token: 0x04000491 RID: 1169
		public static readonly PdfName ID = new PdfName("ID");

		// Token: 0x04000492 RID: 1170
		public static readonly PdfName IDENTITY = new PdfName("Identity");

		// Token: 0x04000493 RID: 1171
		public static readonly PdfName IF = new PdfName("IF");

		// Token: 0x04000494 RID: 1172
		public static readonly PdfName IMAGE = new PdfName("Image");

		// Token: 0x04000495 RID: 1173
		public static readonly PdfName IMAGEB = new PdfName("ImageB");

		// Token: 0x04000496 RID: 1174
		public static readonly PdfName IMAGEC = new PdfName("ImageC");

		// Token: 0x04000497 RID: 1175
		public static readonly PdfName IMAGEI = new PdfName("ImageI");

		// Token: 0x04000498 RID: 1176
		public static readonly PdfName IMAGEMASK = new PdfName("ImageMask");

		// Token: 0x04000499 RID: 1177
		public static readonly PdfName IND = new PdfName("Ind");

		// Token: 0x0400049A RID: 1178
		public static readonly PdfName INDEX = new PdfName("Index");

		// Token: 0x0400049B RID: 1179
		public static readonly PdfName INDEXED = new PdfName("Indexed");

		// Token: 0x0400049C RID: 1180
		public static readonly PdfName INFO = new PdfName("Info");

		// Token: 0x0400049D RID: 1181
		public static readonly PdfName INK = new PdfName("Ink");

		// Token: 0x0400049E RID: 1182
		public static readonly PdfName INKLIST = new PdfName("InkList");

		// Token: 0x0400049F RID: 1183
		public static readonly PdfName INSTANCES = new PdfName("Instances");

		// Token: 0x040004A0 RID: 1184
		public static readonly PdfName IMPORTDATA = new PdfName("ImportData");

		// Token: 0x040004A1 RID: 1185
		public static readonly PdfName INTENT = new PdfName("Intent");

		// Token: 0x040004A2 RID: 1186
		public static readonly PdfName INTERPOLATE = new PdfName("Interpolate");

		// Token: 0x040004A3 RID: 1187
		public static readonly PdfName ISMAP = new PdfName("IsMap");

		// Token: 0x040004A4 RID: 1188
		public static readonly PdfName IRT = new PdfName("IRT");

		// Token: 0x040004A5 RID: 1189
		public static readonly PdfName ITALICANGLE = new PdfName("ItalicAngle");

		// Token: 0x040004A6 RID: 1190
		public static readonly PdfName ITXT = new PdfName("ITXT");

		// Token: 0x040004A7 RID: 1191
		public static readonly PdfName IX = new PdfName("IX");

		// Token: 0x040004A8 RID: 1192
		public static readonly PdfName JAVASCRIPT = new PdfName("JavaScript");

		// Token: 0x040004A9 RID: 1193
		public static readonly PdfName JBIG2DECODE = new PdfName("JBIG2Decode");

		// Token: 0x040004AA RID: 1194
		public static readonly PdfName JBIG2GLOBALS = new PdfName("JBIG2Globals");

		// Token: 0x040004AB RID: 1195
		public static readonly PdfName JPXDECODE = new PdfName("JPXDecode");

		// Token: 0x040004AC RID: 1196
		public static readonly PdfName JS = new PdfName("JS");

		// Token: 0x040004AD RID: 1197
		public static readonly PdfName K = new PdfName("K");

		// Token: 0x040004AE RID: 1198
		public static readonly PdfName KEYWORDS = new PdfName("Keywords");

		// Token: 0x040004AF RID: 1199
		public static readonly PdfName KIDS = new PdfName("Kids");

		// Token: 0x040004B0 RID: 1200
		public static readonly PdfName L = new PdfName("L");

		// Token: 0x040004B1 RID: 1201
		public static readonly PdfName L2R = new PdfName("L2R");

		// Token: 0x040004B2 RID: 1202
		public static readonly PdfName LANG = new PdfName("Lang");

		// Token: 0x040004B3 RID: 1203
		public static readonly PdfName LANGUAGE = new PdfName("Language");

		// Token: 0x040004B4 RID: 1204
		public static readonly PdfName LAST = new PdfName("Last");

		// Token: 0x040004B5 RID: 1205
		public static readonly PdfName LASTCHAR = new PdfName("LastChar");

		// Token: 0x040004B6 RID: 1206
		public static readonly PdfName LASTPAGE = new PdfName("LastPage");

		// Token: 0x040004B7 RID: 1207
		public static readonly PdfName LAUNCH = new PdfName("Launch");

		// Token: 0x040004B8 RID: 1208
		public static readonly PdfName LBL = new PdfName("Lbl");

		// Token: 0x040004B9 RID: 1209
		public static readonly PdfName LBODY = new PdfName("LBody");

		// Token: 0x040004BA RID: 1210
		public static readonly PdfName LENGTH = new PdfName("Length");

		// Token: 0x040004BB RID: 1211
		public static readonly PdfName LENGTH1 = new PdfName("Length1");

		// Token: 0x040004BC RID: 1212
		public static readonly PdfName LI = new PdfName("LI");

		// Token: 0x040004BD RID: 1213
		public static readonly PdfName LIMITS = new PdfName("Limits");

		// Token: 0x040004BE RID: 1214
		public static readonly PdfName LINE = new PdfName("Line");

		// Token: 0x040004BF RID: 1215
		public static readonly PdfName LINEAR = new PdfName("Linear");

		// Token: 0x040004C0 RID: 1216
		public static readonly PdfName LINK = new PdfName("Link");

		// Token: 0x040004C1 RID: 1217
		public static readonly PdfName LISTMODE = new PdfName("ListMode");

		// Token: 0x040004C2 RID: 1218
		public static readonly PdfName LOCATION = new PdfName("Location");

		// Token: 0x040004C3 RID: 1219
		public static readonly PdfName LOCK = new PdfName("Lock");

		// Token: 0x040004C4 RID: 1220
		public static readonly PdfName LOCKED = new PdfName("Locked");

		// Token: 0x040004C5 RID: 1221
		public static readonly PdfName LZWDECODE = new PdfName("LZWDecode");

		// Token: 0x040004C6 RID: 1222
		public static readonly PdfName M = new PdfName("M");

		// Token: 0x040004C7 RID: 1223
		public static readonly PdfName MATERIAL = new PdfName("Material");

		// Token: 0x040004C8 RID: 1224
		public static readonly PdfName MATRIX = new PdfName("Matrix");

		// Token: 0x040004C9 RID: 1225
		public static readonly PdfName MAC_EXPERT_ENCODING = new PdfName("MacExpertEncoding");

		// Token: 0x040004CA RID: 1226
		public static readonly PdfName MAC_ROMAN_ENCODING = new PdfName("MacRomanEncoding");

		// Token: 0x040004CB RID: 1227
		public static readonly PdfName MARKED = new PdfName("Marked");

		// Token: 0x040004CC RID: 1228
		public static readonly PdfName MARKINFO = new PdfName("MarkInfo");

		// Token: 0x040004CD RID: 1229
		public static readonly PdfName MASK = new PdfName("Mask");

		// Token: 0x040004CE RID: 1230
		public static readonly PdfName MAX_LOWER_CASE = new PdfName("max");

		// Token: 0x040004CF RID: 1231
		public static readonly PdfName MAX_CAMEL_CASE = new PdfName("Max");

		// Token: 0x040004D0 RID: 1232
		public static readonly PdfName MAXLEN = new PdfName("MaxLen");

		// Token: 0x040004D1 RID: 1233
		public static readonly PdfName MEDIABOX = new PdfName("MediaBox");

		// Token: 0x040004D2 RID: 1234
		public static readonly PdfName MCID = new PdfName("MCID");

		// Token: 0x040004D3 RID: 1235
		public static readonly PdfName MCR = new PdfName("MCR");

		// Token: 0x040004D4 RID: 1236
		public static readonly PdfName METADATA = new PdfName("Metadata");

		// Token: 0x040004D5 RID: 1237
		public static readonly PdfName MIN_LOWER_CASE = new PdfName("min");

		// Token: 0x040004D6 RID: 1238
		public static readonly PdfName MIN_CAMEL_CASE = new PdfName("Min");

		// Token: 0x040004D7 RID: 1239
		public static readonly PdfName MK = new PdfName("MK");

		// Token: 0x040004D8 RID: 1240
		public static readonly PdfName MMTYPE1 = new PdfName("MMType1");

		// Token: 0x040004D9 RID: 1241
		public static readonly PdfName MODDATE = new PdfName("ModDate");

		// Token: 0x040004DA RID: 1242
		public static readonly PdfName N = new PdfName("N");

		// Token: 0x040004DB RID: 1243
		public static readonly PdfName N0 = new PdfName("n0");

		// Token: 0x040004DC RID: 1244
		public static readonly PdfName N1 = new PdfName("n1");

		// Token: 0x040004DD RID: 1245
		public static readonly PdfName N2 = new PdfName("n2");

		// Token: 0x040004DE RID: 1246
		public static readonly PdfName N3 = new PdfName("n3");

		// Token: 0x040004DF RID: 1247
		public static readonly PdfName N4 = new PdfName("n4");

		// Token: 0x040004E0 RID: 1248
		public new static readonly PdfName NAME = new PdfName("Name");

		// Token: 0x040004E1 RID: 1249
		public static readonly PdfName NAMED = new PdfName("Named");

		// Token: 0x040004E2 RID: 1250
		public static readonly PdfName NAMES = new PdfName("Names");

		// Token: 0x040004E3 RID: 1251
		public static readonly PdfName NAVIGATION = new PdfName("Navigation");

		// Token: 0x040004E4 RID: 1252
		public static readonly PdfName NAVIGATIONPANE = new PdfName("NavigationPane");

		// Token: 0x040004E5 RID: 1253
		public static readonly PdfName NEAR = new PdfName("Near");

		// Token: 0x040004E6 RID: 1254
		public static readonly PdfName NEEDAPPEARANCES = new PdfName("NeedAppearances");

		// Token: 0x040004E7 RID: 1255
		public static readonly PdfName NEWWINDOW = new PdfName("NewWindow");

		// Token: 0x040004E8 RID: 1256
		public static readonly PdfName NEXT = new PdfName("Next");

		// Token: 0x040004E9 RID: 1257
		public static readonly PdfName NEXTPAGE = new PdfName("NextPage");

		// Token: 0x040004EA RID: 1258
		public static readonly PdfName NM = new PdfName("NM");

		// Token: 0x040004EB RID: 1259
		public static readonly PdfName NONE = new PdfName("None");

		// Token: 0x040004EC RID: 1260
		public static readonly PdfName NONFULLSCREENPAGEMODE = new PdfName("NonFullScreenPageMode");

		// Token: 0x040004ED RID: 1261
		public static readonly PdfName NONSTRUCT = new PdfName("NonStruct");

		// Token: 0x040004EE RID: 1262
		public static readonly PdfName NOT = new PdfName("Not");

		// Token: 0x040004EF RID: 1263
		public static readonly PdfName NOTE = new PdfName("Note");

		// Token: 0x040004F0 RID: 1264
		public static readonly PdfName NUMCOPIES = new PdfName("NumCopies");

		// Token: 0x040004F1 RID: 1265
		public static readonly PdfName NUMS = new PdfName("Nums");

		// Token: 0x040004F2 RID: 1266
		public static readonly PdfName O = new PdfName("O");

		// Token: 0x040004F3 RID: 1267
		public static readonly PdfName OBJ = new PdfName("Obj");

		// Token: 0x040004F4 RID: 1268
		public static readonly PdfName OBJR = new PdfName("OBJR");

		// Token: 0x040004F5 RID: 1269
		public static readonly PdfName OBJSTM = new PdfName("ObjStm");

		// Token: 0x040004F6 RID: 1270
		public static readonly PdfName OC = new PdfName("OC");

		// Token: 0x040004F7 RID: 1271
		public static readonly PdfName OCG = new PdfName("OCG");

		// Token: 0x040004F8 RID: 1272
		public static readonly PdfName OCGS = new PdfName("OCGs");

		// Token: 0x040004F9 RID: 1273
		public static readonly PdfName OCMD = new PdfName("OCMD");

		// Token: 0x040004FA RID: 1274
		public static readonly PdfName OCPROPERTIES = new PdfName("OCProperties");

		// Token: 0x040004FB RID: 1275
		public static readonly PdfName Off_ = new PdfName("Off");

		// Token: 0x040004FC RID: 1276
		public static readonly PdfName OFF = new PdfName("OFF");

		// Token: 0x040004FD RID: 1277
		public static readonly PdfName ON = new PdfName("ON");

		// Token: 0x040004FE RID: 1278
		public static readonly PdfName ONECOLUMN = new PdfName("OneColumn");

		// Token: 0x040004FF RID: 1279
		public static readonly PdfName OPEN = new PdfName("Open");

		// Token: 0x04000500 RID: 1280
		public static readonly PdfName OPENACTION = new PdfName("OpenAction");

		// Token: 0x04000501 RID: 1281
		public static readonly PdfName OP = new PdfName("OP");

		// Token: 0x04000502 RID: 1282
		public static readonly PdfName op_ = new PdfName("op");

		// Token: 0x04000503 RID: 1283
		public static readonly PdfName OPM = new PdfName("OPM");

		// Token: 0x04000504 RID: 1284
		public static readonly PdfName OPT = new PdfName("Opt");

		// Token: 0x04000505 RID: 1285
		public static readonly PdfName OR = new PdfName("Or");

		// Token: 0x04000506 RID: 1286
		public static readonly PdfName ORDER = new PdfName("Order");

		// Token: 0x04000507 RID: 1287
		public static readonly PdfName ORDERING = new PdfName("Ordering");

		// Token: 0x04000508 RID: 1288
		public static readonly PdfName ORG = new PdfName("Org");

		// Token: 0x04000509 RID: 1289
		public static readonly PdfName OSCILLATING = new PdfName("Oscillating");

		// Token: 0x0400050A RID: 1290
		public static readonly PdfName OUTLINES = new PdfName("Outlines");

		// Token: 0x0400050B RID: 1291
		public static readonly PdfName OUTPUTCONDITION = new PdfName("OutputCondition");

		// Token: 0x0400050C RID: 1292
		public static readonly PdfName OUTPUTCONDITIONIDENTIFIER = new PdfName("OutputConditionIdentifier");

		// Token: 0x0400050D RID: 1293
		public static readonly PdfName OUTPUTINTENT = new PdfName("OutputIntent");

		// Token: 0x0400050E RID: 1294
		public static readonly PdfName OUTPUTINTENTS = new PdfName("OutputIntents");

		// Token: 0x0400050F RID: 1295
		public static readonly PdfName P = new PdfName("P");

		// Token: 0x04000510 RID: 1296
		public static readonly PdfName PAGE = new PdfName("Page");

		// Token: 0x04000511 RID: 1297
		public static readonly PdfName PAGEELEMENT = new PdfName("PageElement");

		// Token: 0x04000512 RID: 1298
		public static readonly PdfName PAGELABELS = new PdfName("PageLabels");

		// Token: 0x04000513 RID: 1299
		public static readonly PdfName PAGELAYOUT = new PdfName("PageLayout");

		// Token: 0x04000514 RID: 1300
		public static readonly PdfName PAGEMODE = new PdfName("PageMode");

		// Token: 0x04000515 RID: 1301
		public static readonly PdfName PAGES = new PdfName("Pages");

		// Token: 0x04000516 RID: 1302
		public static readonly PdfName PAINTTYPE = new PdfName("PaintType");

		// Token: 0x04000517 RID: 1303
		public static readonly PdfName PANOSE = new PdfName("Panose");

		// Token: 0x04000518 RID: 1304
		public static readonly PdfName PARAMS = new PdfName("Params");

		// Token: 0x04000519 RID: 1305
		public static readonly PdfName PARENT = new PdfName("Parent");

		// Token: 0x0400051A RID: 1306
		public static readonly PdfName PARENTTREE = new PdfName("ParentTree");

		// Token: 0x0400051B RID: 1307
		public static readonly PdfName PARENTTREENEXTKEY = new PdfName("ParentTreeNextKey");

		// Token: 0x0400051C RID: 1308
		public static readonly PdfName PART = new PdfName("Part");

		// Token: 0x0400051D RID: 1309
		public static readonly PdfName PASSCONTEXTCLICK = new PdfName("PassContextClick");

		// Token: 0x0400051E RID: 1310
		public static readonly PdfName PATTERN = new PdfName("Pattern");

		// Token: 0x0400051F RID: 1311
		public static readonly PdfName PATTERNTYPE = new PdfName("PatternType");

		// Token: 0x04000520 RID: 1312
		public static readonly PdfName PC = new PdfName("PC");

		// Token: 0x04000521 RID: 1313
		public static readonly PdfName PDF = new PdfName("PDF");

		// Token: 0x04000522 RID: 1314
		public static readonly PdfName PDFDOCENCODING = new PdfName("PDFDocEncoding");

		// Token: 0x04000523 RID: 1315
		public static readonly PdfName PERCEPTUAL = new PdfName("Perceptual");

		// Token: 0x04000524 RID: 1316
		public static readonly PdfName PERMS = new PdfName("Perms");

		// Token: 0x04000525 RID: 1317
		public static readonly PdfName PG = new PdfName("Pg");

		// Token: 0x04000526 RID: 1318
		public static readonly PdfName PI = new PdfName("PI");

		// Token: 0x04000527 RID: 1319
		public static readonly PdfName PICKTRAYBYPDFSIZE = new PdfName("PickTrayByPDFSize");

		// Token: 0x04000528 RID: 1320
		public static readonly PdfName PLAYCOUNT = new PdfName("PlayCount");

		// Token: 0x04000529 RID: 1321
		public static readonly PdfName PO = new PdfName("PO");

		// Token: 0x0400052A RID: 1322
		public static readonly PdfName POLYGON = new PdfName("Polygon");

		// Token: 0x0400052B RID: 1323
		public static readonly PdfName POLYLINE = new PdfName("Polyline");

		// Token: 0x0400052C RID: 1324
		public static readonly PdfName POPUP = new PdfName("Popup");

		// Token: 0x0400052D RID: 1325
		public static readonly PdfName POSITION = new PdfName("Position");

		// Token: 0x0400052E RID: 1326
		public static readonly PdfName PREDICTOR = new PdfName("Predictor");

		// Token: 0x0400052F RID: 1327
		public static readonly PdfName PREFERRED = new PdfName("Preferred");

		// Token: 0x04000530 RID: 1328
		public static readonly PdfName PRESENTATION = new PdfName("Presentation");

		// Token: 0x04000531 RID: 1329
		public static readonly PdfName PRESERVERB = new PdfName("PreserveRB");

		// Token: 0x04000532 RID: 1330
		public static readonly PdfName PREV = new PdfName("Prev");

		// Token: 0x04000533 RID: 1331
		public static readonly PdfName PREVPAGE = new PdfName("PrevPage");

		// Token: 0x04000534 RID: 1332
		public static readonly PdfName PRINT = new PdfName("Print");

		// Token: 0x04000535 RID: 1333
		public static readonly PdfName PRINTAREA = new PdfName("PrintArea");

		// Token: 0x04000536 RID: 1334
		public static readonly PdfName PRINTCLIP = new PdfName("PrintClip");

		// Token: 0x04000537 RID: 1335
		public static readonly PdfName PRINTPAGERANGE = new PdfName("PrintPageRange");

		// Token: 0x04000538 RID: 1336
		public static readonly PdfName PRINTSCALING = new PdfName("PrintScaling");

		// Token: 0x04000539 RID: 1337
		public static readonly PdfName PRINTSTATE = new PdfName("PrintState");

		// Token: 0x0400053A RID: 1338
		public static readonly PdfName PRIVATE = new PdfName("Private");

		// Token: 0x0400053B RID: 1339
		public static readonly PdfName PROCSET = new PdfName("ProcSet");

		// Token: 0x0400053C RID: 1340
		public static readonly PdfName PRODUCER = new PdfName("Producer");

		// Token: 0x0400053D RID: 1341
		public static readonly PdfName PROPERTIES = new PdfName("Properties");

		// Token: 0x0400053E RID: 1342
		public static readonly PdfName PS = new PdfName("PS");

		// Token: 0x0400053F RID: 1343
		public static readonly PdfName PUBSEC = new PdfName("Adobe.PubSec");

		// Token: 0x04000540 RID: 1344
		public static readonly PdfName PV = new PdfName("PV");

		// Token: 0x04000541 RID: 1345
		public static readonly PdfName Q = new PdfName("Q");

		// Token: 0x04000542 RID: 1346
		public static readonly PdfName QUADPOINTS = new PdfName("QuadPoints");

		// Token: 0x04000543 RID: 1347
		public static readonly PdfName QUOTE = new PdfName("Quote");

		// Token: 0x04000544 RID: 1348
		public static readonly PdfName R = new PdfName("R");

		// Token: 0x04000545 RID: 1349
		public static readonly PdfName R2L = new PdfName("R2L");

		// Token: 0x04000546 RID: 1350
		public static readonly PdfName RANGE = new PdfName("Range");

		// Token: 0x04000547 RID: 1351
		public static readonly PdfName RC = new PdfName("RC");

		// Token: 0x04000548 RID: 1352
		public static readonly PdfName RBGROUPS = new PdfName("RBGroups");

		// Token: 0x04000549 RID: 1353
		public static readonly PdfName REASON = new PdfName("Reason");

		// Token: 0x0400054A RID: 1354
		public static readonly PdfName RECIPIENTS = new PdfName("Recipients");

		// Token: 0x0400054B RID: 1355
		public static readonly PdfName RECT = new PdfName("Rect");

		// Token: 0x0400054C RID: 1356
		public static readonly PdfName REFERENCE = new PdfName("Reference");

		// Token: 0x0400054D RID: 1357
		public static readonly PdfName REGISTRY = new PdfName("Registry");

		// Token: 0x0400054E RID: 1358
		public static readonly PdfName REGISTRYNAME = new PdfName("RegistryName");

		// Token: 0x0400054F RID: 1359
		public static readonly PdfName RELATIVECOLORIMETRIC = new PdfName("RelativeColorimetric");

		// Token: 0x04000550 RID: 1360
		public static readonly PdfName RENDITION = new PdfName("Rendition");

		// Token: 0x04000551 RID: 1361
		public static readonly PdfName RESETFORM = new PdfName("ResetForm");

		// Token: 0x04000552 RID: 1362
		public static readonly PdfName RESOURCES = new PdfName("Resources");

		// Token: 0x04000553 RID: 1363
		public static readonly PdfName RI = new PdfName("RI");

		// Token: 0x04000554 RID: 1364
		public static readonly PdfName RICHMEDIA = new PdfName("RichMedia");

		// Token: 0x04000555 RID: 1365
		public static readonly PdfName RICHMEDIAACTIVATION = new PdfName("RichMediaActivation");

		// Token: 0x04000556 RID: 1366
		public static readonly PdfName RICHMEDIAANIMATION = new PdfName("RichMediaAnimation");

		// Token: 0x04000557 RID: 1367
		public static readonly PdfName RICHMEDIACOMMAND = new PdfName("RichMediaCommand");

		// Token: 0x04000558 RID: 1368
		public static readonly PdfName RICHMEDIACONFIGURATION = new PdfName("RichMediaConfiguration");

		// Token: 0x04000559 RID: 1369
		public static readonly PdfName RICHMEDIACONTENT = new PdfName("RichMediaContent");

		// Token: 0x0400055A RID: 1370
		public static readonly PdfName RICHMEDIADEACTIVATION = new PdfName("RichMediaDeactivation");

		// Token: 0x0400055B RID: 1371
		public static readonly PdfName RICHMEDIAEXECUTE = new PdfName("RichMediaExecute");

		// Token: 0x0400055C RID: 1372
		public static readonly PdfName RICHMEDIAINSTANCE = new PdfName("RichMediaInstance");

		// Token: 0x0400055D RID: 1373
		public static readonly PdfName RICHMEDIAPARAMS = new PdfName("RichMediaParams");

		// Token: 0x0400055E RID: 1374
		public static readonly PdfName RICHMEDIAPOSITION = new PdfName("RichMediaPosition");

		// Token: 0x0400055F RID: 1375
		public static readonly PdfName RICHMEDIAPRESENTATION = new PdfName("RichMediaPresentation");

		// Token: 0x04000560 RID: 1376
		public static readonly PdfName RICHMEDIASETTINGS = new PdfName("RichMediaSettings");

		// Token: 0x04000561 RID: 1377
		public static readonly PdfName RICHMEDIAWINDOW = new PdfName("RichMediaWindow");

		// Token: 0x04000562 RID: 1378
		public static readonly PdfName ROLEMAP = new PdfName("RoleMap");

		// Token: 0x04000563 RID: 1379
		public static readonly PdfName ROOT = new PdfName("Root");

		// Token: 0x04000564 RID: 1380
		public static readonly PdfName ROTATE = new PdfName("Rotate");

		// Token: 0x04000565 RID: 1381
		public static readonly PdfName ROWS = new PdfName("Rows");

		// Token: 0x04000566 RID: 1382
		public static readonly PdfName RUBY = new PdfName("Ruby");

		// Token: 0x04000567 RID: 1383
		public static readonly PdfName RUNLENGTHDECODE = new PdfName("RunLengthDecode");

		// Token: 0x04000568 RID: 1384
		public static readonly PdfName RV = new PdfName("RV");

		// Token: 0x04000569 RID: 1385
		public static readonly PdfName S = new PdfName("S");

		// Token: 0x0400056A RID: 1386
		public static readonly PdfName SATURATION = new PdfName("Saturation");

		// Token: 0x0400056B RID: 1387
		public static readonly PdfName SCHEMA = new PdfName("Schema");

		// Token: 0x0400056C RID: 1388
		public static readonly PdfName SCREEN = new PdfName("Screen");

		// Token: 0x0400056D RID: 1389
		public static readonly PdfName SCRIPTS = new PdfName("Scripts");

		// Token: 0x0400056E RID: 1390
		public static readonly PdfName SECT = new PdfName("Sect");

		// Token: 0x0400056F RID: 1391
		public static readonly PdfName SEPARATION = new PdfName("Separation");

		// Token: 0x04000570 RID: 1392
		public static readonly PdfName SETOCGSTATE = new PdfName("SetOCGState");

		// Token: 0x04000571 RID: 1393
		public static readonly PdfName SETTINGS = new PdfName("Settings");

		// Token: 0x04000572 RID: 1394
		public static readonly PdfName SHADING = new PdfName("Shading");

		// Token: 0x04000573 RID: 1395
		public static readonly PdfName SHADINGTYPE = new PdfName("ShadingType");

		// Token: 0x04000574 RID: 1396
		public static readonly PdfName SHIFT_JIS = new PdfName("Shift-JIS");

		// Token: 0x04000575 RID: 1397
		public static readonly PdfName SIG = new PdfName("Sig");

		// Token: 0x04000576 RID: 1398
		public static readonly PdfName SIGFLAGS = new PdfName("SigFlags");

		// Token: 0x04000577 RID: 1399
		public static readonly PdfName SIGREF = new PdfName("SigRef");

		// Token: 0x04000578 RID: 1400
		public static readonly PdfName SIMPLEX = new PdfName("Simplex");

		// Token: 0x04000579 RID: 1401
		public static readonly PdfName SINGLEPAGE = new PdfName("SinglePage");

		// Token: 0x0400057A RID: 1402
		public static readonly PdfName SIZE = new PdfName("Size");

		// Token: 0x0400057B RID: 1403
		public static readonly PdfName SMASK = new PdfName("SMask");

		// Token: 0x0400057C RID: 1404
		public static readonly PdfName SORT = new PdfName("Sort");

		// Token: 0x0400057D RID: 1405
		public static readonly PdfName SOUND = new PdfName("Sound");

		// Token: 0x0400057E RID: 1406
		public static readonly PdfName SPAN = new PdfName("Span");

		// Token: 0x0400057F RID: 1407
		public static readonly PdfName SPEED = new PdfName("Speed");

		// Token: 0x04000580 RID: 1408
		public static readonly PdfName SPLIT = new PdfName("Split");

		// Token: 0x04000581 RID: 1409
		public static readonly PdfName SQUARE = new PdfName("Square");

		// Token: 0x04000582 RID: 1410
		public static readonly PdfName SQUIGGLY = new PdfName("Squiggly");

		// Token: 0x04000583 RID: 1411
		public static readonly PdfName ST = new PdfName("St");

		// Token: 0x04000584 RID: 1412
		public static readonly PdfName STAMP = new PdfName("Stamp");

		// Token: 0x04000585 RID: 1413
		public static readonly PdfName STANDARD = new PdfName("Standard");

		// Token: 0x04000586 RID: 1414
		public static readonly PdfName STATE = new PdfName("State");

		// Token: 0x04000587 RID: 1415
		public static readonly PdfName STDCF = new PdfName("StdCF");

		// Token: 0x04000588 RID: 1416
		public static readonly PdfName STEMV = new PdfName("StemV");

		// Token: 0x04000589 RID: 1417
		public static readonly PdfName STMF = new PdfName("StmF");

		// Token: 0x0400058A RID: 1418
		public static readonly PdfName STRF = new PdfName("StrF");

		// Token: 0x0400058B RID: 1419
		public static readonly PdfName STRIKEOUT = new PdfName("StrikeOut");

		// Token: 0x0400058C RID: 1420
		public static readonly PdfName STRUCTPARENT = new PdfName("StructParent");

		// Token: 0x0400058D RID: 1421
		public static readonly PdfName STRUCTPARENTS = new PdfName("StructParents");

		// Token: 0x0400058E RID: 1422
		public static readonly PdfName STRUCTTREEROOT = new PdfName("StructTreeRoot");

		// Token: 0x0400058F RID: 1423
		public static readonly PdfName STYLE = new PdfName("Style");

		// Token: 0x04000590 RID: 1424
		public static readonly PdfName SUBFILTER = new PdfName("SubFilter");

		// Token: 0x04000591 RID: 1425
		public static readonly PdfName SUBJECT = new PdfName("Subject");

		// Token: 0x04000592 RID: 1426
		public static readonly PdfName SUBMITFORM = new PdfName("SubmitForm");

		// Token: 0x04000593 RID: 1427
		public static readonly PdfName SUBTYPE = new PdfName("Subtype");

		// Token: 0x04000594 RID: 1428
		public static readonly PdfName SUPPLEMENT = new PdfName("Supplement");

		// Token: 0x04000595 RID: 1429
		public static readonly PdfName SV = new PdfName("SV");

		// Token: 0x04000596 RID: 1430
		public static readonly PdfName SW = new PdfName("SW");

		// Token: 0x04000597 RID: 1431
		public static readonly PdfName SYMBOL = new PdfName("Symbol");

		// Token: 0x04000598 RID: 1432
		public static readonly PdfName T = new PdfName("T");

		// Token: 0x04000599 RID: 1433
		public static readonly PdfName TA = new PdfName("TA");

		// Token: 0x0400059A RID: 1434
		public static readonly PdfName TABLE = new PdfName("Table");

		// Token: 0x0400059B RID: 1435
		public static readonly PdfName TABS = new PdfName("Tabs");

		// Token: 0x0400059C RID: 1436
		public static readonly PdfName TBODY = new PdfName("TBody");

		// Token: 0x0400059D RID: 1437
		public static readonly PdfName TD = new PdfName("TD");

		// Token: 0x0400059E RID: 1438
		public static readonly PdfName TEXT = new PdfName("Text");

		// Token: 0x0400059F RID: 1439
		public static readonly PdfName TFOOT = new PdfName("TFoot");

		// Token: 0x040005A0 RID: 1440
		public static readonly PdfName TH = new PdfName("TH");

		// Token: 0x040005A1 RID: 1441
		public static readonly PdfName THEAD = new PdfName("THead");

		// Token: 0x040005A2 RID: 1442
		public static readonly PdfName THUMB = new PdfName("Thumb");

		// Token: 0x040005A3 RID: 1443
		public static readonly PdfName THREADS = new PdfName("Threads");

		// Token: 0x040005A4 RID: 1444
		public static readonly PdfName TI = new PdfName("TI");

		// Token: 0x040005A5 RID: 1445
		public static readonly PdfName TIME = new PdfName("Time");

		// Token: 0x040005A6 RID: 1446
		public static readonly PdfName TILINGTYPE = new PdfName("TilingType");

		// Token: 0x040005A7 RID: 1447
		public static readonly PdfName TIMES_ROMAN = new PdfName("Times-Roman");

		// Token: 0x040005A8 RID: 1448
		public static readonly PdfName TIMES_BOLD = new PdfName("Times-Bold");

		// Token: 0x040005A9 RID: 1449
		public static readonly PdfName TIMES_ITALIC = new PdfName("Times-Italic");

		// Token: 0x040005AA RID: 1450
		public static readonly PdfName TIMES_BOLDITALIC = new PdfName("Times-BoldItalic");

		// Token: 0x040005AB RID: 1451
		public static readonly PdfName TITLE = new PdfName("Title");

		// Token: 0x040005AC RID: 1452
		public static readonly PdfName TK = new PdfName("TK");

		// Token: 0x040005AD RID: 1453
		public static readonly PdfName TM = new PdfName("TM");

		// Token: 0x040005AE RID: 1454
		public static readonly PdfName TOC = new PdfName("TOC");

		// Token: 0x040005AF RID: 1455
		public static readonly PdfName TOCI = new PdfName("TOCI");

		// Token: 0x040005B0 RID: 1456
		public static readonly PdfName TOGGLE = new PdfName("Toggle");

		// Token: 0x040005B1 RID: 1457
		public static readonly PdfName TOOLBAR = new PdfName("Toolbar");

		// Token: 0x040005B2 RID: 1458
		public static readonly PdfName TOUNICODE = new PdfName("ToUnicode");

		// Token: 0x040005B3 RID: 1459
		public static readonly PdfName TP = new PdfName("TP");

		// Token: 0x040005B4 RID: 1460
		public static readonly PdfName TABLEROW = new PdfName("TR");

		// Token: 0x040005B5 RID: 1461
		public static readonly PdfName TRANS = new PdfName("Trans");

		// Token: 0x040005B6 RID: 1462
		public static readonly PdfName TRANSFORMPARAMS = new PdfName("TransformParams");

		// Token: 0x040005B7 RID: 1463
		public static readonly PdfName TRANSFORMMETHOD = new PdfName("TransformMethod");

		// Token: 0x040005B8 RID: 1464
		public static readonly PdfName TRANSPARENCY = new PdfName("Transparency");

		// Token: 0x040005B9 RID: 1465
		public static readonly PdfName TRANSPARENT = new PdfName("Transparent");

		// Token: 0x040005BA RID: 1466
		public static readonly PdfName TRAPPED = new PdfName("Trapped");

		// Token: 0x040005BB RID: 1467
		public static readonly PdfName TRIMBOX = new PdfName("TrimBox");

		// Token: 0x040005BC RID: 1468
		public static readonly PdfName TRUETYPE = new PdfName("TrueType");

		// Token: 0x040005BD RID: 1469
		public static readonly PdfName TTL = new PdfName("Ttl");

		// Token: 0x040005BE RID: 1470
		public static readonly PdfName TU = new PdfName("TU");

		// Token: 0x040005BF RID: 1471
		public static readonly PdfName TWOCOLUMNLEFT = new PdfName("TwoColumnLeft");

		// Token: 0x040005C0 RID: 1472
		public static readonly PdfName TWOCOLUMNRIGHT = new PdfName("TwoColumnRight");

		// Token: 0x040005C1 RID: 1473
		public static readonly PdfName TWOPAGELEFT = new PdfName("TwoPageLeft");

		// Token: 0x040005C2 RID: 1474
		public static readonly PdfName TWOPAGERIGHT = new PdfName("TwoPageRight");

		// Token: 0x040005C3 RID: 1475
		public static readonly PdfName TX = new PdfName("Tx");

		// Token: 0x040005C4 RID: 1476
		public static readonly PdfName TYPE = new PdfName("Type");

		// Token: 0x040005C5 RID: 1477
		public static readonly PdfName TYPE0 = new PdfName("Type0");

		// Token: 0x040005C6 RID: 1478
		public static readonly PdfName TYPE1 = new PdfName("Type1");

		// Token: 0x040005C7 RID: 1479
		public static readonly PdfName TYPE3 = new PdfName("Type3");

		// Token: 0x040005C8 RID: 1480
		public static readonly PdfName U = new PdfName("U");

		// Token: 0x040005C9 RID: 1481
		public static readonly PdfName UF = new PdfName("UF");

		// Token: 0x040005CA RID: 1482
		public static readonly PdfName UHC = new PdfName("UHC");

		// Token: 0x040005CB RID: 1483
		public static readonly PdfName UNDERLINE = new PdfName("Underline");

		// Token: 0x040005CC RID: 1484
		public static readonly PdfName UR = new PdfName("UR");

		// Token: 0x040005CD RID: 1485
		public static readonly PdfName UR3 = new PdfName("UR3");

		// Token: 0x040005CE RID: 1486
		public static readonly PdfName URI = new PdfName("URI");

		// Token: 0x040005CF RID: 1487
		public static readonly PdfName URL = new PdfName("URL");

		// Token: 0x040005D0 RID: 1488
		public static readonly PdfName USAGE = new PdfName("Usage");

		// Token: 0x040005D1 RID: 1489
		public static readonly PdfName USEATTACHMENTS = new PdfName("UseAttachments");

		// Token: 0x040005D2 RID: 1490
		public static readonly PdfName USENONE = new PdfName("UseNone");

		// Token: 0x040005D3 RID: 1491
		public static readonly PdfName USEOC = new PdfName("UseOC");

		// Token: 0x040005D4 RID: 1492
		public static readonly PdfName USEOUTLINES = new PdfName("UseOutlines");

		// Token: 0x040005D5 RID: 1493
		public static readonly PdfName USER = new PdfName("User");

		// Token: 0x040005D6 RID: 1494
		public static readonly PdfName USERPROPERTIES = new PdfName("UserProperties");

		// Token: 0x040005D7 RID: 1495
		public static readonly PdfName USERUNIT = new PdfName("UserUnit");

		// Token: 0x040005D8 RID: 1496
		public static readonly PdfName USETHUMBS = new PdfName("UseThumbs");

		// Token: 0x040005D9 RID: 1497
		public static readonly PdfName V = new PdfName("V");

		// Token: 0x040005DA RID: 1498
		public static readonly PdfName V2 = new PdfName("V2");

		// Token: 0x040005DB RID: 1499
		public static readonly PdfName VALIGN = new PdfName("VAlign");

		// Token: 0x040005DC RID: 1500
		public static readonly PdfName VE = new PdfName("VE");

		// Token: 0x040005DD RID: 1501
		public static readonly PdfName VERISIGN_PPKVS = new PdfName("VeriSign.PPKVS");

		// Token: 0x040005DE RID: 1502
		public static readonly PdfName VERSION = new PdfName("Version");

		// Token: 0x040005DF RID: 1503
		public static readonly PdfName VERTICES = new PdfName("Vertices");

		// Token: 0x040005E0 RID: 1504
		public static readonly PdfName VIDEO = new PdfName("Video");

		// Token: 0x040005E1 RID: 1505
		public static readonly PdfName VIEW = new PdfName("View");

		// Token: 0x040005E2 RID: 1506
		public static readonly PdfName VIEWS = new PdfName("Views");

		// Token: 0x040005E3 RID: 1507
		public static readonly PdfName VIEWAREA = new PdfName("ViewArea");

		// Token: 0x040005E4 RID: 1508
		public static readonly PdfName VIEWCLIP = new PdfName("ViewClip");

		// Token: 0x040005E5 RID: 1509
		public static readonly PdfName VIEWERPREFERENCES = new PdfName("ViewerPreferences");

		// Token: 0x040005E6 RID: 1510
		public static readonly PdfName VIEWSTATE = new PdfName("ViewState");

		// Token: 0x040005E7 RID: 1511
		public static readonly PdfName VISIBLEPAGES = new PdfName("VisiblePages");

		// Token: 0x040005E8 RID: 1512
		public static readonly PdfName VOFFSET = new PdfName("VOffset");

		// Token: 0x040005E9 RID: 1513
		public static readonly PdfName W = new PdfName("W");

		// Token: 0x040005EA RID: 1514
		public static readonly PdfName W2 = new PdfName("W2");

		// Token: 0x040005EB RID: 1515
		public static readonly PdfName WARICHU = new PdfName("Warichu");

		// Token: 0x040005EC RID: 1516
		public static readonly PdfName WC = new PdfName("WC");

		// Token: 0x040005ED RID: 1517
		public static readonly PdfName WIDGET = new PdfName("Widget");

		// Token: 0x040005EE RID: 1518
		public static readonly PdfName WIDTH = new PdfName("Width");

		// Token: 0x040005EF RID: 1519
		public static readonly PdfName WIDTHS = new PdfName("Widths");

		// Token: 0x040005F0 RID: 1520
		public static readonly PdfName WIN = new PdfName("Win");

		// Token: 0x040005F1 RID: 1521
		public static readonly PdfName WIN_ANSI_ENCODING = new PdfName("WinAnsiEncoding");

		// Token: 0x040005F2 RID: 1522
		public static readonly PdfName WINDOW = new PdfName("Window");

		// Token: 0x040005F3 RID: 1523
		public static readonly PdfName WINDOWED = new PdfName("Windowed");

		// Token: 0x040005F4 RID: 1524
		public static readonly PdfName WIPE = new PdfName("Wipe");

		// Token: 0x040005F5 RID: 1525
		public static readonly PdfName WHITEPOINT = new PdfName("WhitePoint");

		// Token: 0x040005F6 RID: 1526
		public static readonly PdfName WP = new PdfName("WP");

		// Token: 0x040005F7 RID: 1527
		public static readonly PdfName WS = new PdfName("WS");

		// Token: 0x040005F8 RID: 1528
		public static readonly PdfName X = new PdfName("X");

		// Token: 0x040005F9 RID: 1529
		public static readonly PdfName XA = new PdfName("XA");

		// Token: 0x040005FA RID: 1530
		public static readonly PdfName XD = new PdfName("XD");

		// Token: 0x040005FB RID: 1531
		public static readonly PdfName XFA = new PdfName("XFA");

		// Token: 0x040005FC RID: 1532
		public static readonly PdfName XML = new PdfName("XML");

		// Token: 0x040005FD RID: 1533
		public static readonly PdfName XOBJECT = new PdfName("XObject");

		// Token: 0x040005FE RID: 1534
		public static readonly PdfName XSTEP = new PdfName("XStep");

		// Token: 0x040005FF RID: 1535
		public static readonly PdfName XREF = new PdfName("XRef");

		// Token: 0x04000600 RID: 1536
		public static readonly PdfName XREFSTM = new PdfName("XRefStm");

		// Token: 0x04000601 RID: 1537
		public static readonly PdfName XYZ = new PdfName("XYZ");

		// Token: 0x04000602 RID: 1538
		public static readonly PdfName YSTEP = new PdfName("YStep");

		// Token: 0x04000603 RID: 1539
		public static readonly PdfName ZADB = new PdfName("ZaDb");

		// Token: 0x04000604 RID: 1540
		public static readonly PdfName ZAPFDINGBATS = new PdfName("ZapfDingbats");

		// Token: 0x04000605 RID: 1541
		public static readonly PdfName ZOOM = new PdfName("Zoom");

		// Token: 0x04000606 RID: 1542
		public static Dictionary<string, PdfName> staticNames;

		// Token: 0x04000607 RID: 1543
		private int hash;
	}
}
