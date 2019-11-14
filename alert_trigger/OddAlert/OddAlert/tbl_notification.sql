/*
Navicat SQLite Data Transfer

Source Server         : db_init
Source Server Version : 30808
Source Host           : :0

Target Server Type    : SQLite
Target Server Version : 30808
File Encoding         : 65001

Date: 2019-06-13 13:50:39
*/

PRAGMA foreign_keys = OFF;

-- ----------------------------
-- Table structure for tbl_notification
-- ----------------------------
DROP TABLE IF EXISTS "main"."tbl_notification";
CREATE TABLE "tbl_notification" (
"id"  INTEGER NOT NULL,
"datetime"  DATETIME,
"time_to_jump"  TEXT(100),
"degree"  INTEGER,
"state"  TEXT,
"venue"  TEXT,
"race_number"  INTEGER,
"horse_number"  INTEGER,
"horse_name"  TEXT,
"previous_price"  REAL,
"current_price"  REAL,
"bookie"  TEXT,
"suggested_stake"  REAL,
"max_profit"  REAL,
"top_price_1"  TEXT,
"top_price_2"  TEXT,
"top_price_3"  TEXT,
"top_price_4"  TEXT,
"bookie_taken"  TEXT,
"price_taken"  REAL,
"bet_amount"  REAL,
"result"  TEXT,
"BF_SP"  REAL,
"status"  INTEGER,
PRIMARY KEY ("id" ASC)
);
