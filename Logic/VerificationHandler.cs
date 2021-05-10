using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    class VerificationHandler
    {
        public bool VerifyMatrixSizeInput(int i_SizeInput)
        {
            return (i_SizeInput >= 3 && i_SizeInput <= 9);
        }
    }
}