/******************************************************************************
  * Author : Cody Morgan
  * Brief  : This is a collection of my solutions to ProjectEuler
  *          and Leetcode problems mostly done for fun
  *          This was orginally done in C# 2013 while I was deployed with the 
  *          Army but I was a young and reckless programmer so now I'm doing 
  *          it the right way - or at least a better way

******************************************************************************/

#include "ProjectEulerProblems.h"

#include <stdio.h>
#include <iostream>
using std::cout;

#include <vector>
using std::vector;

void Help()
{
  cout << "This is a collection of ProjectEuler Solutions.\n";
  cout << "Usage: ProjectEuler /[command] ##\n";
  cout << "  Available commands: /a : Print answer to all problems\n";
  cout << "                      /? : print help message\n";
  cout << "                       # : print answer to Project Euler problem #\n";
}

void All()
{
  for (unsigned i = 1; i <= ProjectEulerProblems::GetTotalSolved(); i++)
  {
    cout << "==========Project Euler Problem " << i << " ==========\n";

    ProjectEulerProblems::RunEulerSolution(i);

    cout << "========== End ==========\n\n";
  }
}


int main(int argc, const char* argv[])
{
  if(argv[1][0] == '/')
  {
    switch (argv[1][1])
    {
    case 'a':
      All();
      break;

    case 'h':
    case '?':
    default:
      Help();
      break;
    }
  }
  else
  {
    unsigned eulerNumber = atoi(argv[1]);
    ProjectEulerProblems::RunEulerSolution(eulerNumber);
  }
}



