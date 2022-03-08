﻿using GameOfLife.Core.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife.Core
{
    public interface IUnitOfWork
    {
        public ICellRepository Cells { get; }

        int Commit();
    }
}
