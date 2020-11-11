export function toUrlParams(obj, prefix = null, isEncode = true) {
  let str = [];
  let p;

  for (p in obj) {
    if (obj.hasOwnProperty(p)) {
      let k = prefix ? prefix + '.' + p  : p;
      let v = obj[p];
      str.push(
        v !== null && typeof v === 'object'
          ? toUrlParams(v, k, isEncode)
          : isEncode ? encodeURIComponent(k) + '=' + encodeURIComponent(v)
          : k + '=' + v
      );
    }
  }
  return str.filter(element => element !== '').join('&');
}
