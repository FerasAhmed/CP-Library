#include <iostream>
#include <math.h>

typedef long long ll;

ll n, maxK = 25;
ll table[200200][25]; 
ll val[200200];

using namespace  std;
int getK(int num){
  if(num == 0) return 0;
  int k = 0;
  do {
    ++k;
    num = num >> 1;
  } while(num > 0);
  return k - 1;
}

int minimum(int i, int j) {
  int dist = j - i;
  int k = getK(dist);
  return min(table[i][k], table[j-k][k]);
}

int main() {

  cin >> n;
  for(int i = 0 ; i < n; ++i){
    int num;
    cin >> num;
    table[i][0] = num;
  }

  for(int k = 1; k < maxK; ++k){
    for(int i = 0; i < n; ++i){
      if(i + (1 << (k-1)) < n ) {
      table[i][k] = 
      min(table[i][k-1], table[i + (1 << (k-1))][k-1]);}
      else{
        table[i][k] = table[i][k-1];
      }
    }
  }

  cout << "minimum in array is: " << minimum(0, 4) << endl;
  for(int i = 0; i < n; ++i)
  for(int j = i; j < n; ++j)
  cout << "minimum for range(" <<i<<","<<j<<") is: "<< minimum(i, j) << endl;
  
  std::cout << "Hello World!\n";
}
