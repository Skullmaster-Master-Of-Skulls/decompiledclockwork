using System;
using OracleInternal.Common;
using OracleInternal.TTC.Accessors;

namespace OracleInternal.TTC
{
	// Token: 0x02000222 RID: 546
	internal class TTCDataTypeNegotiation : TTCMessage
	{
		// Token: 0x0600143D RID: 5181 RVA: 0x000D6E70 File Offset: 0x000D5070
		static TTCDataTypeNegotiation()
		{
			TTCDataTypeNegotiation.typeAndRep[0] = 1;
			TTCDataTypeNegotiation.AddTypeRepresentation(1, 0, 1, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(2, 0, 2, 10);
			TTCDataTypeNegotiation.AddTypeRepresentation(8, 0, 8, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(12, 0, 12, 10);
			TTCDataTypeNegotiation.AddTypeRepresentation(23, 0, 23, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(24, 0, 24, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(25, 0, 25, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(26, 0, 26, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(27, 0, 27, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(28, 0, 28, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(29, 0, 29, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(30, 0, 30, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(31, 0, 31, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(32, 0, 32, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(33, 0, 33, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(10, 1, 10, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(11, 1, 11, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(40, 1, 40, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(41, 1, 41, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(117, 1, 117, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(120, 1, 120, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(290, 1, 290, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(291, 1, 291, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(292, 1, 292, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(293, 1, 293, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(294, 1, 294, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(298, 1, 298, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(299, 1, 299, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(300, 1, 300, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(301, 1, 301, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(302, 1, 302, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(303, 1, 303, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(304, 1, 304, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(305, 1, 305, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(306, 1, 306, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(307, 1, 307, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(308, 1, 308, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(309, 1, 309, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(310, 1, 310, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(311, 1, 311, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(312, 1, 312, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(313, 1, 313, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(315, 1, 315, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(316, 1, 316, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(317, 1, 317, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(318, 1, 318, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(319, 1, 319, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(320, 1, 320, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(321, 1, 321, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(322, 1, 322, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(323, 1, 323, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(327, 1, 327, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(328, 1, 328, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(329, 1, 329, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(331, 1, 331, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(333, 1, 333, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(334, 1, 334, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(335, 1, 335, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(336, 1, 336, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(337, 1, 337, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(338, 1, 338, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(339, 1, 339, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(340, 1, 340, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(341, 1, 341, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(342, 1, 342, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(343, 1, 343, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(344, 1, 344, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(345, 1, 345, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(346, 1, 346, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(348, 1, 348, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(349, 1, 349, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(354, 1, 354, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(355, 1, 355, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(359, 1, 359, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(363, 1, 363, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(380, 1, 380, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(381, 1, 381, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(382, 1, 382, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(383, 1, 383, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(384, 1, 384, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(385, 1, 385, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(386, 1, 386, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(387, 1, 387, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(388, 1, 388, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(389, 1, 389, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(390, 1, 390, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(391, 1, 391, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(393, 1, 393, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(394, 1, 394, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(395, 1, 395, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(396, 1, 396, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(397, 1, 397, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(398, 1, 398, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(399, 1, 399, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(400, 1, 400, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(401, 1, 401, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(404, 1, 404, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(405, 1, 405, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(406, 1, 406, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(407, 1, 407, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(413, 1, 413, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(414, 1, 414, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(415, 1, 415, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(416, 1, 416, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(417, 1, 417, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(418, 1, 418, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(419, 1, 419, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(420, 1, 420, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(421, 1, 421, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(422, 1, 422, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(423, 1, 423, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(424, 1, 424, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(425, 1, 425, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(426, 1, 426, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(427, 1, 427, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(429, 1, 429, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(430, 1, 430, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(431, 1, 431, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(432, 1, 432, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(433, 1, 433, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(449, 1, 449, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(450, 1, 450, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(454, 1, 454, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(455, 1, 455, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(456, 1, 456, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(457, 1, 457, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(458, 1, 458, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(459, 1, 459, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(460, 1, 460, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(461, 1, 461, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(462, 1, 462, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(463, 1, 463, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(466, 1, 466, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(467, 1, 467, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(468, 1, 468, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(469, 1, 469, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(470, 1, 470, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(471, 1, 471, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(472, 1, 472, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(473, 1, 473, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(474, 1, 474, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(475, 1, 475, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(476, 1, 476, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(477, 1, 477, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(478, 1, 478, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(479, 1, 479, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(480, 1, 480, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(481, 1, 481, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(482, 1, 482, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(483, 1, 483, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(484, 1, 484, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(485, 1, 485, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(486, 1, 486, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(490, 1, 490, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(491, 1, 491, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(492, 1, 492, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(493, 1, 493, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(494, 1, 494, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(495, 1, 495, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(496, 1, 496, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(498, 1, 498, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(499, 1, 499, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(500, 1, 500, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(501, 1, 501, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(502, 1, 502, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(509, 1, 509, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(510, 1, 510, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(513, 1, 513, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(514, 1, 514, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(516, 1, 516, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(517, 1, 517, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(518, 1, 518, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(519, 1, 519, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(520, 1, 520, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(521, 1, 521, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(522, 1, 522, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(523, 1, 523, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(524, 1, 524, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(525, 1, 525, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(526, 1, 526, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(527, 1, 527, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(528, 1, 528, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(529, 1, 529, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(530, 1, 530, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(531, 1, 531, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(532, 1, 532, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(533, 1, 533, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(534, 1, 534, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(535, 1, 535, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(536, 1, 536, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(537, 1, 537, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(538, 1, 538, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(539, 1, 539, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(540, 1, 540, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(541, 1, 541, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(542, 1, 542, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(543, 1, 543, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(560, 1, 560, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(565, 1, 565, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(572, 1, 572, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(573, 1, 573, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(574, 1, 574, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(575, 1, 575, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(576, 1, 576, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(578, 1, 578, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(563, 1, 563, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(564, 1, 564, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(579, 1, 579, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(580, 1, 580, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(581, 1, 581, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(582, 1, 582, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(583, 1, 583, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(584, 1, 584, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(585, 1, 585, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(3, 0, 2, 10);
			TTCDataTypeNegotiation.AddTypeRepresentation(4, 0, 2, 10);
			TTCDataTypeNegotiation.AddTypeRepresentation(5, 0, 1, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(6, 0, 2, 10);
			TTCDataTypeNegotiation.AddTypeRepresentation(7, 0, 2, 10);
			TTCDataTypeNegotiation.AddTypeRepresentation(9, 0, 1, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(13, 0, 0, 0);
			TTCDataTypeNegotiation.AddTypeRepresentation(14, 0, 0, 0);
			TTCDataTypeNegotiation.AddTypeRepresentation(15, 0, 23, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(16, 0, 0, 0);
			TTCDataTypeNegotiation.AddTypeRepresentation(17, 0, 0, 0);
			TTCDataTypeNegotiation.AddTypeRepresentation(18, 0, 0, 0);
			TTCDataTypeNegotiation.AddTypeRepresentation(19, 0, 0, 0);
			TTCDataTypeNegotiation.AddTypeRepresentation(20, 0, 0, 0);
			TTCDataTypeNegotiation.AddTypeRepresentation(21, 0, 0, 0);
			TTCDataTypeNegotiation.AddTypeRepresentation(22, 0, 0, 0);
			TTCDataTypeNegotiation.AddTypeRepresentation(39, 0, 120, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(58, 0, 0, 0);
			TTCDataTypeNegotiation.AddTypeRepresentation(68, 0, 2, 10);
			TTCDataTypeNegotiation.AddTypeRepresentation(69, 0, 0, 0);
			TTCDataTypeNegotiation.AddTypeRepresentation(70, 0, 0, 0);
			TTCDataTypeNegotiation.AddTypeRepresentation(74, 0, 0, 0);
			TTCDataTypeNegotiation.AddTypeRepresentation(76, 0, 0, 0);
			TTCDataTypeNegotiation.AddTypeRepresentation(91, 0, 2, 10);
			TTCDataTypeNegotiation.AddTypeRepresentation(94, 0, 1, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(95, 0, 23, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(96, 0, 96, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(97, 0, 96, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(100, 0, 100, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(101, 0, 101, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(102, 0, 102, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(104, 0, 11, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(105, 0, 0, 0);
			TTCDataTypeNegotiation.AddTypeRepresentation(106, 0, 106, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(108, 0, 109, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(109, 0, 109, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(110, 0, 111, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(111, 0, 111, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(112, 0, 112, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(113, 0, 113, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(114, 0, 114, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(115, 0, 115, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(116, 0, 102, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(118, 0, 0, 0);
			TTCDataTypeNegotiation.AddTypeRepresentation(119, 0, 0, 0);
			TTCDataTypeNegotiation.AddTypeRepresentation(121, 0, 0, 0);
			TTCDataTypeNegotiation.AddTypeRepresentation(122, 0, 0, 0);
			TTCDataTypeNegotiation.AddTypeRepresentation(123, 0, 0, 0);
			TTCDataTypeNegotiation.AddTypeRepresentation(136, 0, 0, 0);
			TTCDataTypeNegotiation.AddTypeRepresentation(146, 0, 146, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(147, 0, 0, 0);
			TTCDataTypeNegotiation.AddTypeRepresentation(152, 0, 2, 10);
			TTCDataTypeNegotiation.AddTypeRepresentation(153, 0, 2, 10);
			TTCDataTypeNegotiation.AddTypeRepresentation(154, 0, 2, 10);
			TTCDataTypeNegotiation.AddTypeRepresentation(155, 0, 1, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(156, 0, 12, 10);
			TTCDataTypeNegotiation.AddTypeRepresentation(172, 0, 2, 10);
			TTCDataTypeNegotiation.AddTypeRepresentation(178, 0, 178, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(179, 0, 179, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(180, 0, 180, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(181, 0, 181, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(182, 0, 182, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(183, 0, 183, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(184, 0, 12, 10);
			TTCDataTypeNegotiation.AddTypeRepresentation(185, 0, 185, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(186, 0, 186, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(187, 0, 187, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(188, 0, 188, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(189, 0, 189, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(190, 0, 190, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(191, 0, 0, 0);
			TTCDataTypeNegotiation.AddTypeRepresentation(192, 0, 0, 0);
			TTCDataTypeNegotiation.AddTypeRepresentation(195, 0, 112, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(196, 0, 113, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(197, 0, 114, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(208, 0, 208, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(209, 0, 0, 0);
			TTCDataTypeNegotiation.AddTypeRepresentation(231, 0, 231, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(232, 0, 231, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(233, 0, 233, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(252, 0, 252, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(241, 0, 109, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(515, 0, 0, 0);
			TTCDataTypeNegotiation.typeAndRepFor1100 = new short[(int)TTCDataTypeNegotiation.typeAndRep[0]];
			Array.Copy(TTCDataTypeNegotiation.typeAndRep, 0, TTCDataTypeNegotiation.typeAndRepFor1100, 0, (int)TTCDataTypeNegotiation.typeAndRep[0]);
			TTCDataTypeNegotiation.AddTypeRepresentation(590, 1, 590, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(591, 1, 591, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(592, 1, 592, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(613, 1, 613, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(614, 1, 614, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(615, 1, 615, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(616, 1, 616, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(611, 1, 611, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(612, 1, 612, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(593, 1, 593, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(594, 1, 594, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(595, 1, 595, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(596, 1, 596, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(597, 1, 597, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(598, 1, 598, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(599, 1, 599, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(600, 1, 600, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(601, 1, 601, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(602, 1, 602, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(603, 1, 603, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(604, 1, 604, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(605, 1, 605, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(622, 1, 622, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(623, 1, 623, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(624, 1, 624, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(625, 1, 625, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(626, 1, 626, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(627, 1, 627, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(628, 1, 628, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(629, 1, 629, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(630, 1, 630, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(631, 1, 631, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(632, 1, 632, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(637, 1, 637, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(638, 1, 638, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(636, 1, 636, 1);
			TTCDataTypeNegotiation.typeAndRepFor1200 = new short[(int)TTCDataTypeNegotiation.typeAndRep[0]];
			Array.Copy(TTCDataTypeNegotiation.typeAndRep, 0, TTCDataTypeNegotiation.typeAndRepFor1200, 0, (int)TTCDataTypeNegotiation.typeAndRep[0]);
			TTCDataTypeNegotiation.AddTypeRepresentation(639, 1, 639, 1);
			TTCDataTypeNegotiation.AddTypeRepresentation(640, 1, 640, 1);
		}

		// Token: 0x0600143E RID: 5182 RVA: 0x000D8344 File Offset: 0x000D6544
		private static void AddTypeRepresentation(short dty, short ttctype, short ndty, short rep)
		{
			if (TTCDataTypeNegotiation.typeAndRep.Length < (int)(TTCDataTypeNegotiation.typeAndRep[0] + 4))
			{
				short[] destinationArray = new short[TTCDataTypeNegotiation.typeAndRep.Length * 2];
				Array.Copy(TTCDataTypeNegotiation.typeAndRep, 0, destinationArray, 0, (int)(TTCDataTypeNegotiation.typeAndRep[0] + 1));
				TTCDataTypeNegotiation.typeAndRep = null;
				TTCDataTypeNegotiation.typeAndRep = destinationArray;
			}
			int num = (int)TTCDataTypeNegotiation.typeAndRep[0];
			TTCDataTypeNegotiation.typeAndRep[num] = dty;
			TTCDataTypeNegotiation.typeAndRep[num + 1] = ndty;
			if (ndty == 0)
			{
				TTCDataTypeNegotiation.typeAndRep[0] = (short)(num + 2);
				return;
			}
			TTCDataTypeNegotiation.typeAndRep[num + 2] = rep;
			TTCDataTypeNegotiation.typeAndRep[num + 3] = 0;
			TTCDataTypeNegotiation.typeAndRep[0] = (short)(num + 4);
		}

		// Token: 0x0600143F RID: 5183 RVA: 0x000D83DC File Offset: 0x000D65DC
		internal TTCDataTypeNegotiation(MarshallingEngine marshallingEngine)
		{
			byte[] array = new byte[7];
			array[0] = 2;
			array[1] = 1;
			this.m_RuntimeCapabilities = array;
			base..ctor(marshallingEngine, 2);
		}

		// Token: 0x06001440 RID: 5184 RVA: 0x000D8420 File Offset: 0x000D6620
		internal void Initialize(byte[] serverCompileTimeCap, byte[] serverRunTimeCap, short serverCharacterSet, short serverNCharSet, byte serverFlags)
		{
			if (serverCompileTimeCap == null || serverCompileTimeCap.Length <= 27 || serverCompileTimeCap[27] == 0)
			{
				this.m_CompileTimeCapabilities[27] = 0;
			}
			if (serverCompileTimeCap != null && serverCompileTimeCap[7] >= 8 && ConfigBaseClass.m_XMLTypeClientSideDecoding)
			{
				this.m_CompileTimeCapabilities[36] = 4;
			}
			else if (serverCompileTimeCap != null && serverCompileTimeCap[7] < 7)
			{
				this.m_CompileTimeCapabilities[36] = 0;
			}
			this.m_clientRemoteIn = serverCharacterSet;
			this.m_clientRemoteOut = serverCharacterSet;
			this.m_ncharSetId = serverNCharSet;
			this.m_clientFlags = serverFlags;
			if (serverRunTimeCap == null || serverRunTimeCap.Length < 1 || (serverRunTimeCap[1] & 1) != 1)
			{
				byte[] runtimeCapabilities = this.m_RuntimeCapabilities;
				int num = 1;
				byte b = runtimeCapabilities[num];
				runtimeCapabilities[num] = (byte)0;
			}
			if (serverRunTimeCap != null && serverRunTimeCap.Length > 6 && (serverRunTimeCap[6] & 4) == 4)
			{
				byte[] runtimeCapabilities2 = this.m_RuntimeCapabilities;
				int num2 = 6;
				runtimeCapabilities2[num2] |= 4;
				this.m_b32kTypeSupported = true;
			}
			if (serverRunTimeCap != null && serverRunTimeCap.Length > 6 && (serverRunTimeCap[6] & 2) == 2)
			{
				byte[] runtimeCapabilities3 = this.m_RuntimeCapabilities;
				int num3 = 6;
				runtimeCapabilities3[num3] |= 2;
			}
			if (serverCompileTimeCap == null || serverCompileTimeCap.Length <= 37 || (serverCompileTimeCap[37] & 2) != 2)
			{
				this.m_CompileTimeCapabilities[37] = 0;
				this.m_CompileTimeCapabilities[1] = 0;
			}
			if (serverCompileTimeCap != null && serverCompileTimeCap.Length > 7)
			{
				if (serverCompileTimeCap[7] >= 8)
				{
					this.runtimeTypeAndRep = TTCDataTypeNegotiation.typeAndRep;
					return;
				}
				if (serverCompileTimeCap[7] >= 7)
				{
					this.runtimeTypeAndRep = TTCDataTypeNegotiation.typeAndRepFor1200;
					return;
				}
				this.runtimeTypeAndRep = TTCDataTypeNegotiation.typeAndRepFor1100;
			}
		}

		// Token: 0x06001441 RID: 5185 RVA: 0x000D857C File Offset: 0x000D677C
		internal override void ReInit(MarshallingEngine marshallingEngine)
		{
			base.ReInit(marshallingEngine);
			this.m_dbTimeZoneBytes = null;
			this.m_sendTZDataAsLocalTime = false;
		}

		// Token: 0x06001442 RID: 5186 RVA: 0x000D8594 File Offset: 0x000D6794
		private void MarshalTypeReps()
		{
			if (this.m_CompileTimeCapabilities[27] == 0)
			{
				for (int i = 1; i < (int)this.runtimeTypeAndRep[0]; i++)
				{
					this.m_marshallingEngine.MarshalUB1((short)((byte)(this.runtimeTypeAndRep[i] & 255)));
				}
				this.m_marshallingEngine.MarshalUB1(0);
				return;
			}
			byte b = this.m_marshallingEngine.m_typeRepresentation.m_representationArray[1];
			this.m_marshallingEngine.m_typeRepresentation.m_representationArray[1] = 0;
			for (int j = 1; j < (int)this.runtimeTypeAndRep[0]; j++)
			{
				this.m_marshallingEngine.MarshalUB2((int)this.runtimeTypeAndRep[j]);
			}
			this.m_marshallingEngine.MarshalUB2(0);
			this.m_marshallingEngine.m_typeRepresentation.m_representationArray[1] = b;
		}

		// Token: 0x06001443 RID: 5187 RVA: 0x000D8654 File Offset: 0x000D6854
		internal void WriteMessage(byte[] serverCompileTimeCap, byte[] serverRunTimeCap, short serverCharacterSet, short serverNCharSet, byte serverFlags)
		{
			this.Initialize(serverCompileTimeCap, serverRunTimeCap, serverCharacterSet, serverNCharSet, serverFlags);
			base.WriteTTCCode();
			this.m_marshallingEngine.MarshalUB2((int)this.m_clientRemoteIn);
			this.m_marshallingEngine.MarshalUB2((int)this.m_clientRemoteOut);
			this.m_marshallingEngine.MarshalUB1((short)this.m_clientFlags);
			this.m_marshallingEngine.MarshalUB1((short)this.m_CompileTimeCapabilities.Length);
			this.m_marshallingEngine.MarshalB1Array(this.m_CompileTimeCapabilities);
			this.m_marshallingEngine.MarshalUB1((short)this.m_RuntimeCapabilities.Length);
			this.m_marshallingEngine.MarshalB1Array(this.m_RuntimeCapabilities);
			if ((this.m_RuntimeCapabilities[1] & 1) == 1)
			{
				this.m_marshallingEngine.MarshalB1Array(TTCDataTypeNegotiation.GetTZBytes());
				if ((this.m_CompileTimeCapabilities[37] & 2) == 2)
				{
					byte[] bytes = BitConverter.GetBytes(TTCDataTypeNegotiation.s_latestTZVersion);
					Array.Reverse(bytes, 0, bytes.Length);
					this.m_marshallingEngine.MarshalB1Array(bytes);
				}
			}
			this.m_marshallingEngine.MarshalUB2((int)this.m_ncharSetId);
			this.MarshalTypeReps();
		}

		// Token: 0x06001444 RID: 5188 RVA: 0x000D8754 File Offset: 0x000D6954
		internal static byte[] GetTZBytes()
		{
			TimeZone currentTimeZone = TimeZone.CurrentTimeZone;
			double totalMilliseconds = currentTimeZone.GetUtcOffset(DateTime.Today).TotalMilliseconds;
			int num = (int)(totalMilliseconds / 3600000.0);
			int num2 = (int)(totalMilliseconds / 60000.0 % 60.0);
			int num3 = (int)(totalMilliseconds / 1000.0 % 60.0);
			byte[] array = new byte[11];
			array[0] = 128;
			array[4] = (byte)(num + 60 & 255);
			array[5] = (byte)(num2 + 60 & 255);
			array[6] = (byte)(num3 + 60 & 255);
			array[7] = 128;
			return array;
		}

		// Token: 0x06001445 RID: 5189 RVA: 0x000D8808 File Offset: 0x000D6A08
		internal void ReadResponse()
		{
			if ((byte)this.m_marshallingEngine.UnmarshalUB1(false) != 2)
			{
				throw new Exception("TTC Error");
			}
			if (!this.ValidateTypeRepresentations())
			{
				throw new Exception("TTC Error");
			}
			this.SetBasicTypeRepresentations(this.m_marshallingEngine.m_typeRepresentation);
		}

		// Token: 0x06001446 RID: 5190 RVA: 0x000D8854 File Offset: 0x000D6A54
		private bool ValidateTypeRepresentations()
		{
			bool flag = false;
			int num = 0;
			byte b = 0;
			if (this.m_CompileTimeCapabilities[27] == 1)
			{
				b = this.m_marshallingEngine.m_typeRepresentation.m_representationArray[1];
				this.m_marshallingEngine.m_typeRepresentation.m_representationArray[1] = 0;
			}
			if (this.m_RuntimeCapabilities[1] == 1)
			{
				this.m_dbTimeZoneBytes = this.m_marshallingEngine.UnmarshalNBytes(TTCIntervalTypeAccessor.INTERVALTYPE_MAX_LENGTH);
				if ((this.m_CompileTimeCapabilities[37] & 2) == 2)
				{
					byte[] array = this.m_marshallingEngine.UnmarshalNBytes(4);
					int num2 = (int)(array[0] & byte.MaxValue) << 24 | (int)(array[1] & byte.MaxValue) << 16 | (int)(array[2] & byte.MaxValue) << 8 | (int)(array[3] & byte.MaxValue);
					if (num2 != TTCDataTypeNegotiation.s_latestTZVersion)
					{
						this.m_sendTZDataAsLocalTime = true;
					}
				}
			}
			for (;;)
			{
				short num3;
				if (this.m_CompileTimeCapabilities[27] == 1)
				{
					num3 = (short)this.m_marshallingEngine.UnmarshalUB2(false);
				}
				else
				{
					num3 = this.m_marshallingEngine.UnmarshalUB1(false);
				}
				if (!flag)
				{
					if (num3 == 0)
					{
						break;
					}
					flag = true;
				}
				else
				{
					switch (num)
					{
					case 0:
						if (num3 == 0)
						{
							flag = false;
						}
						else
						{
							num = 1;
						}
						break;
					case 1:
						num = 0;
						break;
					}
				}
			}
			if (this.m_CompileTimeCapabilities[27] == 1)
			{
				this.m_marshallingEngine.m_typeRepresentation.m_representationArray[1] = b;
			}
			return true;
		}

		// Token: 0x06001447 RID: 5191 RVA: 0x000D899C File Offset: 0x000D6B9C
		private void SetBasicTypeRepresentations(TTCTypeRepresentation types)
		{
			types.m_representationArray[0] = 0;
			types.m_representationArray[1] = 1;
			types.m_representationArray[2] = 1;
			types.m_representationArray[3] = 1;
			types.m_representationArray[4] = 1;
		}

		// Token: 0x04001664 RID: 5732
		internal const byte TTC_FLD_VSN_820 = 1;

		// Token: 0x04001665 RID: 5733
		internal const byte TTC_FLD_VSN_902 = 2;

		// Token: 0x04001666 RID: 5734
		internal const byte TTC_FLD_VSN_1000 = 3;

		// Token: 0x04001667 RID: 5735
		internal const byte TTC_FLD_VSN_1020 = 4;

		// Token: 0x04001668 RID: 5736
		internal const byte TTC_FLD_VSN_1100 = 5;

		// Token: 0x04001669 RID: 5737
		internal const byte TTC_FLD_VSN_1120 = 6;

		// Token: 0x0400166A RID: 5738
		internal const byte TTC_FLD_VSN_1200 = 7;

		// Token: 0x0400166B RID: 5739
		internal const byte TTC_FLD_VSN_1220 = 8;

		// Token: 0x0400166C RID: 5740
		private const byte TTC_FLD_VSN_MAX = 8;

		// Token: 0x0400166D RID: 5741
		internal const byte KPCCAP_TTC_VSN_OFFSET = 7;

		// Token: 0x0400166E RID: 5742
		private const byte KPULMAXL = 6;

		// Token: 0x0400166F RID: 5743
		internal const byte KPCCAP_CTB_TTC1_EOCS = 1;

		// Token: 0x04001670 RID: 5744
		private const byte KPCCAP_CTB_TTC1_INRC = 8;

		// Token: 0x04001671 RID: 5745
		private const byte KPCCAP_CTB_TTC1_FBVC = 32;

		// Token: 0x04001672 RID: 5746
		internal const byte KPCCAP_CTB_OCI1_FSAP = 16;

		// Token: 0x04001673 RID: 5747
		private const byte KPCCAP_CTB_OCI1_APCTX = 128;

		// Token: 0x04001674 RID: 5748
		private const byte KOLE_LOB_CAP_UB8_SIZE = 1;

		// Token: 0x04001675 RID: 5749
		private const byte KOLE_LOB_CAP_ENCS = 2;

		// Token: 0x04001676 RID: 5750
		private const byte KOLE_LOB_CAP_DIL = 4;

		// Token: 0x04001677 RID: 5751
		private const byte KOLE_LOB_CAP_TMPLOC_SZ = 8;

		// Token: 0x04001678 RID: 5752
		private const byte KOLE_LOB_CAP_ARRAY = 32;

		// Token: 0x04001679 RID: 5753
		private const byte KOLE_LOB_CAP_PRFCH = 64;

		// Token: 0x0400167A RID: 5754
		private const byte KOLE_LOB_CAP_12C = 128;

		// Token: 0x0400167B RID: 5755
		private const byte KOLE_LOB_CAP_ALL = 235;

		// Token: 0x0400167C RID: 5756
		private const byte KPCCAP_CT_AQ_PROP_DQA = 1;

		// Token: 0x0400167D RID: 5757
		private const byte KPCCAP_CT_AQ_BUFQ = 2;

		// Token: 0x0400167E RID: 5758
		private const byte KPCCAP_CT_AQ_BPROP_RCV = 4;

		// Token: 0x0400167F RID: 5759
		private const byte KOPT_VNFT = 3;

		// Token: 0x04001680 RID: 5760
		private const byte KPCCAP_CTB_TTC2_ZLNP = 4;

		// Token: 0x04001681 RID: 5761
		private const byte KPCCAP_CTB_TTC2_NCPR = 1;

		// Token: 0x04001682 RID: 5762
		private const byte ZTVOV_KPCLOG_O30L = 0;

		// Token: 0x04001683 RID: 5763
		private const byte ZTVOV_KPCLOG_O3L = 1;

		// Token: 0x04001684 RID: 5764
		internal const byte ZTVOV_KPCLOG_O5L_NP = 2;

		// Token: 0x04001685 RID: 5765
		private const byte ZTVOV_KPCLOG_O4L = 4;

		// Token: 0x04001686 RID: 5766
		private const byte ZTVOV_KPCLOG_O5L = 8;

		// Token: 0x04001687 RID: 5767
		private const byte ZTVOV_KPCLOG_O6L = 16;

		// Token: 0x04001688 RID: 5768
		private const byte KZTVOV_KPCLOG_O7L_MR = 32;

		// Token: 0x04001689 RID: 5769
		private const int KZTVOV_KPCLOG_O8L_LI = 64;

		// Token: 0x0400168A RID: 5770
		internal const int KPCCAP_CTB_TTC3_COLMETADATA = 1;

		// Token: 0x0400168B RID: 5771
		private const byte KPCCAP_CTB_TTC3_TZVER = 2;

		// Token: 0x0400168C RID: 5772
		internal const int KPCCAP_CTB_TTC3_LTXID = 8;

		// Token: 0x0400168D RID: 5773
		internal const int KPCCAP_CTB_TTC3_IMPLRES = 16;

		// Token: 0x0400168E RID: 5774
		internal const int KPCCAP_CTB_TTC3_BIGCHUNK_CLR = 32;

		// Token: 0x0400168F RID: 5775
		internal const int KPCCAP_CTB_TTC3_KEEP_OUT_ORDER = 128;

		// Token: 0x04001690 RID: 5776
		internal const int KPCCAP_CTB_TTC4_RENEG = 1;

		// Token: 0x04001691 RID: 5777
		internal const int KPCCAP_CT_UB2DTY = 27;

		// Token: 0x04001692 RID: 5778
		internal const int KPCCAP_CTB_TTC3 = 37;

		// Token: 0x04001693 RID: 5779
		internal const int KPCCAP_CTB_XML = 36;

		// Token: 0x04001694 RID: 5780
		internal const int KPCCAP_CTB_XML_UNSET = 0;

		// Token: 0x04001695 RID: 5781
		internal const int KPCCAP_CTB_XML_CSXXMLT = 1;

		// Token: 0x04001696 RID: 5782
		internal const int KPCCAP_CTB_XML_LOBSTR_IMG_ONLY = 2;

		// Token: 0x04001697 RID: 5783
		internal const int KPCCAP_CTB_XML_LOBTOKENBASED_IMG_ONLY = 4;

		// Token: 0x04001698 RID: 5784
		internal const int KPCCAP_CTB_OCI2 = 31;

		// Token: 0x04001699 RID: 5785
		internal const int KPCCAP_CTB_OCI2_SCTAF = 1;

		// Token: 0x0400169A RID: 5786
		internal const int KPCCAP_CTB_OCI2_FTHDR = 2;

		// Token: 0x0400169B RID: 5787
		internal const int KPCCAP_CTB_OCI2_CQC = 4;

		// Token: 0x0400169C RID: 5788
		internal const int KPCCAP_CTB_OCI2_EDITION = 8;

		// Token: 0x0400169D RID: 5789
		internal const int KPCCAP_CTB_OCI2_SRVCP = 16;

		// Token: 0x0400169E RID: 5790
		private const int KPCCAP_RTB_TTC = 6;

		// Token: 0x0400169F RID: 5791
		private const int KPCCAP_RTB_TTC_ZCPY = 1;

		// Token: 0x040016A0 RID: 5792
		private const int KPCCAP_RTB_TTC_TZLT = 2;

		// Token: 0x040016A1 RID: 5793
		private const int KPCCAP_RTB_TTC_32K = 4;

		// Token: 0x040016A2 RID: 5794
		private const short DTYEXPBASE = 256;

		// Token: 0x040016A3 RID: 5795
		private const short DTY0 = 0;

		// Token: 0x040016A4 RID: 5796
		private const short DTYCHR = 1;

		// Token: 0x040016A5 RID: 5797
		private const short DTYNUM = 2;

		// Token: 0x040016A6 RID: 5798
		private const short DTYINT = 3;

		// Token: 0x040016A7 RID: 5799
		private const short DTYFLT = 4;

		// Token: 0x040016A8 RID: 5800
		private const short DTYSTR = 5;

		// Token: 0x040016A9 RID: 5801
		private const short DTYVNU = 6;

		// Token: 0x040016AA RID: 5802
		private const short DTYPDN = 7;

		// Token: 0x040016AB RID: 5803
		private const short DTYLNG = 8;

		// Token: 0x040016AC RID: 5804
		private const short DTYVCS = 9;

		// Token: 0x040016AD RID: 5805
		private const short DTYTI5 = 10;

		// Token: 0x040016AE RID: 5806
		private const short DTYRID = 11;

		// Token: 0x040016AF RID: 5807
		private const short DTYDAT = 12;

		// Token: 0x040016B0 RID: 5808
		private const short DTYIDT = 13;

		// Token: 0x040016B1 RID: 5809
		private const short DTYIJU = 14;

		// Token: 0x040016B2 RID: 5810
		private const short DTYVBI = 15;

		// Token: 0x040016B3 RID: 5811
		private const short DTYDIF = 16;

		// Token: 0x040016B4 RID: 5812
		private const short DTYDOF = 17;

		// Token: 0x040016B5 RID: 5813
		private const short DTYDTZ = 18;

		// Token: 0x040016B6 RID: 5814
		private const short DTYDYN = 19;

		// Token: 0x040016B7 RID: 5815
		private const short DTYDPC = 20;

		// Token: 0x040016B8 RID: 5816
		private const short DTYBFLOAT = 21;

		// Token: 0x040016B9 RID: 5817
		private const short DTYBDOUBLE = 22;

		// Token: 0x040016BA RID: 5818
		internal const short DTYBIN = 23;

		// Token: 0x040016BB RID: 5819
		private const short DTYUB1 = 23;

		// Token: 0x040016BC RID: 5820
		private const short DTYLBI = 24;

		// Token: 0x040016BD RID: 5821
		private const short DTYUB2 = 25;

		// Token: 0x040016BE RID: 5822
		private const short DTYUB4 = 26;

		// Token: 0x040016BF RID: 5823
		private const short DTYB1 = 27;

		// Token: 0x040016C0 RID: 5824
		private const short DTYB2 = 28;

		// Token: 0x040016C1 RID: 5825
		private const short DTYB4 = 29;

		// Token: 0x040016C2 RID: 5826
		private const short DTYSB4 = 29;

		// Token: 0x040016C3 RID: 5827
		private const short DTYWORD = 30;

		// Token: 0x040016C4 RID: 5828
		private const short DTYUWORD = 31;

		// Token: 0x040016C5 RID: 5829
		private const short DTYIIN = 29;

		// Token: 0x040016C6 RID: 5830
		private const short DTYCURID = 25;

		// Token: 0x040016C7 RID: 5831
		private const short DTYAMID = 26;

		// Token: 0x040016C8 RID: 5832
		private const short DTYDBA = 26;

		// Token: 0x040016C9 RID: 5833
		private const short DTYPTN = 26;

		// Token: 0x040016CA RID: 5834
		private const short DTYPB = 32;

		// Token: 0x040016CB RID: 5835
		private const short DTYPW = 33;

		// Token: 0x040016CC RID: 5836
		private const short DTYOER8 = 290;

		// Token: 0x040016CD RID: 5837
		private const short DTYFUN = 291;

		// Token: 0x040016CE RID: 5838
		private const short DTYAUA = 292;

		// Token: 0x040016CF RID: 5839
		private const short DTYRXH7 = 293;

		// Token: 0x040016D0 RID: 5840
		private const short DTYNA6 = 294;

		// Token: 0x040016D1 RID: 5841
		private const short DTYOAC = 39;

		// Token: 0x040016D2 RID: 5842
		private const short DTYAMS = 40;

		// Token: 0x040016D3 RID: 5843
		private const short DTYBRN = 41;

		// Token: 0x040016D4 RID: 5844
		private const short DTYBRP = 298;

		// Token: 0x040016D5 RID: 5845
		private const short DTYBRV = 299;

		// Token: 0x040016D6 RID: 5846
		private const short DTYKVA = 300;

		// Token: 0x040016D7 RID: 5847
		private const short DTYCLS = 301;

		// Token: 0x040016D8 RID: 5848
		private const short DTYCUI = 302;

		// Token: 0x040016D9 RID: 5849
		private const short DTYDFN = 303;

		// Token: 0x040016DA RID: 5850
		private const short DTYDQR = 304;

		// Token: 0x040016DB RID: 5851
		private const short DTYDSC = 305;

		// Token: 0x040016DC RID: 5852
		private const short DTYEXE = 306;

		// Token: 0x040016DD RID: 5853
		private const short DTYFCH = 307;

		// Token: 0x040016DE RID: 5854
		private const short DTYGBV = 308;

		// Token: 0x040016DF RID: 5855
		private const short DTYGEM = 309;

		// Token: 0x040016E0 RID: 5856
		private const short DTYGIV = 310;

		// Token: 0x040016E1 RID: 5857
		private const short DTYOKG = 311;

		// Token: 0x040016E2 RID: 5858
		private const short DTYHMI = 312;

		// Token: 0x040016E3 RID: 5859
		private const short DTYINO = 313;

		// Token: 0x040016E4 RID: 5860
		private const short DTYOPQ = 58;

		// Token: 0x040016E5 RID: 5861
		private const short DTYLNF = 315;

		// Token: 0x040016E6 RID: 5862
		private const short DTYONT = 316;

		// Token: 0x040016E7 RID: 5863
		private const short DTYOPE = 317;

		// Token: 0x040016E8 RID: 5864
		private const short DTYOSQ = 318;

		// Token: 0x040016E9 RID: 5865
		private const short DTYSFE = 319;

		// Token: 0x040016EA RID: 5866
		private const short DTYSPF = 320;

		// Token: 0x040016EB RID: 5867
		private const short DTYVSN = 321;

		// Token: 0x040016EC RID: 5868
		private const short DTYUD7 = 322;

		// Token: 0x040016ED RID: 5869
		private const short DTYDSA = 323;

		// Token: 0x040016EE RID: 5870
		private const short DTYUIN = 68;

		// Token: 0x040016EF RID: 5871
		private const short DTYBRI = 69;

		// Token: 0x040016F0 RID: 5872
		private const short DTY70 = 70;

		// Token: 0x040016F1 RID: 5873
		private const short DTYPIN = 327;

		// Token: 0x040016F2 RID: 5874
		private const short DTYPFN = 328;

		// Token: 0x040016F3 RID: 5875
		private const short DTYPPT = 329;

		// Token: 0x040016F4 RID: 5876
		private const short DTYOCU = 74;

		// Token: 0x040016F5 RID: 5877
		private const short DTYSTO = 331;

		// Token: 0x040016F6 RID: 5878
		private const short DTY76 = 76;

		// Token: 0x040016F7 RID: 5879
		private const short DTYARC = 333;

		// Token: 0x040016F8 RID: 5880
		private const short DTYMRS = 334;

		// Token: 0x040016F9 RID: 5881
		private const short DTYMRT = 335;

		// Token: 0x040016FA RID: 5882
		private const short DTYMRG = 336;

		// Token: 0x040016FB RID: 5883
		private const short DTYMRR = 337;

		// Token: 0x040016FC RID: 5884
		private const short DTYMRC = 338;

		// Token: 0x040016FD RID: 5885
		private const short DTYVER = 339;

		// Token: 0x040016FE RID: 5886
		private const short DTYLON2 = 340;

		// Token: 0x040016FF RID: 5887
		private const short DTYINO2 = 341;

		// Token: 0x04001700 RID: 5888
		private const short DTYALL = 342;

		// Token: 0x04001701 RID: 5889
		private const short DTYUDB = 343;

		// Token: 0x04001702 RID: 5890
		private const short DTYAQI = 344;

		// Token: 0x04001703 RID: 5891
		private const short DTYULB = 345;

		// Token: 0x04001704 RID: 5892
		private const short DTYULD = 346;

		// Token: 0x04001705 RID: 5893
		private const short DTYSLS = 91;

		// Token: 0x04001706 RID: 5894
		private const short DTYSID = 348;

		// Token: 0x04001707 RID: 5895
		private const short DTYNA7 = 349;

		// Token: 0x04001708 RID: 5896
		private const short DTYLVC = 94;

		// Token: 0x04001709 RID: 5897
		private const short DTYLVB = 95;

		// Token: 0x0400170A RID: 5898
		private const short DTYAFC = 96;

		// Token: 0x0400170B RID: 5899
		private const short DTYAVC = 97;

		// Token: 0x0400170C RID: 5900
		private const short DTYAL7 = 354;

		// Token: 0x0400170D RID: 5901
		private const short DTYK2RPC = 355;

		// Token: 0x0400170E RID: 5902
		private const short DTYIBFLOAT = 100;

		// Token: 0x0400170F RID: 5903
		private const short DTYIBDOUBLE = 101;

		// Token: 0x04001710 RID: 5904
		private const short DTYCUR = 102;

		// Token: 0x04001711 RID: 5905
		private const short DTYXDP = 359;

		// Token: 0x04001712 RID: 5906
		private const short DTYRDD = 104;

		// Token: 0x04001713 RID: 5907
		private const short DTYARR = 70;

		// Token: 0x04001714 RID: 5908
		private const short DTYVAR = 76;

		// Token: 0x04001715 RID: 5909
		private const short DTYLAB = 105;

		// Token: 0x04001716 RID: 5910
		private const short DTYOSL = 106;

		// Token: 0x04001717 RID: 5911
		private const short DTYOKO8 = 363;

		// Token: 0x04001718 RID: 5912
		private const short DTYNTY = 108;

		// Token: 0x04001719 RID: 5913
		private const short DTYINTY = 109;

		// Token: 0x0400171A RID: 5914
		private const short DTYREF = 110;

		// Token: 0x0400171B RID: 5915
		private const short DTYIREF = 111;

		// Token: 0x0400171C RID: 5916
		private const short DTYCLOB = 112;

		// Token: 0x0400171D RID: 5917
		private const short DTYBLOB = 113;

		// Token: 0x0400171E RID: 5918
		private const short DTYBFIL = 114;

		// Token: 0x0400171F RID: 5919
		private const short DTYFILE = 114;

		// Token: 0x04001720 RID: 5920
		private const short DTYCFIL = 115;

		// Token: 0x04001721 RID: 5921
		private const short DTYRSET = 116;

		// Token: 0x04001722 RID: 5922
		private const short DTYCWD = 117;

		// Token: 0x04001723 RID: 5923
		private const short DTYSVT = 118;

		// Token: 0x04001724 RID: 5924
		private const short DTYISVT = 119;

		// Token: 0x04001725 RID: 5925
		private const short DTYNAC = 120;

		// Token: 0x04001726 RID: 5926
		private const short DTYADT = 121;

		// Token: 0x04001727 RID: 5927
		private const short DTYNTB = 122;

		// Token: 0x04001728 RID: 5928
		private const short DTYNAR = 123;

		// Token: 0x04001729 RID: 5929
		private const short DTYUD12 = 380;

		// Token: 0x0400172A RID: 5930
		private const short DTYAL8 = 381;

		// Token: 0x0400172B RID: 5931
		private const short DTYLFOP = 382;

		// Token: 0x0400172C RID: 5932
		private const short DTYFCRT = 383;

		// Token: 0x0400172D RID: 5933
		private const short DTYDNY = 384;

		// Token: 0x0400172E RID: 5934
		private const short DTYOPR = 385;

		// Token: 0x0400172F RID: 5935
		private const short DTYPLS = 386;

		// Token: 0x04001730 RID: 5936
		private const short DTYXID = 387;

		// Token: 0x04001731 RID: 5937
		private const short DTYTXN = 388;

		// Token: 0x04001732 RID: 5938
		private const short DTYDCB = 389;

		// Token: 0x04001733 RID: 5939
		private const short DTYCCA = 390;

		// Token: 0x04001734 RID: 5940
		private const short DTYWRN = 391;

		// Token: 0x04001735 RID: 5941
		private const short DTYOBJ = 136;

		// Token: 0x04001736 RID: 5942
		private const short DTYTLH = 393;

		// Token: 0x04001737 RID: 5943
		private const short DTYTOH = 394;

		// Token: 0x04001738 RID: 5944
		private const short DTYFOI = 395;

		// Token: 0x04001739 RID: 5945
		private const short DTYSID2 = 396;

		// Token: 0x0400173A RID: 5946
		private const short DTYTCH = 397;

		// Token: 0x0400173B RID: 5947
		private const short DTYPII = 398;

		// Token: 0x0400173C RID: 5948
		private const short DTYPFI = 399;

		// Token: 0x0400173D RID: 5949
		private const short DTYPPU = 400;

		// Token: 0x0400173E RID: 5950
		private const short DTYPTE = 401;

		// Token: 0x0400173F RID: 5951
		private const short DTYCLV = 146;

		// Token: 0x04001740 RID: 5952
		private const short DTYBLV = 147;

		// Token: 0x04001741 RID: 5953
		private const short DTYRXH8 = 404;

		// Token: 0x04001742 RID: 5954
		private const short DTYTN12 = 405;

		// Token: 0x04001743 RID: 5955
		private const short DTYAUTH = 406;

		// Token: 0x04001744 RID: 5956
		private const short DTYKVAL = 407;

		// Token: 0x04001745 RID: 5957
		private const short DTYDTR = 152;

		// Token: 0x04001746 RID: 5958
		private const short DTYDUN = 153;

		// Token: 0x04001747 RID: 5959
		private const short DTYDOP = 154;

		// Token: 0x04001748 RID: 5960
		private const short DTYVST = 155;

		// Token: 0x04001749 RID: 5961
		private const short DTYODT = 156;

		// Token: 0x0400174A RID: 5962
		private const short DTYFGI = 413;

		// Token: 0x0400174B RID: 5963
		private const short DTYDSY = 414;

		// Token: 0x0400174C RID: 5964
		private const short DTYDSYR8 = 415;

		// Token: 0x0400174D RID: 5965
		private const short DTYDSYH8 = 416;

		// Token: 0x0400174E RID: 5966
		private const short DTYDSYL = 417;

		// Token: 0x0400174F RID: 5967
		private const short DTYDSYT8 = 418;

		// Token: 0x04001750 RID: 5968
		private const short DTYDSYV8 = 419;

		// Token: 0x04001751 RID: 5969
		private const short DTYDSYP = 420;

		// Token: 0x04001752 RID: 5970
		private const short DTYDSYF = 421;

		// Token: 0x04001753 RID: 5971
		private const short DTYDSYK = 422;

		// Token: 0x04001754 RID: 5972
		private const short DTYDSYY = 423;

		// Token: 0x04001755 RID: 5973
		private const short DTYDSYQ = 424;

		// Token: 0x04001756 RID: 5974
		private const short DTYDSYC = 425;

		// Token: 0x04001757 RID: 5975
		private const short DTYDSYA = 426;

		// Token: 0x04001758 RID: 5976
		private const short DTYOT8 = 427;

		// Token: 0x04001759 RID: 5977
		private const short DTYDOL = 172;

		// Token: 0x0400175A RID: 5978
		private const short DTYDSYTY = 429;

		// Token: 0x0400175B RID: 5979
		private const short DTYAQE = 430;

		// Token: 0x0400175C RID: 5980
		private const short DTYKV = 431;

		// Token: 0x0400175D RID: 5981
		private const short DTYAQD = 432;

		// Token: 0x0400175E RID: 5982
		private const short DTYAQ8 = 433;

		// Token: 0x0400175F RID: 5983
		private const short DTYTIME = 178;

		// Token: 0x04001760 RID: 5984
		private const short DTYTTZ = 179;

		// Token: 0x04001761 RID: 5985
		private const short DTYSTAMP = 180;

		// Token: 0x04001762 RID: 5986
		private const short DTYSTZ = 181;

		// Token: 0x04001763 RID: 5987
		private const short DTYIYM = 182;

		// Token: 0x04001764 RID: 5988
		private const short DTYIDS = 183;

		// Token: 0x04001765 RID: 5989
		private const short DTYEDATE = 184;

		// Token: 0x04001766 RID: 5990
		private const short DTYETIME = 185;

		// Token: 0x04001767 RID: 5991
		private const short DTYETTZ = 186;

		// Token: 0x04001768 RID: 5992
		private const short DTYESTAMP = 187;

		// Token: 0x04001769 RID: 5993
		private const short DTYESTZ = 188;

		// Token: 0x0400176A RID: 5994
		private const short DTYEIYM = 189;

		// Token: 0x0400176B RID: 5995
		private const short DTYEIDS = 190;

		// Token: 0x0400176C RID: 5996
		private const short DTYLDIIF = 191;

		// Token: 0x0400176D RID: 5997
		private const short DTYLDIOF = 192;

		// Token: 0x0400176E RID: 5998
		private const short DTYRFS = 449;

		// Token: 0x0400176F RID: 5999
		private const short DTYRXH10 = 450;

		// Token: 0x04001770 RID: 6000
		private const short DTYDCLOB = 195;

		// Token: 0x04001771 RID: 6001
		private const short DTYDBLOB = 196;

		// Token: 0x04001772 RID: 6002
		private const short DTYDBFIL = 197;

		// Token: 0x04001773 RID: 6003
		private const short DTYKPN = 454;

		// Token: 0x04001774 RID: 6004
		private const short DTYKPDNR = 455;

		// Token: 0x04001775 RID: 6005
		private const short DTYDSYD = 456;

		// Token: 0x04001776 RID: 6006
		private const short DTYDSYS = 457;

		// Token: 0x04001777 RID: 6007
		private const short DTYDSYR = 458;

		// Token: 0x04001778 RID: 6008
		private const short DTYDSYH = 459;

		// Token: 0x04001779 RID: 6009
		private const short DTYDSYT = 460;

		// Token: 0x0400177A RID: 6010
		private const short DTYDSYV = 461;

		// Token: 0x0400177B RID: 6011
		private const short DTYAQM = 462;

		// Token: 0x0400177C RID: 6012
		private const short DTYOER11 = 463;

		// Token: 0x0400177D RID: 6013
		private const short DTYBURI = 208;

		// Token: 0x0400177E RID: 6014
		private const short DTYPSR = 209;

		// Token: 0x0400177F RID: 6015
		private const short DTYAQL = 466;

		// Token: 0x04001780 RID: 6016
		private const short DTYOTC = 467;

		// Token: 0x04001781 RID: 6017
		private const short DTYKFNO = 468;

		// Token: 0x04001782 RID: 6018
		private const short DTYKFNP = 469;

		// Token: 0x04001783 RID: 6019
		private const short DTYOKGT8 = 470;

		// Token: 0x04001784 RID: 6020
		private const short DTYRASB4 = 471;

		// Token: 0x04001785 RID: 6021
		private const short DTYRAUB2 = 472;

		// Token: 0x04001786 RID: 6022
		private const short DTYRAUB1 = 473;

		// Token: 0x04001787 RID: 6023
		private const short DTYRATXT = 474;

		// Token: 0x04001788 RID: 6024
		private const short DTYRSSB4 = 475;

		// Token: 0x04001789 RID: 6025
		private const short DTYRSUB2 = 476;

		// Token: 0x0400178A RID: 6026
		private const short DTYRSUB1 = 477;

		// Token: 0x0400178B RID: 6027
		private const short DTYRSTXT = 478;

		// Token: 0x0400178C RID: 6028
		private const short DTYRIDL = 479;

		// Token: 0x0400178D RID: 6029
		private const short DTYGLRDD = 480;

		// Token: 0x0400178E RID: 6030
		private const short DTYGLRDG = 481;

		// Token: 0x0400178F RID: 6031
		private const short DTYGLRDC = 482;

		// Token: 0x04001790 RID: 6032
		private const short DTYOKO = 483;

		// Token: 0x04001791 RID: 6033
		private const short DTYDPP = 484;

		// Token: 0x04001792 RID: 6034
		private const short DTYDPLS = 485;

		// Token: 0x04001793 RID: 6035
		private const short DTYDPMOP = 486;

		// Token: 0x04001794 RID: 6036
		private const short DTYSITZ = 231;

		// Token: 0x04001795 RID: 6037
		private const short DTYESITZ = 232;

		// Token: 0x04001796 RID: 6038
		private const short DTYUB8 = 233;

		// Token: 0x04001797 RID: 6039
		private const short DTYSTAT = 490;

		// Token: 0x04001798 RID: 6040
		private const short DTYRFX = 491;

		// Token: 0x04001799 RID: 6041
		private const short DTYFAL = 492;

		// Token: 0x0400179A RID: 6042
		private const short DTYCKV = 493;

		// Token: 0x0400179B RID: 6043
		private const short DTYDRCX = 494;

		// Token: 0x0400179C RID: 6044
		private const short DTYKGH = 495;

		// Token: 0x0400179D RID: 6045
		private const short DTYAQO = 496;

		// Token: 0x0400179E RID: 6046
		private const short DTYPNTY = 241;

		// Token: 0x0400179F RID: 6047
		private const short DTYOKGT = 498;

		// Token: 0x040017A0 RID: 6048
		private const short DTYKPFC = 499;

		// Token: 0x040017A1 RID: 6049
		private const short DTYFE2 = 500;

		// Token: 0x040017A2 RID: 6050
		private const short DTYSPFP = 501;

		// Token: 0x040017A3 RID: 6051
		private const short DTYDPULS = 502;

		// Token: 0x040017A4 RID: 6052
		private const short DTY_T_VA = 247;

		// Token: 0x040017A5 RID: 6053
		private const short DTY_T_TB = 248;

		// Token: 0x040017A6 RID: 6054
		private const short DTYNLOB = 249;

		// Token: 0x040017A7 RID: 6055
		private const short DTYREC = 250;

		// Token: 0x040017A8 RID: 6056
		private const short DTYTAB = 251;

		// Token: 0x040017A9 RID: 6057
		private const short DTYBOL = 252;

		// Token: 0x040017AA RID: 6058
		private const short DTYAQA = 509;

		// Token: 0x040017AB RID: 6059
		private const short DTYKPBF = 510;

		// Token: 0x040017AC RID: 6060
		private const short DTYDTY = 255;

		// Token: 0x040017AD RID: 6061
		private const short DTYTSM = 513;

		// Token: 0x040017AE RID: 6062
		private const short DTYMSS = 514;

		// Token: 0x040017AF RID: 6063
		private const short DTYABS = 515;

		// Token: 0x040017B0 RID: 6064
		private const short DTYKPC = 516;

		// Token: 0x040017B1 RID: 6065
		private const short DTYCRS = 517;

		// Token: 0x040017B2 RID: 6066
		private const short DTYKKS = 518;

		// Token: 0x040017B3 RID: 6067
		private const short DTYKSP = 519;

		// Token: 0x040017B4 RID: 6068
		private const short DTYKSPTOP = 520;

		// Token: 0x040017B5 RID: 6069
		private const short DTYKSPVAL = 521;

		// Token: 0x040017B6 RID: 6070
		private const short DTYPSS = 522;

		// Token: 0x040017B7 RID: 6071
		private const short DTYNLS = 523;

		// Token: 0x040017B8 RID: 6072
		private const short DTYALS = 524;

		// Token: 0x040017B9 RID: 6073
		private const short DTYKSDEVTVAL = 525;

		// Token: 0x040017BA RID: 6074
		private const short DTYKSDEVTTOP = 526;

		// Token: 0x040017BB RID: 6075
		private const short DTYKPSPP = 527;

		// Token: 0x040017BC RID: 6076
		private const short DTYKOL = 528;

		// Token: 0x040017BD RID: 6077
		private const short DTYLST = 529;

		// Token: 0x040017BE RID: 6078
		private const short DTYACX = 530;

		// Token: 0x040017BF RID: 6079
		private const short DTYSCS = 531;

		// Token: 0x040017C0 RID: 6080
		private const short DTYRXH = 532;

		// Token: 0x040017C1 RID: 6081
		private const short DTYKPDNS = 533;

		// Token: 0x040017C2 RID: 6082
		private const short DTYKPDCN = 534;

		// Token: 0x040017C3 RID: 6083
		private const short DTYKPNNS = 535;

		// Token: 0x040017C4 RID: 6084
		private const short DTYKPNCN = 536;

		// Token: 0x040017C5 RID: 6085
		private const short DTYKPS = 537;

		// Token: 0x040017C6 RID: 6086
		private const short DTYAPINF = 538;

		// Token: 0x040017C7 RID: 6087
		private const short DTYTEN = 539;

		// Token: 0x040017C8 RID: 6088
		private const short DTYXSSCS = 540;

		// Token: 0x040017C9 RID: 6089
		private const short DTYXSSSO = 541;

		// Token: 0x040017CA RID: 6090
		private const short DTYXSSAO = 542;

		// Token: 0x040017CB RID: 6091
		private const short DTYKSRPC = 543;

		// Token: 0x040017CC RID: 6092
		private const short DTYKVL = 560;

		// Token: 0x040017CD RID: 6093
		private const short DTYSESSGET = 563;

		// Token: 0x040017CE RID: 6094
		private const short DTYSESSRLS = 564;

		// Token: 0x040017CF RID: 6095
		private const short DTYXSSSDEF = 565;

		// Token: 0x040017D0 RID: 6096
		private const short DTYKPDQCINV = 572;

		// Token: 0x040017D1 RID: 6097
		private const short DTYKPDQIDC = 573;

		// Token: 0x040017D2 RID: 6098
		private const short DTYKPDQCSTA = 574;

		// Token: 0x040017D3 RID: 6099
		private const short DTYKPRS = 575;

		// Token: 0x040017D4 RID: 6100
		private const short DTYKPDQCID = 576;

		// Token: 0x040017D5 RID: 6101
		private const short DTYTRCEVT = 577;

		// Token: 0x040017D6 RID: 6102
		private const short DTYRTSTRM = 578;

		// Token: 0x040017D7 RID: 6103
		private const short DTYSESSRET = 579;

		// Token: 0x040017D8 RID: 6104
		private const short DTYSCN6 = 580;

		// Token: 0x040017D9 RID: 6105
		private const short DTYKECPA = 581;

		// Token: 0x040017DA RID: 6106
		private const short DTYKECPP = 582;

		// Token: 0x040017DB RID: 6107
		private const short DTYSXA = 583;

		// Token: 0x040017DC RID: 6108
		private const short DTYKVARR = 584;

		// Token: 0x040017DD RID: 6109
		private const short DTYKPNGN = 585;

		// Token: 0x040017DE RID: 6110
		private const short DTYXSNSOP = 590;

		// Token: 0x040017DF RID: 6111
		private const short DTYXSATTR = 591;

		// Token: 0x040017E0 RID: 6112
		private const short DTYXSNS = 592;

		// Token: 0x040017E1 RID: 6113
		private const short DTYTXT = 593;

		// Token: 0x040017E2 RID: 6114
		private const short DTYXSSESSNS = 594;

		// Token: 0x040017E3 RID: 6115
		private const short DTYXSATTOP = 595;

		// Token: 0x040017E4 RID: 6116
		private const short DTYXSCREOP = 596;

		// Token: 0x040017E5 RID: 6117
		private const short DTYXSDETOP = 597;

		// Token: 0x040017E6 RID: 6118
		private const short DTYXSDESOP = 598;

		// Token: 0x040017E7 RID: 6119
		private const short DTYXSSETSP = 599;

		// Token: 0x040017E8 RID: 6120
		private const short DTYXSSIDP = 600;

		// Token: 0x040017E9 RID: 6121
		private const short DTYXSPRIN = 601;

		// Token: 0x040017EA RID: 6122
		private const short DTYXSKVL = 602;

		// Token: 0x040017EB RID: 6123
		private const short DTYXSSSDEF2 = 603;

		// Token: 0x040017EC RID: 6124
		private const short DTYXSNSOP2 = 604;

		// Token: 0x040017ED RID: 6125
		private const short DTYXSNS2 = 605;

		// Token: 0x040017EE RID: 6126
		private const short DTYIMPLRES = 611;

		// Token: 0x040017EF RID: 6127
		private const short DTYOER = 612;

		// Token: 0x040017F0 RID: 6128
		private const short DTYUB1ARRAY = 613;

		// Token: 0x040017F1 RID: 6129
		private const short DTYSESSSTATE = 614;

		// Token: 0x040017F2 RID: 6130
		private const short DTYAPPCONTREPLAY = 615;

		// Token: 0x040017F3 RID: 6131
		private const short DTYAPPCONTCTL = 616;

		// Token: 0x040017F4 RID: 6132
		private const short DTYKPDNREQ = 622;

		// Token: 0x040017F5 RID: 6133
		private const short DTYKPDNRNF = 623;

		// Token: 0x040017F6 RID: 6134
		private const short DTYKPNGNC = 624;

		// Token: 0x040017F7 RID: 6135
		private const short DTYKPNRI = 625;

		// Token: 0x040017F8 RID: 6136
		private const short DTYAQENQ = 626;

		// Token: 0x040017F9 RID: 6137
		private const short DTYAQDEQ = 627;

		// Token: 0x040017FA RID: 6138
		private const short DTYAQJMS = 628;

		// Token: 0x040017FB RID: 6139
		private const short DTYKPDNRPAY = 629;

		// Token: 0x040017FC RID: 6140
		private const short DTYKPDNRACK = 630;

		// Token: 0x040017FD RID: 6141
		private const short DTYKPDNRMP = 631;

		// Token: 0x040017FE RID: 6142
		private const short DTYKPDNRDQ = 632;

		// Token: 0x040017FF RID: 6143
		private const short DTYCHUNKINFO = 636;

		// Token: 0x04001800 RID: 6144
		private const short DTYSCN = 637;

		// Token: 0x04001801 RID: 6145
		private const short DTYSCN8 = 638;

		// Token: 0x04001802 RID: 6146
		private const short DTYUDS = 639;

		// Token: 0x04001803 RID: 6147
		private const short DTYTNP = 640;

		// Token: 0x04001804 RID: 6148
		private const short DTYMAX = 640;

		// Token: 0x04001805 RID: 6149
		private const short SCALAR = 0;

		// Token: 0x04001806 RID: 6150
		private const short RECORD = 1;

		// Token: 0x04001807 RID: 6151
		private const short UTF8_CHARACTER_SET_ID = 871;

		// Token: 0x04001808 RID: 6152
		private const byte KPCCAP_RT_TZ = 1;

		// Token: 0x04001809 RID: 6153
		private const byte KPCCAP_RT_COMPAT_UNK = 0;

		// Token: 0x0400180A RID: 6154
		private const byte KPCCAP_RT_COMPAT_80 = 1;

		// Token: 0x0400180B RID: 6155
		private const byte KPCCAP_RT_COMPAT_81 = 2;

		// Token: 0x0400180C RID: 6156
		private const byte KPCCAP_RT_TZ_EX = 1;

		// Token: 0x0400180D RID: 6157
		private static short[] typeAndRep = new short[2561];

		// Token: 0x0400180E RID: 6158
		private static short[] typeAndRepFor1100 = null;

		// Token: 0x0400180F RID: 6159
		private static short[] typeAndRepFor1200 = null;

		// Token: 0x04001810 RID: 6160
		private short[] runtimeTypeAndRep;

		// Token: 0x04001811 RID: 6161
		internal static int s_latestTZVersion = 21;

		// Token: 0x04001812 RID: 6162
		internal short m_clientRemoteIn;

		// Token: 0x04001813 RID: 6163
		internal short m_clientRemoteOut;

		// Token: 0x04001814 RID: 6164
		internal short m_ncharSetId;

		// Token: 0x04001815 RID: 6165
		internal byte[] m_dbTimeZoneBytes;

		// Token: 0x04001816 RID: 6166
		internal bool m_sendTZDataAsLocalTime;

		// Token: 0x04001817 RID: 6167
		internal byte m_clientFlags;

		// Token: 0x04001818 RID: 6168
		internal byte[] m_CompileTimeCapabilities = new byte[]
		{
			6,
			1,
			0,
			0,
			106,
			1,
			1,
			8,
			1,
			1,
			1,
			1,
			1,
			1,
			0,
			41,
			144,
			3,
			7,
			3,
			0,
			1,
			0,
			235,
			1,
			0,
			5,
			1,
			0,
			0,
			0,
			24,
			0,
			0,
			7,
			0,
			2,
			58,
			0,
			0,
			1
		};

		// Token: 0x04001819 RID: 6169
		internal byte[] m_RuntimeCapabilities;

		// Token: 0x0400181A RID: 6170
		internal bool m_b32kTypeSupported;
	}
}
