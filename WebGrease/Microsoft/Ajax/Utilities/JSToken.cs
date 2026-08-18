using System;

namespace Microsoft.Ajax.Utilities
{
	// Token: 0x020000AC RID: 172
	public enum JSToken
	{
		// Token: 0x04000415 RID: 1045
		None = -1,
		// Token: 0x04000416 RID: 1046
		EndOfFile,
		// Token: 0x04000417 RID: 1047
		Semicolon,
		// Token: 0x04000418 RID: 1048
		RightCurly,
		// Token: 0x04000419 RID: 1049
		LeftCurly,
		// Token: 0x0400041A RID: 1050
		Debugger,
		// Token: 0x0400041B RID: 1051
		Var,
		// Token: 0x0400041C RID: 1052
		If,
		// Token: 0x0400041D RID: 1053
		For,
		// Token: 0x0400041E RID: 1054
		Do,
		// Token: 0x0400041F RID: 1055
		While,
		// Token: 0x04000420 RID: 1056
		Continue,
		// Token: 0x04000421 RID: 1057
		Break,
		// Token: 0x04000422 RID: 1058
		Return,
		// Token: 0x04000423 RID: 1059
		With,
		// Token: 0x04000424 RID: 1060
		Switch,
		// Token: 0x04000425 RID: 1061
		Throw,
		// Token: 0x04000426 RID: 1062
		Try,
		// Token: 0x04000427 RID: 1063
		Function,
		// Token: 0x04000428 RID: 1064
		Else,
		// Token: 0x04000429 RID: 1065
		ConditionalCommentStart,
		// Token: 0x0400042A RID: 1066
		ConditionalCommentEnd,
		// Token: 0x0400042B RID: 1067
		ConditionalCompilationOn,
		// Token: 0x0400042C RID: 1068
		ConditionalCompilationSet,
		// Token: 0x0400042D RID: 1069
		ConditionalCompilationIf,
		// Token: 0x0400042E RID: 1070
		ConditionalCompilationElseIf,
		// Token: 0x0400042F RID: 1071
		ConditionalCompilationElse,
		// Token: 0x04000430 RID: 1072
		ConditionalCompilationEnd,
		// Token: 0x04000431 RID: 1073
		ConditionalCompilationVariable,
		// Token: 0x04000432 RID: 1074
		Identifier,
		// Token: 0x04000433 RID: 1075
		Null,
		// Token: 0x04000434 RID: 1076
		True,
		// Token: 0x04000435 RID: 1077
		False,
		// Token: 0x04000436 RID: 1078
		This,
		// Token: 0x04000437 RID: 1079
		StringLiteral,
		// Token: 0x04000438 RID: 1080
		IntegerLiteral,
		// Token: 0x04000439 RID: 1081
		NumericLiteral,
		// Token: 0x0400043A RID: 1082
		TemplateLiteral,
		// Token: 0x0400043B RID: 1083
		LeftParenthesis,
		// Token: 0x0400043C RID: 1084
		LeftBracket,
		// Token: 0x0400043D RID: 1085
		AccessField,
		// Token: 0x0400043E RID: 1086
		ArrowFunction,
		// Token: 0x0400043F RID: 1087
		RestSpread,
		// Token: 0x04000440 RID: 1088
		FirstOperator,
		// Token: 0x04000441 RID: 1089
		Delete = 42,
		// Token: 0x04000442 RID: 1090
		Increment,
		// Token: 0x04000443 RID: 1091
		Decrement,
		// Token: 0x04000444 RID: 1092
		Void,
		// Token: 0x04000445 RID: 1093
		TypeOf,
		// Token: 0x04000446 RID: 1094
		LogicalNot,
		// Token: 0x04000447 RID: 1095
		BitwiseNot,
		// Token: 0x04000448 RID: 1096
		FirstBinaryOperator,
		// Token: 0x04000449 RID: 1097
		Plus = 49,
		// Token: 0x0400044A RID: 1098
		Minus,
		// Token: 0x0400044B RID: 1099
		Multiply,
		// Token: 0x0400044C RID: 1100
		Divide,
		// Token: 0x0400044D RID: 1101
		Modulo,
		// Token: 0x0400044E RID: 1102
		BitwiseAnd,
		// Token: 0x0400044F RID: 1103
		BitwiseOr,
		// Token: 0x04000450 RID: 1104
		BitwiseXor,
		// Token: 0x04000451 RID: 1105
		LeftShift,
		// Token: 0x04000452 RID: 1106
		RightShift,
		// Token: 0x04000453 RID: 1107
		UnsignedRightShift,
		// Token: 0x04000454 RID: 1108
		Equal,
		// Token: 0x04000455 RID: 1109
		NotEqual,
		// Token: 0x04000456 RID: 1110
		StrictEqual,
		// Token: 0x04000457 RID: 1111
		StrictNotEqual,
		// Token: 0x04000458 RID: 1112
		LessThan,
		// Token: 0x04000459 RID: 1113
		LessThanEqual,
		// Token: 0x0400045A RID: 1114
		GreaterThan,
		// Token: 0x0400045B RID: 1115
		GreaterThanEqual,
		// Token: 0x0400045C RID: 1116
		LogicalAnd,
		// Token: 0x0400045D RID: 1117
		LogicalOr,
		// Token: 0x0400045E RID: 1118
		InstanceOf,
		// Token: 0x0400045F RID: 1119
		In,
		// Token: 0x04000460 RID: 1120
		Comma,
		// Token: 0x04000461 RID: 1121
		Assign,
		// Token: 0x04000462 RID: 1122
		PlusAssign,
		// Token: 0x04000463 RID: 1123
		MinusAssign,
		// Token: 0x04000464 RID: 1124
		MultiplyAssign,
		// Token: 0x04000465 RID: 1125
		DivideAssign,
		// Token: 0x04000466 RID: 1126
		ModuloAssign,
		// Token: 0x04000467 RID: 1127
		BitwiseAndAssign,
		// Token: 0x04000468 RID: 1128
		BitwiseOrAssign,
		// Token: 0x04000469 RID: 1129
		BitwiseXorAssign,
		// Token: 0x0400046A RID: 1130
		LeftShiftAssign,
		// Token: 0x0400046B RID: 1131
		RightShiftAssign,
		// Token: 0x0400046C RID: 1132
		UnsignedRightShiftAssign,
		// Token: 0x0400046D RID: 1133
		LastAssign = 84,
		// Token: 0x0400046E RID: 1134
		ConditionalIf,
		// Token: 0x0400046F RID: 1135
		Colon,
		// Token: 0x04000470 RID: 1136
		LastOperator = 86,
		// Token: 0x04000471 RID: 1137
		Case,
		// Token: 0x04000472 RID: 1138
		Catch,
		// Token: 0x04000473 RID: 1139
		Default,
		// Token: 0x04000474 RID: 1140
		Finally,
		// Token: 0x04000475 RID: 1141
		New,
		// Token: 0x04000476 RID: 1142
		RightParenthesis,
		// Token: 0x04000477 RID: 1143
		RightBracket,
		// Token: 0x04000478 RID: 1144
		SingleLineComment,
		// Token: 0x04000479 RID: 1145
		MultipleLineComment,
		// Token: 0x0400047A RID: 1146
		UnterminatedComment,
		// Token: 0x0400047B RID: 1147
		PreprocessorDirective,
		// Token: 0x0400047C RID: 1148
		Enum,
		// Token: 0x0400047D RID: 1149
		Extends,
		// Token: 0x0400047E RID: 1150
		Super,
		// Token: 0x0400047F RID: 1151
		Class,
		// Token: 0x04000480 RID: 1152
		Const,
		// Token: 0x04000481 RID: 1153
		Export,
		// Token: 0x04000482 RID: 1154
		Import,
		// Token: 0x04000483 RID: 1155
		Module,
		// Token: 0x04000484 RID: 1156
		Let,
		// Token: 0x04000485 RID: 1157
		Implements,
		// Token: 0x04000486 RID: 1158
		Interface,
		// Token: 0x04000487 RID: 1159
		Package,
		// Token: 0x04000488 RID: 1160
		Private,
		// Token: 0x04000489 RID: 1161
		Protected,
		// Token: 0x0400048A RID: 1162
		Public,
		// Token: 0x0400048B RID: 1163
		Static,
		// Token: 0x0400048C RID: 1164
		Yield,
		// Token: 0x0400048D RID: 1165
		Native,
		// Token: 0x0400048E RID: 1166
		Get,
		// Token: 0x0400048F RID: 1167
		Set,
		// Token: 0x04000490 RID: 1168
		AspNetBlock,
		// Token: 0x04000491 RID: 1169
		ReplacementToken,
		// Token: 0x04000492 RID: 1170
		EndOfLine,
		// Token: 0x04000493 RID: 1171
		WhiteSpace,
		// Token: 0x04000494 RID: 1172
		Error,
		// Token: 0x04000495 RID: 1173
		RegularExpression
	}
}
