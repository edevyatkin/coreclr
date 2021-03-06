using System;
using System.Globalization;
using System.Collections;
/// <summary>
/// BinarySearch(System.Array,System.Int32,System.Int32,System.Object,System.Collections.IComparer)
/// </summary>
public class ArrayBinarySearch1
{
    const int c_MaxValue = 10;
    const int c_MinValue = 0;
    public static int Main()
    {
        ArrayBinarySearch1 ArrayBinarySearch1 = new ArrayBinarySearch1();

        TestLibrary.TestFramework.BeginTestCase("ArrayBinarySearch1");
        if (ArrayBinarySearch1.RunTests())
        {
            TestLibrary.TestFramework.EndTestCase();
            TestLibrary.TestFramework.LogInformation("PASS");
            return 100;
        }
        else
        {
            TestLibrary.TestFramework.EndTestCase();
            TestLibrary.TestFramework.LogInformation("FAIL");
            return 0;
        }
    }

    public bool RunTests()
    {
        bool retVal = true;

        TestLibrary.TestFramework.LogInformation("[Positive]");
        retVal = PosTest1() && retVal;
        retVal = PosTest2() && retVal;
        retVal = PosTest3() && retVal;
        TestLibrary.TestFramework.LogInformation("[Negative]");
        retVal = NegTest1() && retVal;
        retVal = NegTest2() && retVal;
        retVal = NegTest3() && retVal;
        retVal = NegTest4() && retVal;
        retVal = NegTest5() && retVal;
        retVal = NegTest6() && retVal;
        return retVal;
    }

    // Returns true if the expected result is right
    // Returns false if the expected result is wrong
    public bool PosTest1()
    {
        bool retVal = true;

        TestLibrary.TestFramework.BeginScenario("PosTest1: Searches a range of elements in a one-dimensional sorted Array for a int type value, using the default IComparer implement.");

        try
        {

            Array myArray = Array.CreateInstance(typeof(int), c_MaxValue);
            int generator = 0;
            for (int i = 0; i < c_MaxValue; i++)
            {
                generator = TestLibrary.Generator.GetInt32();
                myArray.SetValue(generator, i);
            }
            int searchValue = (int)myArray.GetValue(c_MaxValue - 1);
            //sort the array
            Array.Sort(myArray);
            int returnvalue = Array.BinarySearch(myArray, c_MinValue, c_MaxValue, searchValue, null);
            if (returnvalue >= 0)
            {
                if (searchValue != (int)myArray.GetValue(returnvalue))
                {
                    TestLibrary.TestFramework.LogError("001", "Search falure .");
                    retVal = false;
                }
            }
            else
            {
                TestLibrary.TestFramework.LogError("002", "Postive condition is error.");
                retVal = false;
            }

        }
        catch (Exception e)
        {
            TestLibrary.TestFramework.LogError("003", "Unexpected exception: " + e);
            retVal = false;
        }

        return retVal;
    }

    public string GetBytes(string str)
    {
        string bytes = "0x";
        char[] chArr;
        chArr = str.ToCharArray();
        for (int i=0; i<chArr.Length; i++) bytes += String.Format("{0:x} ", (short)chArr[i]);
        return bytes;
    }

    // Returns true if the expected result is right
    // Returns false if the expected result is wrong
    public bool PosTest2()
    {
        bool retVal = true;


        TestLibrary.TestFramework.BeginScenario("PosTest2: Searches a range of elements in a one-dimensional sorted Array for a string type value, using the StringComparer.Ordinal.");

        try
        {
            Array myArray = Array.CreateInstance(typeof(string), c_MaxValue);
            string generator = string.Empty;
            for (int i = 0; i < c_MaxValue; i++)
            {
                generator = TestLibrary.Generator.GetString(true, c_MinValue+1, c_MaxValue);
                myArray.SetValue(generator, i);
            }
            string expectedstring = (string)myArray.GetValue(c_MaxValue - 1);
            myArray.SetValue(expectedstring, c_MaxValue - 1);

            //sort the array
            Array.Sort(myArray, StringComparer.Ordinal);
            string searchValue = expectedstring;
            int returnvalue = Array.BinarySearch(myArray, c_MinValue, c_MaxValue, searchValue, StringComparer.Ordinal);
            if (returnvalue >= 0)
            {
                if (0 != expectedstring.CompareTo(myArray.GetValue(returnvalue).ToString()))
                {
                    TestLibrary.TestFramework.LogError("004", "Search falure.");
                    TestLibrary.TestFramework.LogError("004", "  Expected: Val("+ expectedstring + ") Len(" +expectedstring.Length+ ")" );
                    TestLibrary.TestFramework.LogError("004", "            Hex: " + GetBytes(expectedstring));
                    TestLibrary.TestFramework.LogError("004", "  Actual:   Val(" + (string)myArray.GetValue(returnvalue) + ") Len(" +((string)myArray.GetValue(returnvalue)).Length+ ")");
                    TestLibrary.TestFramework.LogError("004", "            Hex: " + GetBytes((string)myArray.GetValue(returnvalue)));
                    TestLibrary.TestFramework.LogError("004", "  Array values...");
                    for (int i=0; i<c_MaxValue; i++)
                    {
                        if (i == returnvalue) TestLibrary.TestFramework.LogError("004", "     ** " + i + " matches the returned value " + returnvalue + " **");
                        TestLibrary.TestFramework.LogError("004", "   " + i + " " + GetBytes((string)myArray.GetValue(i)));
                    }
                    retVal = false;
                }
            }
            else
            {
                TestLibrary.TestFramework.LogError("005", "Postive condition is error.");
                retVal = false;
            }

        }
        catch (Exception e)
        {
            TestLibrary.TestFramework.LogError("006", "Unexpected exception: " + e);
            retVal = false;
        }

        return retVal;
    }

