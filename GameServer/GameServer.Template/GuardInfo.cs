﻿using System;
using System.Collections.Generic;

namespace GameServer.Template;

public sealed class GuardInfo
{
    public static Dictionary<ushort, GuardInfo> DataSheet;

    public string Name;
    public ushort GuardID;
    public byte Level;
    public bool Virtual;
    public bool CanBeInjured;
    public int CorpsePreservation;
    public int RevivalInterval;
    public bool ActiveAttack;
    public byte RangeHate;
    public string BasicAttackSkills;
    public int StoreID;
    public string InterfaceCode;

    public static void LoadData()
    {
        DataSheet = new Dictionary<ushort, GuardInfo>();

        if (!DBAgent.X.Connected)
            return;

        try
        {
            var qstr = "SELECT * FROM Guards";
            using (var connection = DBAgent.X.DB.GetConnection())
            {
                using var command = DBAgent.X.DB.GetCommand(connection, qstr);

                using var reader = command.ExecuteReader();
                if (reader != null)
                {
                    while (reader.Read() == true)
                    {
                        var guard = new GuardInfo();
                        guard.Name = reader.GetString("Name");
                        guard.GuardID = reader.GetUInt16("GuardID");
                        guard.Level = reader.GetByte("Level");
                        guard.Virtual = reader.GetBoolean("Virtual");
                        guard.CanBeInjured = reader.GetBoolean("CanBeInjured");
                        guard.CorpsePreservation = reader.GetInt32("CorpsePreservation");
                        guard.RevivalInterval = reader.GetInt32("RevivalInterval");
                        guard.ActiveAttack = reader.GetBoolean("ActiveAttack");
                        guard.RangeHate = reader.GetByte("RangeHate");
                        guard.BasicAttackSkills = reader.GetString("BasicAttackSkills");
                        guard.StoreID = reader.GetInt32("StoreID");
                        guard.InterfaceCode = reader.GetString("InterfaceCode");

                        DataSheet.Add(guard.GuardID, guard);
                    }
                }
            }
        }
        catch (Exception err)
        {
            SMain.AddSystemLog(err.ToString());
        }
    }
}
