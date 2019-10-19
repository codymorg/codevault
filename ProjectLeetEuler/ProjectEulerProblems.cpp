/******************************************************************************
  * Project Euler Solutions
  * Author : Cody Morgan
  * Brief  : This is a collection of my solutions to ProjectEuler
  *          and Leetcode problems mostly done for fun
  *          This was orginally done in C# 2013 while I was deployed with the
  *          Army but I was a young and reckless programmer so now I'm doing
  *          it the right way - or at least a better way

******************************************************************************/

#include "StandardIncludes.h"
#include "ProjectEulerProblems.h"
#include "Utility.h"
#include "MathUtility.h"



// actual problems
namespace ProjectEulerProblems
{
  void Euler1()
  {
    cout << "what is the sum of all factors of 3 or 5 < 1000?\n";

    int sum = 0;
    for (int i = 3; i < 1000; i++)
    {
      if ((i % 3 == 0) || (i % 5) == 0)
      {
        sum += i;
      }
    }

    cout << "answer: " << sum << "\n";
  }

  void Euler2()
  {
    cout << "what is the sum of all even fibonacci numbers less than 4000000?\n";

    unsigned num = 1;
    unsigned back1 = 2;
    unsigned back2 = 1;
    unsigned sum = 0;

    // calculate fibonacci values
    while (num < 4000000)
    {
      num = back1 + back2;
      back2 = back1;
      back1 = num;

      // sum only those that are even
      if (num % 2 == 0)
        sum += num;
    }

    cout << "answer: " << sum << "\n";
  }

  void Euler3()
  {
    cout << "what is the largest prime factor of 600851475143?\n";

    long long number = 600851475143;
    long largestFactor = long(sqrt(number));         // there's no factor greater than the sqrt of a number
    vector<long> primes = PrimeSieve(largestFactor); // get all primes that might be factors

    // check each prime to see if it's a factor
    for (long i = long(primes.size()) - 1; i >= 0; i--)
    {
      if (number % primes[i] == 0)
      {
        largestFactor = primes[i];
        break;
      }
    }

    cout << "answer: " << largestFactor << "\n";
  }

  void Euler4()
  {
    cout << "Find the largest palindrome made from the product of two 3-digit numbers.\n";

    char palindrome[7] = {}; // no 3 digit multiplication is greater than 6 digits

    for (unsigned factor1 = 999; factor1 >= 100; factor1--)
    {
      for (unsigned factor2 = 999; factor2 >= 100; factor2--)
      {
        // convert number into char array for palindrome testing
        unsigned testFactor = factor1 * factor2;
        _itoa_s(testFactor, &palindrome[0], _countof(palindrome), 10);

        // if it's a plindrome we're done here
        if (StringManipulation::IsPalindrome(palindrome, _countof(palindrome)))
        {
          // let's assume that it starts with a 9 and is 6 digits
          if (palindrome[0] == '9')
          {
            cout << "answer: " << palindrome << "\n";
            return;
          }
        }
      }
    }
  }

  void Euler5()
  {
    cout << "What is the smallest positive number that is evenly divisible by all of the numbers from 1 to 20?\n";

    unsigned x = 20;
    unsigned num = x;
    unsigned factor = x;
    bool done = false;

    while (done == false)
    {
      if (num % factor == 0 && factor > 0)
      {
        factor--;
      }
      else
      {
        factor = x;
        num += x;
      }
      if (factor == 0)
      {
        done = true;
      }
    }
    
    cout << "answer: " << num << '\n';
  }
  
  void Euler6()
  {
    cout << "Find the difference between the sum of the squares of the first one hundred natural numbers and the square of the sum.\n";

    //square of sum - sum of square
    long square;
    long sum = 0;
    long num = 1;
    while (num < 101)
    {
      square = num * num;
      sum = sum + square;
      num++;
    }

    num = 1;
    square = 0;
    while (num < 101)
    {
      square += num;
      num++;
    }
    square *= square;
    num = square - sum;
    cout << "Answer: " << num << "\n";
  }

  void Euler7()
  {
    cout << "What is the 10001st prime number?\n";

    // we're guarenteed to find X primes under 90X
    vector<unsigned>primes = PrimeSieve(unsigned(10001 * 90));

    cout << "answer: " << primes[10001] << "\n";
  }