    // Returns true if the expected result is right
    // Returns false if the expected result is wrong
    public bool PosTest3()
    {
        bool retVal = true;

        TestLibrary.TestFramework.BeginScenario("PosTest3: Searches a range of elements in a one-dimensional sorted Array for a customer define type , using the default IComparer implement.");

        try
        {
            Array myArray = Array.CreateInstance(typeof(Temperature), c_MaxValue);
            Temperature generator = null;
            for (int i = 0; i < c_MaxValue; i++)
            {
                generator = new Temperature();
                generator.Value = i * 4;
                myArray.SetValue(generator, i);
            }
            Temperature expected = myArray.GetValue(c_MaxValue - 1) as Temperature;


            //Temperature searchValue = expectedstring;
            IComparer iComparableImpl = (Temperature)myArray.GetValue(c_MaxValue - 1);
            //sort the array
            Array.Sort(myArray, ((IComparer)iComparableImpl));
            int returnvalue = Array.BinarySearch(myArray, c_MinValue, c_MaxValue, iComparableImpl, ((IComparer)iComparableImpl));
            if (returnvalue >= 0)
            {
                if (!expected.Equals(myArray.GetValue(returnvalue)))
                {
                    TestLibrary.TestFramework.LogError("007", "Search falure .");
                    retVal = false;
                }
            }
            else
            {
                TestLibrary.TestFramework.LogError("008", "Postive condition is error.");
                retVal = false;
            }

        }
        catch (Exception e)
        {
            TestLibrary.TestFramework.LogError("009", "Unexpected exception: " + e);
            retVal = false;
        }

        return retVal;
    }

