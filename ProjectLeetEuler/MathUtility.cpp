/******************************************************************************
  * Project Euler Solutions
  * Author : Cody Morgan
  * Brief  : contains utility funstions related to math not computer science

******************************************************************************/

#include "MathUtility.h"
#include "Utility.h"
#include <bitset>
#include <iostream>
using std::cout;


//***** Huge class members *****//

Huge::Huge(unsigned long long init)
{
  setValue(init);
}

void Huge::addString(std::string input)
{
  std::list<unsigned char>::iterator current = baseArray_.begin();
  byte carry = 0;

  for (long i = long(input.size() - 1); i >= 0; i--)
  {
    byte c = input[i];

    // size verification
    if (current == baseArray_.end())
    {
      baseArray_.push_back(byte('\0'));
      current--;
    }

    // add this digit
    (*current) += c - '0' + carry;
    //cout << std::bitset<8>(*current) << '\n';

    // carry bit
    carry = (*current) / 10;
    (*current) %= 10;
    //cout << std::bitset<8>(*current) << '\n';

    current++;
  }

  // there could be one more carry
  while (carry)
  {
    if (current == baseArray_.end())
    {
      baseArray_.push_back(byte('\0'));
      current--;
    }

    // carry bit
    (*current) += carry;
    carry = (*current) / 10;
    (*current) %= 10;
    //cout << std::bitset<8>(*current) << '\n';

    current++;
  }
}

std::list<Huge::byte>::const_iterator Huge::begin() const
{
  return baseArray_.begin();
}

std::list<Huge::byte>::const_iterator Huge::end() const
{
  return baseArray_.end();
}

void Huge::setValue(unsigned long long value)
{
  baseArray_.clear();
  std::list<unsigned char>::iterator current = baseArray_.begin();
  byte carry = 0;

  while (value)
  {
    byte c = value % 10;
    value /= 10;

    // size verification
    if (current == baseArray_.end())
    {
      baseArray_.push_back(byte('\0'));
      current--;
    }

    // add this digit
    (*current) = c;
    (*current) += carry;
    //cout << std::bitset<8>(*current) << '\n';

    // carry bit
    carry = (*current) / 10;
    (*current) %= 10;
    //cout << std::bitset<8>(*current) << '\n';

    current++;
  }

  // there could be one more carry
  while (carry)
  {
    if (current == baseArray_.end())
    {
      baseArray_.push_back(byte('\0'));
      current--;
    }

    // carry bit
    (*current) += carry;
    carry = (*current) / 10;
    (*current) %= 10;
    //cout << std::bitset<8>(*current) << '\n';

    current++;
  }
}

unsigned long Huge::size() const
{
  return unsigned long(baseArray_.size());
}


//***** Generic math functions *****//

void printHugeFactorial(unsigned number, unsigned until)
{
  Huge start(number);
  Huge end(number);
  Huge factorial(number);

  for (unsigned i = number - 1; i > until; i--)
  {
    factorial *= i;
  }

  cout << factorial << "\n";
}

unsigned long long factorial(unsigned short number, unsigned short until)
{
  unsigned long long f = number;
  unsigned long long overflowDetect = f;

  for (unsigned long long i = f - 1; i > until; i--)
  {
    f *= i;
    if (f < overflowDetect)
    {
      throw Exceptions::Overflow(GET_LINE);
    }
    else
    {
      overflowDetect = f;
    }
  }

  return f;
}

/******************************************************************************

  N choose K determines the number of combinations concerning a given set and 
  choice of that set. the formula is n! / (k!(n-k)!) and can be simplified to
  Π(n->k) / k!

  as k! will likely be huge - it is split in have

******************************************************************************/
unsigned long long nChooseK(unsigned short n, unsigned short k)
{
  unsigned long long top = factorial(n, k + (k/2));       // get n->k as the factorial on top is canceled  
  unsigned long long top2 = factorial(k + (k/2), k);
  unsigned long long bot = factorial(k / 2);      // get half the factorial on bottom as it's going to be big
  unsigned long long bot2 = factorial(k, k / 2);  // get second half
  long double combos = top / double(bot);
  combos /= bot2;
  combos *= top2;
  return unsigned long long(combos);
}