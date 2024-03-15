﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetroCloudSaving.Games
{
    public interface IGameData
    {
        string GetSavePath();

        string GetExecutablePath();
        string GetExecutableName();
    }
}