    // Returns true if the expected result is right
    // Returns false if the expected result is wrong
    public bool NegTest1()
    {
        bool retVal = true;

        TestLibrary.TestFramework.BeginScenario("NegTest1: array is a null reference.");

        try
        {
            Array myArray = Array.CreateInstance(typeof(Temperature), c_MaxValue);
            IComparer iComparableImpl = (Temperature)myArray.GetValue(c_MaxValue - 1);
            myArray = null;
            int returnvalue = Array.BinarySearch(myArray, c_MinValue, c_MaxValue, iComparableImpl, ((IComparer)iComparableImpl));

            TestLibrary.TestFramework.LogError("010", "array is a null reference.");
            retVal = false;
        }
        catch (ArgumentNullException)
        {
        }
        catch (Exception e)
        {
            TestLibrary.TestFramework.LogError("011", "Unexpected exception: " + e);
            retVal = false;
        }

        return retVal;
    }
    // Returns true if the expected result is right
    // Returns false if the expected result is wrong
    public bool NegTest2()
    {
        bool retVal = true;

        TestLibrary.TestFramework.BeginScenario("NegTest2: array is multidimensional.");
        try
        {
            int[] ParamArray ={ c_MaxValue, c_MaxValue };
            Array myArray = Array.CreateInstance(typeof(Temperature), ParamArray);
            int returnvalue = Array.BinarySearch(myArray, c_MinValue, c_MaxValue, c_MaxValue, null);
            TestLibrary.TestFramework.LogError("012", "array is multidimensional.");
            retVal = false;
        }
        catch (RankException)
        {
        }
        catch (Exception e)
        {
            TestLibrary.TestFramework.LogError("013", "Unexpected exception: " + e);
            retVal = false;
        }

        return retVal;
    }
    // Returns true if the expected result is right
    // Returns false if the expected result is wrong
    public bool NegTest3()
    {
        bool retVal = true;

        TestLibrary.TestFramework.BeginScenario("NegTest3: index is less than the lower bound of array.");

        try
        {
            Array myArray = Array.CreateInstance(typeof(int), c_MaxValue);
            int generator = 0;
            for (int i = 0; i < c_MaxValue; i++)
            {
                generator = TestLibrary.Generator.GetInt32();
                myArray.SetValue(generator, i);
            }
            int searchValue = (int)myArray.GetValue(c_MaxValue - 1);
            //sort the array
            Array.Sort(myArray);
            int returnvalue = Array.BinarySearch(myArray, c_MinValue - 1, c_MaxValue, searchValue, null);

            TestLibrary.TestFramework.LogError("014", "index is less than the lower bound of array.");
            retVal = false;
        }
        catch (ArgumentOutOfRangeException)
        {
        }
        catch (Exception e)
        {
            TestLibrary.TestFramework.LogError("015", "Unexpected exception: " + e);
            retVal = false;
        }

        return retVal;
    }
    // Returns true if the expected result is right
    // Returns false if the expected result is wrong
    public bool NegTest4()
    {
        bool retVal = true;

        TestLibrary.TestFramework.BeginScenario("NegTest4: length is less than zero.");

        try
        {
            Array myArray = Array.CreateInstance(typeof(int), c_MaxValue);
            int generator = 0;
            for (int i = 0; i < c_MaxValue; i++)
            {
                generator = TestLibrary.Generator.GetInt32();
                myArray.SetValue(generator, i);
            }
            int searchValue = (int)myArray.GetValue(c_MaxValue - 1);
            //sort the array
            Array.Sort(myArray);
            int returnvalue = Array.BinarySearch(myArray, c_MinValue, c_MinValue - 1, searchValue, null);

            TestLibrary.TestFramework.LogError("016", "length is less than zero.");
            retVal = false;
        }
        catch (ArgumentOutOfRangeException)
        {
        }
        catch (Exception e)
        {
            TestLibrary.TestFramework.LogError("017", "Unexpected exception: " + e);
            retVal = false;
        }

        return retVal;
    }
    // Returns true if the expected result is right
    // Returns false if the expected result is wrong
    public bool NegTest5()
    {
        bool retVal = true;

        TestLibrary.TestFramework.BeginScenario("NegTest5: index and length do not specify a valid range in array.");

        try
        {
            Array myArray = Array.CreateInstance(typeof(int), c_MaxValue);
            int generator = 0;
            for (int i = 0; i < c_MaxValue; i++)
            {
                generator = TestLibrary.Generator.GetInt32();
                myArray.SetValue(generator, i);
            }
            int searchValue = (int)myArray.GetValue(c_MaxValue - 1);
            //sort the array
            Array.Sort(myArray);
            int returnvalue = Array.BinarySearch(myArray, c_MaxValue, c_MaxValue, searchValue, null);

            TestLibrary.TestFramework.LogError("018", "index and length do not specify a valid range in array.");
            retVal = false;
        }
        catch (ArgumentException)
        {
        }
        catch (Exception e)
        {
            TestLibrary.TestFramework.LogError("019", "Unexpected exception: " + e);
            retVal = false;
        }

        return retVal;
    }
    // Returns true if the expected result is right
    // Returns false if the expected result is wrong
    public bool NegTest6()
    {
        bool retVal = true;

        TestLibrary.TestFramework.BeginScenario("NegTest6: comparer is a null reference , " +
            "\nvalue does not implement the IComparable interface, " +
            "\nand the search encounters an element that does not \nimplement the IComparable interface.");

        try
        {
            Array myArray = Array.CreateInstance(typeof(Temperature), c_MaxValue);
            Temperature generator = null;
            for (int i = 0; i < c_MaxValue; i++)
            {
                generator = new Temperature();
                generator.Value = i * 4;
                myArray.SetValue(generator, i);
            }
            //Temperature searchValue = expectedstring;
            IComparer iComparableImpl = (Temperature)myArray.GetValue(c_MaxValue - 1);
            //sort the array
            Array.Sort(myArray, ((IComparer)iComparableImpl));
            TestClass testValueNotImplTemperature = new TestClass();
            int returnvalue = Array.BinarySearch(myArray, c_MinValue, c_MaxValue, testValueNotImplTemperature, null);
            TestLibrary.TestFramework.LogError("020", " comparer is a null reference , " +
            "\nvalue does not implement the IComparable interface, " +
            "\nand the search encounters an element that does not \nimplement the IComparable interface.");
            retVal = false;
        }
        catch (InvalidOperationException)
        {
        }
        catch (Exception e)
        {
            TestLibrary.TestFramework.LogError("021", "Unexpected exception: " + e);
            retVal = false;
        }

        return retVal;
    }
}
//create Temperature  for provding test method and test target.
public class Temperature : System.Collections.IComparer
{

    // The value holder
    protected int m_value;

    public int Value
    {
        get
        {
            return m_value;
        }
        set
        {
            m_value = value;
        }
    }
    #region IComparer Members

    public int Compare(object x, object y)
    {
        if (x is Temperature)
        {
            Temperature temp = (Temperature)x;

            return ((Temperature)y).m_value.CompareTo(temp.m_value);
        }

        throw new Exception("The method parameter x is not expected.");
    }

    #endregion
}
public class TestClass
{

}