  void Euler8()
  {
    {
      cout << "73167176531330624919225119674426574742355349194934\n";
      cout << "96983520312774506326239578318016984801869478851843\n";
      cout << "85861560789112949495459501737958331952853208805511\n";
      cout << "12540698747158523863050715693290963295227443043557\n";
      cout << "66896648950445244523161731856403098711121722383113\n";
      cout << "62229893423380308135336276614282806444486645238749\n";
      cout << "30358907296290491560440772390713810515859307960866\n";
      cout << "70172427121883998797908792274921901699720888093776\n";
      cout << "65727333001053367881220235421809751254540594752243\n";
      cout << "52584907711670556013604839586446706324415722155397\n";
      cout << "53697817977846174064955149290862569321978468622482\n";
      cout << "83972241375657056057490261407972968652414535100474\n";
      cout << "82166370484403199890008895243450658541227588666881\n";
      cout << "16427171479924442928230863465674813919123162824586\n";
      cout << "17866458359124566529476545682848912883142607690042\n";
      cout << "24219022671055626321111109370544217506941658960408\n";
      cout << "07198403850962455444362981230987879927244284909188\n";
      cout << "84580156166097919133875499200524063689912560717606\n";
      cout << "05886116467109405077541002256983155200055935729725\n";
      cout << "71636269561882670428252483600823257530420752963450\n";
    }
    cout << "Find the thirteen adjacent digits in the 1000-digit number that have the greatest product. What is the value of this product?\n";

    string huge = "7316717653133062491922511967442657474235534919493496983520312774506326239578318016984801869478851843858615607891129494954595017379583319528532088055111254069874715852386305071569329096329522744304355766896648950445244523161731856403098711121722383113622298934233803081353362766142828064444866452387493035890729629049156044077239071381051585930796086670172427121883998797908792274921901699720888093776657273330010533678812202354218097512545405947522435258490771167055601360483958644670632441572215539753697817977846174064955149290862569321978468622482839722413756570560574902614079729686524145351004748216637048440319989000889524345065854122758866688116427171479924442928230863465674813919123162824586178664583591245665294765456828489128831426076900422421902267105562632111110937054421750694165896040807198403850962455444362981230987879927244284909188845801561660979191338754992005240636899125607176060588611646710940507754100225698315520005593572972571636269561882670428252483600823257530420752963450";
    unsigned long long max = 0;           // what max we've found
    constexpr unsigned productCount = 13; // how many numbers are in the product

    // check product values
    for (unsigned start = 0; start < huge.size(); start++)
    {
      // get product of next 13 values
      unsigned long long current = 1;
      for (unsigned i = 0; i < productCount; i++)
      {
        unsigned end = start + i;
        unsigned currentChar = huge[end] - '0';
        current *= currentChar;

        // we hit a 0 there's no way it's the max
        if (current == 0)
        {
          start += i;
          break;
        }
      }

      // update max
      if (current > max)
      {
        max = current;
      }
    }

    cout << "answer: " << max << "\n";
  }

  void Euler9()
  {
    cout << "There exists exactly one Pythagorean triplet for which a + b + c = 1000. Find the product abc.\n";

    unsigned a = 1;
    unsigned b = 2;
    unsigned c = 997;
    unsigned count = 0;

    while (b > a)
    {
      while (c > b)
      {
        // pythag check!
        if ((c * c) == ((a * a) + (b * b)))
        {
          cout << "answer: " << a * b * c << "\n";
          return;
        }

        // not pythagorian - keep checking
        else
        {
          
          b++;
          c--;
          count++;
        }
      }

      // update values
      a++;
      b += 1 - count;
      c += count - 2;
      count = 0;
    }
  }

  void Euler10()
  {
    cout << "Find the sum of all the primes below two million.\n";

    vector<unsigned long long>primes = PrimeSieve(unsigned long long(2000000));
    unsigned long long sum = 0;
    for (unsigned long long i : primes)
    {
      sum += i;
    }

    cout << "answer: " << sum << "\n";
  }

