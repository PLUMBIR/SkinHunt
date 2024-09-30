import { HttpParams, HttpUrlEncodingCodec } from "@angular/common/http";

class ExtendedUrlEncoder extends HttpUrlEncodingCodec {
    override encodeKey(key: string): string {
      return encodeURIComponent(key);
    }
  
    override encodeValue(value: string): string {
      return encodeURIComponent(value);
    }
}

export function httpParamsFromRequest(request: any): HttpParams {
    let httpParams = new HttpParams({ encoder: new ExtendedUrlEncoder() });
  
    if (!request) {
      return httpParams;
    }
  
    Object.keys(request).forEach((key) => {
      const val = request[key];
  
      if (typeof val != 'boolean' && !val) {
        return;
      }
      if (Array.isArray(val)) {
        val.forEach((v) => {
          httpParams = httpParams.append(key, v.toString());
        });
  
        return;
      }
  
      if (val instanceof Date) {
        httpParams = httpParams.append(key, val.toISOString());
  
        return;
      }
      httpParams = httpParams.append(key, val.toString());
    });
  
    return httpParams;
  }
  