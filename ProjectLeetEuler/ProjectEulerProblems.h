/******************************************************************************
  * Project Euler Solutions
  * Author : Cody Morgan
  * Brief  : Call RunEulerSolution to run a given Euler problem - if I haven't
  *          solved it an error message will display - otherwise the answer

******************************************************************************/

#ifndef PROJECT_EULER_H
#define PROJECT_EULER_H
#pragma once

namespace ProjectEulerProblems
{
  void RunEulerSolution(unsigned number); 
  unsigned GetTotalSolved(); 
};

// these are functions that are probably too specific to be used for other problems
namespace HelperFunctions
{

}
#endif

