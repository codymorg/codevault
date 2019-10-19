/******************************************************************************
  * Project Euler Solutions
  * Author : Cody Morgan
  * Brief  : contains utility funstions for doing problems these are generic
  *          functions that just didn't fit in their own files

******************************************************************************/

#pragma once
#ifndef UTILITY_EULER_H
#define UTILTY_EULER_H
#include <chrono>
#include <string>

class StopWatch
{
public:
  enum class PrintMode
  {
    PrintDefault,
    PrintMilliSeconds,
    PrintSeconds,
  };

  StopWatch();
  void start();
  void end();
  unsigned getMilliseconds();
  unsigned getSeconds();
  void printElapsed(PrintMode mode = PrintMode::PrintDefault);
private:
  std::chrono::time_point<std::chrono::steady_clock> start_;
  std::chrono::time_point<std::chrono::steady_clock> end_;
};

namespace StringManipulation
{
  template <class charArray>
  bool IsPalindrome(const charArray& input)
  {
    unsigned size;

    // get size from char* or string
    if(dynamic_cast<std::string>(input))
      size = unsigned(input.size());
    else
    {
      for (unsigned i : input)
      {
        if (input[i] != '\0')
          size++;
        else
          break;
      }
    }

    for (unsigned i : input)
    {
      if (input[i] != input[size - 1])
        return false;
    }

    return true;
  }
}

#endif
