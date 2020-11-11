export function isNull(val: any): boolean {
  if (val === undefined || val === null || val === '') {
    return true;
  }
  return false;
}

export function deleteNullProperties(val: any): void {
  for (let propName in val) {
    if (val.hasOwnProperty(propName) && isNull(val[propName])) {
      delete val[propName];
    }
  }
}
