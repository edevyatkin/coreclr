using System;
using System.Globalization;
using TestLibrary;

/// <summary>
///ReadOnly
/// </summary>
public class CultureInfoReadOnly
{
    public static int Main()
    {
        CultureInfoReadOnly CultureInfoReadOnly = new CultureInfoReadOnly();

        TestLibrary.TestFramework.BeginTestCase("CultureInfoReadOnly");
        if (CultureInfoReadOnly.RunTests())
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
        if (!Utilities.IsWindows)
        {
            // Neutral cultures not supported on Windows
            retVal = PosTest2() && retVal;
        }
        retVal = PosTest3() && retVal;
        return retVal;
    }
    // Returns true if the expected result is right
    // Returns false if the expected result is wrong
    public bool PosTest1()
    {
        bool retVal = true;

        TestLibrary.TestFramework.BeginScenario("PosTest1: CultureTypes.SpecificCultures");
        try
        {

            CultureInfo myCultureInfo = new CultureInfo("en-US");
            if (!myCultureInfo.IsReadOnly)
            {
                myCultureInfo = CultureInfo.ReadOnly(myCultureInfo);
                if (!myCultureInfo.IsReadOnly)
                {
                    TestLibrary.TestFramework.LogError("001", "The cultureInfo.IsReadOnly should return true.");
                    retVal = false;
                }
            }
            else
            {
                TestLibrary.TestFramework.LogError("002", "The condition is error.");
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
    // Returns true if the expected result is right
    // Returns false if the expected result is wrong
    public bool PosTest2()
    {
        bool retVal = true;

        TestLibrary.TestFramework.BeginScenario("PosTest2: CultureTypes.NeutralCultures");
        try
        {
            CultureInfo myCultureInfo = new CultureInfo("en");
            if (!myCultureInfo.IsReadOnly)
            {
                myCultureInfo = CultureInfo.ReadOnly(myCultureInfo);
                if (!myCultureInfo.IsReadOnly)
                {
                    TestLibrary.TestFramework.LogError("004", "The cultureInfo.IsReadOnly should return true.");
                    retVal = false;
                }
            }
            else
            {
                TestLibrary.TestFramework.LogError("005", "The condition is error.");
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

        TestLibrary.TestFramework.BeginScenario("PosTest3: invariant culture which is readonly ,then invoke ReadOnly method ,  return value is still a readonly culture");
        try
        {

            CultureInfo myCultureInfo = CultureInfo.InvariantCulture;
            if (myCultureInfo.IsReadOnly)
            {
                myCultureInfo = CultureInfo.ReadOnly(myCultureInfo);
                if (!myCultureInfo.IsReadOnly)
                {
                    TestLibrary.TestFramework.LogError("007", "The cultureInfo.IsReadOnly should return true.");
                    retVal = false;
                }
            }
            else
            {
                TestLibrary.TestFramework.LogError("008", "The condition is error.");
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
}