  void Euler11()
  {
    int limit = 20, mult = 0, max = 0, count = 0;
    int matrix[] = {   8, 2, 22, 97, 38, 15, 0, 40, 0, 75, 4, 5, 7, 78, 52, 12, 50, 77, 91, 8,
                       49, 49, 99, 40, 17, 81, 18, 57, 60, 87, 17, 40, 98, 43, 69, 48, 4, 56, 62, 0,
                       81, 49, 31, 73, 55, 79, 14, 29, 93, 71, 40, 67, 53, 88, 30, 03, 49, 13, 36, 65,
                       52, 70, 95, 23, 04, 60, 11, 42, 69, 24, 68, 56, 01, 32, 56, 71, 37, 02, 36, 91,
                       22, 31, 16, 71, 51, 67, 63, 89, 41, 92, 36, 54, 22, 40, 40, 28, 66, 33, 13, 80,
                       24, 47, 32, 60, 99, 03, 45, 02, 44, 75, 33, 53, 78, 36, 84, 20, 35, 17, 12, 50,
                       32, 98, 81, 28, 64, 23, 67, 10, 26, 38, 40, 67, 59, 54, 70, 66, 18, 38, 64, 70,
                       67, 26, 20, 68, 02, 62, 12, 20, 95, 63, 94, 39, 63, 8, 40, 91, 66, 49, 94, 21,
                       24, 55, 58, 05, 66, 73, 99, 26, 97, 17, 78, 78, 96, 83, 14, 88, 34, 89, 63, 72,
                       21, 36, 23, 9, 75, 00, 76, 44, 20, 45, 35, 14, 00, 61, 33, 97, 34, 31, 33, 95,
                       78, 17, 53, 28, 22, 75, 31, 67, 15, 94, 03, 80, 04, 62, 16, 14, 9, 53, 56, 92,
                       16, 39, 05, 42, 96, 35, 31, 47, 55, 58, 88, 24, 00, 17, 54, 24, 36, 29, 85, 57,
                       86, 56, 00, 48, 35, 71, 89, 07, 05, 44, 44, 37, 44, 60, 21, 58, 51, 54, 17, 58,
                       19, 80, 81, 68, 05, 94, 47, 69, 28, 73, 92, 13, 86, 52, 17, 77, 04, 89, 55, 40,
                       04, 52, 8, 83, 97, 35, 99, 16, 07, 97, 57, 32, 16, 26, 26, 79, 33, 27, 98, 66,
                       88, 36, 68, 87, 57, 62, 20, 72, 03, 46, 33, 67, 46, 55, 12, 32, 63, 93, 53, 69,
                       04, 42, 16, 73, 38, 25, 39, 11, 24, 94, 72, 18, 8, 46, 29, 32, 40, 62, 76, 36,
                       20, 69, 36, 41, 72, 30, 23, 88, 34, 62, 99, 69, 82, 67, 59, 85, 74, 04, 36, 16,
                       20, 73, 35, 29, 78, 31, 90, 01, 74, 31, 49, 71, 48, 86, 81, 16, 23, 57, 05, 54,
                       01, 70, 54, 71, 83, 51, 54, 69, 16, 92, 33, 48, 61, 43, 52, 01, 89, 19, 67, 48 };
    while (limit <= _countof(matrix))
    {
      for (int i = 0; count + i <= limit - 4; i++)//horiz
      {
        mult = matrix[count + i] * matrix[count + i + 1] * matrix[count + i + 2] * matrix[count + i + 3];
        if (mult > max)
          max = mult;
        if (i + 3 < 20 && count < 337) //diag left
        {
          mult = matrix[count + i] * matrix[count + i + 21] * matrix[count + i + 42] * matrix[count + i + 63];
          if (mult > max)
            max = mult;
        }
        if (i >= 3 && count < 340)//diag right
        {
          mult = matrix[count + i] * matrix[count + i + 19] * matrix[count + i + 38] * matrix[count + i + 57];
          if (mult > max)
            max = mult;
          if ((count + i) == (limit - 4))
          {
            for (int j = 1; j < 4; j++) //diag right last three rows
            {
              mult = matrix[count + i + j] * matrix[count + i + 19 + j] * matrix[count + i + 38 + j] * matrix[count + i + 57 + j];
              if (mult > max)
                max = mult;
            }
          }
        }
      }
      for (int i = 0; i < 340; i++)//vert
      {
        mult = matrix[i] * matrix[i + 20] * matrix[i + 40] * matrix[i + 60];
        if (mult > max)
          max = mult;
      }
      limit = limit + 20;
      count = count + 20;
    }
    limit = 20;

    cout << "answer: " << max << "\n";
  }
}


// project management
namespace ProjectEulerProblems
{
  typedef void(*Euler)();
  std::vector<Euler> solutions =
  {
    Euler1,
    Euler2,
    Euler3,
    Euler4,
    Euler5,
    Euler6,
    Euler7,
    Euler8,
    Euler9,
    Euler10,
    Euler11,

  };

  void RunEulerSolution(unsigned number)
  {
    if (number <= solutions.size())
    {
      StopWatch sw;
      sw.start();

      solutions[number - 1]();

      sw.end();
      sw.printElapsed();
    }
    else
    {
      cout << "ERROR There is no Euler " << number << " solution\n";
    }
  }

  unsigned GetTotalSolved()
  {
    return unsigned(solutions.size());
  }
}