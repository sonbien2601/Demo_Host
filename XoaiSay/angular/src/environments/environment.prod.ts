import { Environment } from '@abp/ng.core';

const baseUrl = 'https://vuaxoaivadongbon.site';
const oAuthConfig = {
  issuer: 'https://demohost-production.up.railway.app',
  redirectUri: baseUrl,
  clientId: 'XoaiSay_App',
  responseType: 'code',
  scope: 'offline_access XoaiSay',
  requireHttps: true,
};

export const environment = {
  production: true,
  application: {
    baseUrl,
    name: 'XoaiSay',
  },
  oAuthConfig,
  apis: {
    default: {
      url: 'https://demohost-production.up.railway.app',
      rootNamespace: 'XoaiSay',
    },
    AbpAccountPublic: {
      url: oAuthConfig.issuer,
      rootNamespace: 'AbpAccountPublic',
    },
  },
  remoteEnv: {
    url: '/getEnvConfig',
    mergeStrategy: 'deepmerge',
  },
} as Environment;
