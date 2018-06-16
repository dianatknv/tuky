using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSM
{
    public delegate void MyDelegate(string msg);
    public enum CurrentState
    {
        Zero,
        AccumulateDigits,
        AccumulateDigitsWithDecimal,
        ComputeWithPending,
        ComputeNoPending,
        Compute_spec,
        Memory_Editor,
        ComputeItself,
        Clear,
    }

    public class Brain
    {
        public string number;
        public string result;
        public string op;
        public string spec_op;
        public string res_saver;
        public string memory_saver = "0";
        public string memory_op;
        public string clear_op;
        public string without_op;

        public MyDelegate invoker;
        public CurrentState currentState;
        public bool isZero = false;
        public string[] all_digits = { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };
        public string[] non_zero_digits = { "1", "2", "3", "4", "5", "6", "7", "8", "9" };
        public string[] zero_digits = { "0" };
        public string[] operations = { "+", "-", "x", "÷" };
        public string[] spec_operations = { "√", "x²", "¹̷ₓ ", "±" };
        public string[] equals = { "=", "%" };
        public string[] separators = { "." };
        public string[] clear = { "C", "CE", "⌫" };
        public string[] memory = { "MS", "MR", "M+", "M-", "MC" };

        public Brain()
        {
            currentState = CurrentState.Zero;
        }
        public void Process(string operation)
        {
            switch (currentState)
            {
                case CurrentState.Zero:
                    Zero(false, operation);
                    break;
                case CurrentState.AccumulateDigits:
                    Accumulate_Digits(false, operation);
                    break;
                case CurrentState.AccumulateDigitsWithDecimal:
                    Accumulate_Digits_With_Decimal(false, operation);
                    break;
                case CurrentState.ComputeWithPending:
                    Compute_With_pendings(false, operation);
                    break;
                case CurrentState.ComputeItself:
                    Compute_Itself(false, operation);
                    break;
                case CurrentState.ComputeNoPending:
                    Compute_no_Pending(false, operation);
                    break;
                case CurrentState.Compute_spec:
                    Compute_spec(false, operation);
                    break;
                case CurrentState.Memory_Editor:
                    Memory_Editor(false, operation);
                    break;
                case CurrentState.Clear:
                    Clear(false, operation);
                    break;
            }
        }
        public void Zero(bool isInput, string info)
        {
            if (isInput)
            {

                result = "0";
                currentState = CurrentState.Zero;
                invoker.Invoke(result);

            }
            else
            {
                if (non_zero_digits.Contains(info))
                {
                    Accumulate_Digits(true, info);
                }
                else if (operations.Contains(info))
                {
                    result = "0";
                    Compute_With_pendings(true, info);
                }
                else if (zero_digits.Contains(info))
                {
                    Zero(true, info);
                }
                else if (memory.Contains(info))
                {
                    result = "0";
                    memory_op = info;
                    Memory_Editor(true, info);
                }
                else if (separators.Contains(info))
                {
                    result = "0";
                    Accumulate_Digits_With_Decimal(true, info);
                }
                else if (spec_operations.Contains(info))
                {
                    result = "0";
                    spec_op = info;
                    Compute_spec(true, info);
                }
                else if (equals.Contains(info))
                {
                    Zero(true, info);
                }
                else if (clear.Contains(info))
                {
                    Zero(true, info);
                }
            }
        }
        public void Accumulate_Digits(bool isInput, string info)
        {
            if (isInput)
            {

                if (result == "0")
                {
                    result = info;
                }
                else
                {
                    result += info;
                }
                currentState = CurrentState.AccumulateDigits;
                invoker.Invoke(result);
            }
            else
            {
                if (all_digits.Contains(info))
                {
                    Accumulate_Digits(true, info);
                }
                else if (operations.Contains(info) && op == null)
                {
                    Compute_With_pendings(true, info);
                }
                else if (operations.Contains(info) && op != null && res_saver == null)
                {
                    res_saver = "=";
                    Compute_no_Pending(true, info);
                    op = info;
                    result = null;
                }
                else if (operations.Contains(info) && op != null && res_saver != null)
                {
                    result = "";
                    op = info;
                    Compute_With_pendings(true, info);
                }

                else if (separators.Contains(info))
                {
                    Accumulate_Digits_With_Decimal(true, info);
                }
                else if (spec_operations.Contains(info))
                {
                    spec_op = info;
                    Compute_spec(true, info);
                }
                else if (equals.Contains(info) && op != null && res_saver == null)
                {
                    res_saver = info;
                    Compute_no_Pending(true, info);
                }
                else if (equals.Contains(info) && op != null && res_saver != null)
                {
                    // result = null;
                    res_saver = info;
                    Compute_no_Pending(true, info);
                }
                else if (equals.Contains(info) && op == null)
                {
                    without_op = info;
                    Compute_Itself(true, info);

                }
                else if (memory.Contains(info))
                {
                    memory_op = info;
                    Memory_Editor(true, info);
                }
                else if (clear.Contains(info))
                {
                    clear_op = info;
                    Clear(true, info);
                }
            }
        }
        public void Accumulate_Digits_With_Decimal(bool isInput, string info)
        {
            if (isInput)
            {
                if (info == "." && !result.Contains(",")) result += ",";
                else result += info;
                currentState = CurrentState.AccumulateDigitsWithDecimal;
                invoker.Invoke(result);
            }
            else
            {
                if (operations.Contains(info))
                {
                    Compute_With_pendings(true, info);
                }
                else if (all_digits.Contains(info))
                {
                    Accumulate_Digits_With_Decimal(true, info);
                }
                else if (equals.Contains(info) && op == null)
                {
                    res_saver = info;
                    if (res_saver == "=")
                    {
                        invoker.Invoke(result);
                    }
                }
                else if (equals.Contains(info) && op != null)
                {
                    res_saver = info;
                    Compute_no_Pending(true, info);
                }
                else if (memory.Contains(info))
                {
                    memory_op = info;
                    Memory_Editor(true, info);
                }
                else if (spec_operations.Contains(info))
                {
                    spec_op = info;
                    Compute_spec(true, info);
                }
                else if (clear.Contains(info))
                {
                    clear_op = info;
                    Clear(true, info);
                }
            }
        }
        public void Compute_With_pendings(bool isInput, string info)
        {
            if (isInput)
            {
                if (op == null)
                {
                    op = info;
                    number = result;
                    result = "";
                    currentState = CurrentState.ComputeWithPending;
                    invoker.Invoke("0");
                }
                else if (op != null)
                {
                    op = info;
                    result = "";
                    currentState = CurrentState.ComputeWithPending;
                    invoker.Invoke("0");
                }
            }
            else
            {
                if (all_digits.Contains(info))
                {
                    Accumulate_Digits(true, info);
                }
                else if (equals.Contains(info))
                {
                    res_saver = info;
                    result = number;
                    Compute_no_Pending(true, info);
                }
                else if (separators.Contains(info))
                {
                    result = "0";
                    Accumulate_Digits_With_Decimal(true, info);
                }
            }
        }
        public void Compute_no_Pending(bool isInput, string info)
        {
            double a1 = double.Parse(number);
            double a2 = double.Parse(result);
            if (isInput)
            {
                if (res_saver == "=" && op != null)
                {
                    if (op == "+")
                    {
                        number = (a1 + a2).ToString();
                    }
                    else if (op == "-")
                    {
                        number = (a1 - a2).ToString();
                    }
                    else if (op == "x")
                    {
                        number = (a1 * a2).ToString();
                    }
                    else if (op == "÷")
                    {
                        if (a2 != 0)
                        {
                            number = (a1 / a2).ToString();
                        }
                        else if (a2 == 0)
                        {
                            number = "ERROR";
                            //invoker.Invoke("55");
                        }

                    }
                }
                else if (res_saver == "%")
                {
                    number = (a1 * a2 / 100).ToString();
                }
                invoker.Invoke(number);
                currentState = CurrentState.AccumulateDigits;
            }
            else
            {
                // invoker.Invoke(op);
                if (all_digits.Contains(info))
                {
                    //result = "";
                    //op = info;
                    Accumulate_Digits(true, info);
                }
                else if (equals.Contains(info))
                {
                    res_saver = info;
                    Compute_no_Pending(true, info);
                }
                else if (zero_digits.Contains(info))
                {
                    Zero(true, info);
                }
                else if (memory.Contains(info))
                {
                    memory_op = info;
                    Memory_Editor(true, info);
                }
                else if (clear.Contains(info))
                {
                    clear_op = info;
                    Clear(true, info);
                }
                else if (separators.Contains(info))
                {
                    Accumulate_Digits(true, info);
                }
                else if (operations.Contains(info) && op != null)
                {
                    op = info;
                    result = "";
                    Compute_With_pendings(true, info);
                }
            }
        }
        public void Compute_spec(bool isInput, string info)
        {
            double a2 = double.Parse(result);
            double a1 = double.Parse(number);
            if (isInput)
            {
                if (spec_op == "x²")
                {
                    if (number != "0" && result != "0")
                    {
                        result = (a1 * a1).ToString();
                    }
                    else if (result != "0" && number == "0")
                    {
                        result = (a2 * a2).ToString();
                    }

                }
                else if (spec_op == "√")
                {
                    if (a2 < 0)
                    {
                        result = "ERROR";
                    }
                    else if (result == "0" && number != "0" || result != "0" && number != "0")
                    {
                        result = (Math.Pow(a1, 0.5)).ToString();
                    }
                    else if (number == "0" && result != "0")
                    {
                        result = (Math.Pow(a2, 0.5).ToString());
                    }
                }
                else if (spec_op == "¹̷ₓ ")
                {
                    if ((a2 == 0 && op == null) || ((a1 == 0) && (op != null)))
                    {
                        result = "ERROR";
                    }
                    else
                    {
                        if (result != "0" && number != "0")
                        {
                            result = (1 / a1).ToString();
                        }
                        else if (number == "0" && result != "0" && op == null)
                        {
                            result = (1 / a2).ToString();
                        }
                    }
                }
                else if (spec_op == "±")
                {
                    result = (-a2).ToString();
                }
                invoker.Invoke(result);
                currentState = CurrentState.AccumulateDigits;
            }
            else
            {
                if (memory.Contains(info))
                {
                    memory_op = info;
                    Memory_Editor(true, info);
                }
                else if (clear.Contains(info))
                {
                    if (result == "ERROR")
                    {
                        result = "0";
                    }
                    clear_op = info;
                    Clear(true, info);
                }
                else if (equals.Contains(info))
                {
                    res_saver = info;
                    Accumulate_Digits_With_Decimal(true, info);
                }
                else if (operations.Contains(info))
                {
                    op = info;
                    Compute_With_pendings(true, info);
                }
                else if (spec_operations.Contains(info))
                {
                    spec_op = info;
                    Compute_spec(true, info);
                }
            }
        }
        public void Memory_Editor(bool isInput, string info)
        {
            if (isInput)
            {
                if (memory_op == "MS")
                {
                    memory_saver = result;
                    currentState = CurrentState.AccumulateDigits;

                }
                else if (memory_op == "M+")
                {
                    memory_saver = (Double.Parse(memory_saver) + Double.Parse(result)).ToString();
                    currentState = CurrentState.AccumulateDigits;
                }
                else if (memory_op == "M-")
                {
                    memory_saver = (Double.Parse(memory_saver) - Double.Parse(result)).ToString();
                    currentState = CurrentState.AccumulateDigits;
                }
                else if (memory_op == "MC")
                {
                    memory_saver = "0";
                    currentState = CurrentState.AccumulateDigits;

                }
                else if (memory_op == "MR")
                {
                    invoker.Invoke(memory_saver);
                    currentState = CurrentState.AccumulateDigits;
                    result = memory_saver;
                }
            }
            else
            {
                if (clear.Contains(info))
                {
                    clear_op = info;
                    Clear(true, info);
                }
            }
        }
        public void Compute_Itself(bool isInput, string info)
        {
            if (isInput)
            {
                if (without_op == "=")
                {
                    invoker.Invoke(result);
                }
                else if (without_op == "%")
                {
                    invoker.Invoke("0");
                }
            }
            currentState = CurrentState.AccumulateDigits;
        }
        public void Clear(bool isInput, string info)
        {
            if (isInput)
            {
                if (clear_op == "C")
                {
                    result = "0";
                    number = "0";
                    op = null;
                    res_saver = null;
                    invoker.Invoke(result);
                    Zero(true, info);
                }
                else if (clear_op == "CE")
                {
                    result = "0";
                    invoker.Invoke(result);
                }
                else if (clear_op == "⌫")
                {
                    if (result.Length <= 1)
                    {
                        result = "0";
                        // invoker.Invoke(result);
                        currentState = CurrentState.Zero;
                    }
                    else
                    {
                        result = result.Remove(result.Length - 1, 1);
                    }
                    invoker.Invoke(result);

                }
            }
            else if (all_digits.Contains(info))
            {
                Accumulate_Digits(true, info);
            }
            else if (clear.Contains(info))
            {
                clear_op = info;
                Clear(true, info);
            }
        }
    }
}